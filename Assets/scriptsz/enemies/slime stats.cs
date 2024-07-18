using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimestats : MonoBehaviour
{
    public float
        maxHealth = 5f,
        health,
        moveSpeed = 2f,
        attack = 1f,
        attackRate = 1f;
    enemyChase chaseScript;
    healthBar bar;
    

    void Awake()
    {
        chaseScript = GetComponentInChildren<enemyChase>();
        bar = GetComponentInChildren<healthBar>();

        health = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            health--;
            bar.UpdateHealth(maxHealth, health);
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
