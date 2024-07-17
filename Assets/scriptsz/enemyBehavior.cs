using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    public float moveSpeed = 3f; // Speed at which the enemy moves towards the player
    public float stoppingDistance = 2f; // Distance at which the enemy stops approaching the player
    public float attackDamage = 10f; // Amount of damage the enemy deals per attack
    public float attackCooldown = 1f; // Time between attacks
    private Transform player; // Reference to the player's transform
    [SerializeField] HealthbarBehavior Healthbar;
    [SerializeField] float health, maxHealth; // Maximum health of the enemy

    private Rigidbody2D rb;
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Healthbar.UpdateHealthBar(health, maxHealth);
        lastAttackTime = -attackCooldown; // Initialize to allow immediate attack
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

            // Move towards the player if not within stopping distance
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer > stoppingDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }

            // Rotate to face the player
            transform.up = moveDirection;

            // Attack or perform action when within attacking distance
            if (distanceToPlayer <= stoppingDistance && Time.time >= lastAttackTime + attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }
        }
    }

    void AttackPlayer()
    {
        if (player != null && player.CompareTag("Player"))
        {
            SummonerBehavior playerHealth = player.GetComponent<SummonerBehavior>();
            if (playerHealth != null)
            {
                playerHealth.TakeHit(attackDamage);
            }
            
        }

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
