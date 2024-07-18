using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyChase : MonoBehaviour
{
    public float distance;
    public GameObject player;

    slimestats slime;


    void Awake()
    {
        slime = GetComponentInChildren<slimestats>();
    }
        
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, slime.moveSpeed * Time.deltaTime);
    }
}
