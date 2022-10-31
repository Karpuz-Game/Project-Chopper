using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Public
    public float enemySpawnDelay = 1;
    public GameObject enemyPrefab;
    
    // Private
    private GameObject[] enemies = new GameObject[30];
    
    private void Start()
    {
        for (var i = 0; i < enemies.Length; i++)
        {
            enemies[i] = SpawnEnemy();
        }
    }


    private void Update()
    {
        
    }

    GameObject SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);

        return enemy;
    }
}
