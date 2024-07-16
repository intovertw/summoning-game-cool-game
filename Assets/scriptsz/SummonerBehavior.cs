using UnityEngine;

public class SummonerBehavior : MonoBehaviour
{
    public float attackDamage = 10f; // Amount of damage the player deals per attack
    public float attackRange = 1.5f; // Range within which the player can attack

    [SerializeField] HealthbarBehavior Healthbar;
    [SerializeField] float health, maxHealth; // Maximum health of the enemy
    Rigidbody2D rb;

    void Start()
    {
        health = maxHealth; // Initialize health
        Healthbar.UpdateHealthBar(health, maxHealth);
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Healthbar = GetComponentInChildren<HealthbarBehavior>();
    }

    void Update()
    {
        
    }


    public void TakeHit(float damage)
    {
        // Reduce health by the amount of damage taken
        health -= damage;
        Healthbar.UpdateHealthBar(health, maxHealth);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        Destroy(gameObject);
    }
}
