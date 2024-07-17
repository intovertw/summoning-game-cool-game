using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTurretBehavior : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float stoppingDistance = 2f;
    public float attackDamage = 10f;
    public GameObject bulletPrefab;
    public float shootingInterval = 1.0f;
    public float originalShootingInterval;
    public float bulletSpeed = 5f;
    public float detectionRange = 10f;
    public Transform bulletSpawnPoint;

    private Transform player;
    private Transform aimTransform;
    private Rigidbody2D rb;

    [SerializeField] HealthbarBehavior Healthbar;
    [SerializeField] float health, maxHealth;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Healthbar = GetComponentInChildren<HealthbarBehavior>();
    }

    void Start()
    {
        aimTransform = new GameObject("AimTransform").transform;
        aimTransform.SetParent(transform, false);

        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
        originalShootingInterval = shootingInterval;

        InvokeRepeating("ShootAtPlayer", shootingInterval, shootingInterval);
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 moveDirection = (player.position - transform.position).normalized;

            if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
            {
                transform.position += moveDirection * moveSpeed * Time.deltaTime;
            }

            transform.up = moveDirection;

            Vector3 direction = player.position - aimTransform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            aimTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void ShootAtPlayer()
    {
        if (player != null && bulletSpawnPoint != null)
        {
            Vector2 shootingDirection = (player.position - bulletSpawnPoint.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = shootingDirection * bulletSpeed;

            float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            enemyBullet bulletBehavior = bullet.GetComponent<enemyBullet>();
            if (bulletBehavior != null)
            {
                bulletBehavior.damage = attackDamage;
            }
        }
    }

    public void UpdateShootingInterval(float newInterval)
    {
        CancelInvoke("ShootAtPlayer");
        InvokeRepeating("ShootAtPlayer", newInterval, newInterval);
        shootingInterval = newInterval;
    }

    public void TakeHit(float damage)
    {
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
