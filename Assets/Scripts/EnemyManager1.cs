using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager1 : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 3f;
    private float spawnTimer;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(6, Random.Range(-2, 2)); // Появление справа
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}

