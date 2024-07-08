using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    public float moveSpeed = 3f; // Speed at which the enemy moves towards the player
    public float stoppingDistance = 2f; // Distance at which the enemy stops approaching the player
    public float attackDamage = 10f; // Amount of damage the enemy deals per attack
    private Transform player; // Reference to the player's transform
    public HealthbarBehavior Healthbar;
    public float maxHealth = 100f; // Maximum health of the enemy
    private float currentHealth; // Current health of the enemy

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 
        currentHealth = maxHealth;
        Healthbar.SetHealth(currentHealth, maxHealth);
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate the direction to move towards the player
            Vector3 moveDirection = (player.position - transform.position).normalized;

            // Move towards the player
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

           
            transform.up = moveDirection; 

            // Check distance to player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer <= stoppingDistance)
            {
                // Attack or perform action when within attacking distance
                AttackPlayer();
            }
        }
    }

    void AttackPlayer()
    {
        // Deal damage to the player (Nagdedebug pa)
       // player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);

    }

    public void TakeHit(float damage)
    {
        // Reduce health by the amount of damage taken
        currentHealth -= damage;
        Healthbar.SetHealth(currentHealth, maxHealth);
     
        if (currentHealth <= 0)
        {
            Die(); 
        }
    }

    void Die()
    {
        
        Destroy(gameObject); 
    }
}
