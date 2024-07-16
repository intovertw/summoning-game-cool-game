using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    public float moveSpeed = 3f; // Speed at which the enemy moves towards the player
    public float stoppingDistance = 2f; // Distance at which the enemy stops approaching the player
    public float attackDamage = 10f; // Amount of damage the enemy deals per attack
    private Transform player; // Reference to the player's transform
    [SerializeField] HealthbarBehavior Healthbar;
    [SerializeField] float health, maxHealth; // Maximum health of the enemy
    
    Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Healthbar.UpdateHealthBar(health, maxHealth);

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Healthbar = GetComponentInChildren<HealthbarBehavior>();
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
