using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimestats : MonoBehaviour
{
    //slime's statistics
    public float
        maxHealth = 5f,
        health,
        moveSpeed = 2f,
        attack = 1f,
        attackRate = 1f;
    enemyChase chaseScript;
    healthBar bar;
    
    //get relevant values outside script (healthBar.cs and chase ai)
    void Awake()
    {
        chaseScript = GetComponentInChildren<enemyChase>();
        bar = GetComponentInChildren<healthBar>();

        health = maxHealth;
    }

    //just checks for health changes and updates the health bar with bar.UpdateHealth
    void Update()
    {
        bar.UpdateHealth(maxHealth, health);

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
