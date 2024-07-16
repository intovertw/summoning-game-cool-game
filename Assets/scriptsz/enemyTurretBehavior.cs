using System.Collections;
using UnityEngine;

public class enemyTurretBehavior : MonoBehaviour
{
    public float moveSpeed = 3f; // Speed at which the enemy moves towards the player
    public float stoppingDistance = 2f; // Distance at which the enemy stops approaching the player
    public float attackDamage = 10f; // Amount of damage the enemy deals per attack
    public GameObject bulletPrefab; // Prefab of the bullet
    public float shootingInterval = 1.0f; // Time between shots
    public float bulletSpeed = 5f; // Speed of the bullet
    public float detectionRange = 10f; // Range within which the pet detects and shoots at enemies
    public Transform bulletSpawnPoint;

    private Transform player; // Reference to the player's transform
    private Transform aimTransform;
    private Rigidbody2D rb;

    [SerializeField] HealthbarBehavior Healthbar;
    [SerializeField] float health, maxHealth; // Maximum health of the enemy

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Healthbar = GetComponentInChildren<HealthbarBehavior>();
    }

    // Start is called before the first frame update
    void Start()
    {
        aimTransform = new GameObject("AimTransform").transform;
        aimTransform.SetParent(transform, false);

        // Find the player using the tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth; // Initialize health

        // Start shooting at player with intervals
        InvokeRepeating("ShootAtPlayer", shootingInterval, shootingInterval);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Calculate the direction to move towards the player
            Vector3 moveDirection = (player.position - transform.position).normalized;

            // Move towards the player if not within stopping distance
            if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
            {
                transform.position += moveDirection * moveSpeed * Time.deltaTime;
            }

            // Rotate to face the player
            transform.up = moveDirection;

            // Update aimTransform rotation
            Vector3 direction = player.position - aimTransform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            aimTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void ShootAtPlayer()
    {
        if (player != null && bulletSpawnPoint != null)
        {
            // Calculate current direction to the player
            Vector2 shootingDirection = (player.position - bulletSpawnPoint.position).normalized;

            // Create a bullet and set its direction
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = shootingDirection * bulletSpeed;

            // Rotate the bullet to face the target
            float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Ensure the bullet script has the correct damage value
            enemyBullet bulletBehavior = bullet.GetComponent<enemyBullet>();
            if (bulletBehavior != null)
            {
                bulletBehavior.damage = attackDamage;
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
