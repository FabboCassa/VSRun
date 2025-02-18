using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.Netcode;


public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private bool isSpawning = true;
    public List<GameObject> powerUpPrefabs;
    private readonly Dictionary<EnumPosition, Vector3> positions = new()
    {
        { EnumPosition.Left, new Vector3(-3, 0, 25) },
        { EnumPosition.Center, new Vector3(0, 0, 25) },
        { EnumPosition.Right, new Vector3(3, 0, 25) }
    };

    private void Start()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += SpawnPlayer;
            InvokeRepeating(nameof(SpawnObject), 1, 1);
        }
    }

    private void SpawnPlayer(ulong clientId)
    {
        if (NetworkManager.Singleton.IsServer) // Solo l'host può spawnare i player
        {
            GameObject player = Instantiate(NetworkManager.Singleton.NetworkConfig.PlayerPrefab, Vector3.zero, Quaternion.identity);
            player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
        }
    }

    private float lastSpawnRate = -1f; // Valore iniziale impossibile

    private void FixedUpdate()
    {
        {
            float currentTime = GameManager.Instance.GetTime();
            float newSpawnRate = GetSpawnRate(currentTime);

            if (newSpawnRate != lastSpawnRate)
            {
                StopSpawning();
                Wait(3f);
                if (newSpawnRate > 0)
                {
                    isSpawning = true;
                    InvokeRepeating(nameof(SpawnObject), 1, newSpawnRate);
                    Debug.Log("Spawn rate changed to: " + newSpawnRate);
                }
                lastSpawnRate = newSpawnRate;
            }
        }
    }

    private float GetSpawnRate(float time)
    {
        if (time >= (float)PhaseTime.ThirdPhase) return 0.2f;
        if (time >= (float)PhaseTime.SecondPhase) return 0.5f;
        if (time >= (float)PhaseTime.FirstPhase) return 1f;
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
            var instanceNetworkObject = instance.GetComponent<NetworkObject>();
            instanceNetworkObject.Spawn();
        }
    }

    private void SpawnPowerUp(Vector3 spawnPosition)
    {
        spawnPosition.y = 0.5f;
        int randomIndex = Random.Range(0, powerUpPrefabs.Count);
        var instance = Instantiate(powerUpPrefabs[randomIndex], spawnPosition, Quaternion.identity);
        var instanceNetworkObject = instance.GetComponent<NetworkObject>();
        instanceNetworkObject.Spawn();
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

    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

}