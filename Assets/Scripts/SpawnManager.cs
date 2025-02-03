using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private bool isSpawning = true;
    private Dictionary<EnumPosition, Vector3> positions = new Dictionary<EnumPosition, Vector3>
    {
        { EnumPosition.Left, new Vector3(-5, 0, 25) },
        { EnumPosition.Center, new Vector3(0, 0, 25) },
        { EnumPosition.Right, new Vector3(5, 0, 25) }
    };

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1, 2);
    }

    private void SpawnEnemy()
    {
        if (!isSpawning) return;

        EnumPosition randomPosition = (EnumPosition)Random.Range(0, 3);
        Vector3 spawnPosition = positions[randomPosition];

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    public void StopSpawning()
    {
        isSpawning = false;
        CancelInvoke("SpawnEnemy");
    }

    public bool IsSpawning()
    {
        return isSpawning;
    }
}
