using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public float maxHealth = 10f, health, rateOfFire, speed, petHealth, petROF;
    healthBar bar;

    void Awake()
    {
        bar = GetComponentInChildren<healthBar>();
    }

    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (health <= 0)
        {
            //DAED!!!!
        }
    }
}
