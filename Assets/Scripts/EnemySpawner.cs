using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spawnPrefab;
    public Transform spawnPoint;
    public int maxSpawnCount = 5;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < maxSpawnCount; i++)
        {
            SpawnEnemy();
        }
    }

    private void Update()
    {
        if (spawnedEnemies.Count < 5 )
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(spawnPrefab, spawnPoint.position, Quaternion.identity);
        newEnemy.transform.parent = spawnPoint;
        

        newEnemy.GetComponent<LevelSystem>();
        newEnemy.GetComponent<PathAI>();

        spawnedEnemies.Add(newEnemy);
    }
}