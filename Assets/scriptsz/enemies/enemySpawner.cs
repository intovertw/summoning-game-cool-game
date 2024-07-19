using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject[] enemies;

    void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    //basic random spawner
    IEnumerator EnemySpawn()
    {
        int random;
        while(true)
        {
            random = Random.Range(0, 3);
            Instantiate(enemies[random], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(5f);

        }
    }
}
