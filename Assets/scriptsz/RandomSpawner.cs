using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    //Declare arrays to fill the spawnpoint positions and enemy prefabs
    public float spawnRate = 1f;
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public bool canSpawn = true;

    void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while (canSpawn)
        {
            yield return wait;

            // Randomly select an enemy prefab
            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[randEnemy];

            // Randomly select a spawn point
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randSpawnPoint];

            // Instantiate the enemy at the selected spawn point
            Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);
        }
    }

    void Update()
    {
        
    }
}
