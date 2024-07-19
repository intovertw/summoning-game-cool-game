using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerStats : MonoBehaviour
{
    //player's and summon pet's statistics
    public float 
        maxHealth = 10f, 
        health, 
        rateOfFire, 
        speed, 
        petMaxAmount, 
        petMaxHealth,
        petRange,
        petROF;
    public GameObject[] petSummonsArray;
    public int petAmount;
    healthBar bar;
    petStats pet;
    Summon summon;
    private GameSceneManager gameSceneManager;

    //get health bar which is outside this script (healthBar.cs)
    void Awake()
    {
        summon = GetComponentInChildren<Summon>();
        bar = GetComponentInChildren<healthBar>();
    }

    //sets health with maxHealth value
    void Start()
    {
        health = maxHealth;
        gameSceneManager = FindObjectOfType<GameSceneManager>();
    }

    //checks for health changes and updates healthBar.cs
    void Update()
    {
        //health
        bar.UpdateHealth(maxHealth, health);

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
            if (gameSceneManager != null)
            {
                gameSceneManager.LoadMainMenu();
            }
        }

        //pet
        petSummonsArray = GameObject.FindGameObjectsWithTag("Pet");
        petAmount = GameObject.FindGameObjectsWithTag("Pet").Length;

        //if the amount of pets is more than max, instead of creating a new pet, we move the oldest pet (i.e two max pets only, move first summoned pet, leave second)
        int nextPet = 0;
        if(petAmount > petMaxAmount - 1)
        {
            if (nextPet > petMaxAmount - 1)
            {
                nextPet = 0;
            }
            summon.objectToPlace = petSummonsArray[nextPet];
            nextPet++;
        }
    }
}
