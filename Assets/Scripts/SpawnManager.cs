using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.Netcode;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject playerPrefab;
    private bool isSpawning = true;
    public List<GameObject> powerUpPrefabs;
    private Vector3 positionPlayer = new Vector3(0, 0.7f, 0);
    private GameManager gameManager;

    private readonly Dictionary<EnumPosition, Vector3> positions = new()
    {
        { EnumPosition.Left, new Vector3(-3, 0.5f, 45) }, // Adjusted y-coordinate
        { EnumPosition.Center, new Vector3(0, 0.5f, 45) }, // Adjusted y-coordinate
        { EnumPosition.Right, new Vector3(3, 0.5f, 45) } // Adjusted y-coordinate
    };

    private void Start()
    {
        if (NetworkManager.Singleton == null)
        { //if offline
            InvokeRepeating(nameof(SpawnObject), 1, 1);
            SpawnPlayer();
        }
        else if (NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += SpawnPlayerMultiplayer;
            InvokeRepeating(nameof(SpawnObject), 1, 1);
        }
        gameManager=GameManager.Instance;
    }

    private void SpawnPlayerMultiplayer(ulong clientId)
    {
        if (NetworkManager.Singleton.IsServer) // Only the host can spawn players
        {
            GameObject player = Instantiate(NetworkManager.Singleton.NetworkConfig.PlayerPrefab, positionPlayer, Quaternion.identity); // Adjusted y-coordinate
            player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
        }
    }

    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, positionPlayer, Quaternion.identity); // Adjusted y-coordinate
    }

    private float lastSpawnRate = -1f; // Initial impossible value

    private void FixedUpdate()
    {
        float currentTime = gameManager.GetTime();
        float newSpawnRate = GetSpawnRate(currentTime);

        if (newSpawnRate != lastSpawnRate)
        {
            StopSpawning();
            StartCoroutine(WaitAndStartSpawning(newSpawnRate));
            lastSpawnRate = newSpawnRate;
        }
    }

    private IEnumerator WaitAndStartSpawning(float newSpawnRate)
    {
        yield return Wait(5);
        if (newSpawnRate > 0)
        {
            isSpawning = true;
            InvokeRepeating(nameof(SpawnObject), 1, newSpawnRate);
            Debug.Log("Spawn rate changed to: " + newSpawnRate);
        }
    }

    private float GetSpawnRate(float time)
    {
        if (time >= (float)PhaseTime.FifthPhase) return 0.2f;
        else if (time >= (float)PhaseTime.FourthPhase) return 0.3f;
        else if (time >= (float)PhaseTime.ThirdPhase) return 0.5f;
        else if (time >= (float)PhaseTime.SecondPhase) return 0.7f;
        else if (time >= (float)PhaseTime.FirstPhase) return 1f;
        return -1f;
    }

    private void SpawnObject()
    {
        if (!isSpawning) return;

        EnumPosition randomPosition = (EnumPosition)Random.Range(0, 3);
        Vector3 spawnPosition = positions[randomPosition];

        float roll = Random.Range(0f, 1f);

        if (roll <= 0.05f && powerUpPrefabs.Count > 0)
        {
            SpawnPowerUp(spawnPosition);
        }
        else
        {
            var instance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            if (isMultiplayer())
            {
                SpawnObjectMultiplayer(instance);
            }
        }
    }

    private void SpawnPowerUp(Vector3 spawnPosition)
    {
        spawnPosition.y = 0.5f; // Adjusted y-coordinate for power-ups
        int randomIndex = Random.Range(0, powerUpPrefabs.Count);
        var instance = Instantiate(powerUpPrefabs[randomIndex], spawnPosition, Quaternion.identity);
        if (isMultiplayer())
        {
            SpawnObjectMultiplayer(instance);
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
        CancelInvoke(nameof(SpawnObject));
    }

    public bool IsSpawning()
    {
        return isSpawning;
    }

    private bool isMultiplayer()
    {
        return NetworkManager.Singleton != null;
    }

    private void SpawnObjectMultiplayer(GameObject instance)
    {
        var instanceNetworkObject = instance.GetComponent<NetworkObject>();
        instanceNetworkObject.Spawn();
    }

    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public float getYPlayerPosition()
    {
        return positionPlayer.y;
    }
}