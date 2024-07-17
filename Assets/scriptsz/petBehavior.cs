using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class petBehavior : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public float shootingInterval = 1.0f; 
    public float bulletSpeed = 5f; 
    public float detectionRange = 10f; 
    public float cooldownTime; // Time after which the pet will disappear
   
    public Transform bulletSpawnPoint;

    private Transform target;
    private Transform aimTransform;
    public Animator animator;

    //This actually works when I didnt apply the animation yet. Havent set up the limit of the Active Summoned pet... 
    void Start()
    {

        // Create empty GameObject for aiming
        aimTransform = new GameObject("AimTransform").transform;
        aimTransform.SetParent(transform, false);
        animator.SetBool("Enter", true);
       // For Animation idle -- tbh It doesnt work so I apologize for this TwT
        Invoke("StartIdleAnimation", cooldownTime / 2);

        InvokeRepeating("ShootAtEnemy", shootingInterval, shootingInterval);
        Invoke("Disappear", cooldownTime); // Schedule the pet to disappear after Cooldown
    }

    void Update()
    {
        FindTarget();
        if (target != null && bulletSpawnPoint != null)
        {
            // Update aimTransform rotation instead of the pet otation
            Vector3 direction = target.position - aimTransform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            aimTransform.rotation = rotation;
        }
    }

    // AutoAims Enemies in a certain distance.
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

    //Shoots the enemy. (PetBulletSpawnPoint is assigned with this so the prefab wont rotate along the aiming)
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

    // Attempting to do what you did with the animation. It doesnt actually work TwT
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
