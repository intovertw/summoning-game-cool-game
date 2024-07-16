using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class petBehavior : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab of the bullet
    public float shootingInterval = 1.0f; // Time between shots
    public float bulletSpeed = 5f; // Speed of the bullet
    public float detectionRange = 10f; // Range within which the pet detects and shoots at enemies
    public float cooldownTime; // Time after which the pet will disappear
   
    public Transform bulletSpawnPoint;

    private Transform target;
    private Transform aimTransform;
    public Animator animator;


    void Start()
    {

        // Create an empty GameObject for aiming
        aimTransform = new GameObject("AimTransform").transform;
        aimTransform.SetParent(transform, false);
        animator.SetBool("Enter", true);
        // Schedule the pet to start the Idle animation after half the cooldown time
        Invoke("StartIdleAnimation", cooldownTime / 2);

        InvokeRepeating("ShootAtEnemy", shootingInterval, shootingInterval);
        Invoke("Disappear", cooldownTime); // Schedule the pet to disappear after cooldownTime seconds
    }

    void Update()
    {
        FindTarget();
        if (target != null && bulletSpawnPoint != null)
        {
            // Update aimTransform rotation instead of the prefab's rotation
            Vector3 direction = target.position - aimTransform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            aimTransform.rotation = rotation;
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

    void StartIdleAnimation()
    {
        // Stop the Enter animation and start the Idle animation
        animator.SetBool("Enter", false);
        animator.SetBool("Idle", true);
    }

    void Disappear()
    {
        Destroy(gameObject);
    }
}
