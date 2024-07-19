using System.Collections;
using UnityEngine;

public class HiddenGemSpawner : MonoBehaviour
{
    public float minSpawnInterval = 20f; // Minimum time interval for spawning
    public float maxSpawnInterval = 40f; // Maximum time interval for spawning
    public Transform[] spawnPoints;
    public GameObject hiddenGemPrefab;

    private GameObject currentHiddenGem;

    void Start()
    {
        StartCoroutine(SpawnHiddenGem());
    }

    private IEnumerator SpawnHiddenGem()
    {
        while (true)
        {
            // Wait for a random interval between minSpawnInterval and maxSpawnInterval
            yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));

            // Destroy the existing hidden gem if it exists
            if (currentHiddenGem != null)
            {
                Destroy(currentHiddenGem);
            }

            // Select a random spawn point from the array
            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomSpawnPointIndex];

            // Instantiate the hidden gem prefab spawn point
            currentHiddenGem = Instantiate(hiddenGemPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
