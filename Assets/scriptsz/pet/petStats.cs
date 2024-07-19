using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class petStats : MonoBehaviour
{
    public float
        petMaxHealth,
        petHealth,
        petMaxAmount,
        petAmount,
        petRange,
        petROF;

    playerStats player;

    void Awake()
    {
        player = GetComponentInChildren<playerStats>();
        petHealth = petMaxHealth;
        petMaxAmount = 2;
        petROF = 1f;
        petRange = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        petMaxAmount = player.petMaxAmount;
    }
}
