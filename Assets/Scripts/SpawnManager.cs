using UnityEngine;
using System.Collections.Generic;
using System.Collections;

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

    void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 1, 2);
    }

    private float lastSpawnRate = -1f; // Valore iniziale impossibile

    void Update()
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
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private void SpawnPowerUp(Vector3 spawnPosition)
    {
        spawnPosition.y = 0.5f;
        int randomIndex = Random.Range(0, powerUpPrefabs.Count);
        Instantiate(powerUpPrefabs[randomIndex], spawnPosition, Quaternion.identity);
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