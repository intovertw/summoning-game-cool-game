using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class petBehavior : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab of the bullet
    public float shootingInterval = 1.0f; // Time between shots
    public float bulletSpeed = 5f; // Speed of the bullet
    public float detectionRange = 10f; // Range within which the pet detects and shoots at enemies
    public float cooldownTime = 10f; // Time after which the pet will disappear
    [HideInInspector]
    public Transform bulletSpawnPoint; 

    private Transform target;

    void Start()
    {
        InvokeRepeating("ShootAtEnemy", shootingInterval, shootingInterval);
        Invoke("Disappear", cooldownTime); // Schedule the pet to disappear after cooldownTime seconds
    }

    void Update()
    {
        FindTarget();
        if (target != null && bulletSpawnPoint != null)
        {
           
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
        }
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        float closestDistance = detectionRange;
        target = null; // Reset target

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                target = enemy.transform;
            }
        }
    }

    void ShootAtEnemy()
    {
        if (target != null && bulletSpawnPoint != null)
        {
            // Calculate current direction to the target
            Vector2 shootingDirection = (target.position - bulletSpawnPoint.position).normalized;

            // Create a bullet and set its direction
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = shootingDirection * bulletSpeed;

            // Rotate the bullet to face the target
            float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void Disappear()
    {

        Destroy(gameObject);
    }
}
