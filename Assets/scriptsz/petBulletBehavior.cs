using UnityEngine;

public class PetBullet : MonoBehaviour
{
    public float bulletLifetime = 2f; // Lifetime of the bullet before it self-destructs
    public float bulletDamage = 10f; // Amount of damage the bullet deals   

    void Start()
    {
        // Destroy the bullet after a certain amount of time to prevent clutter
        Destroy(gameObject, bulletLifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            
            enemyBehavior enemy = other.gameObject.GetComponent<enemyBehavior>();

            if (enemy != null)
            {
                
                enemy.TakeHit(bulletDamage);
            }
            else if (!other.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject); 
            }

            Destroy(gameObject);
        }
    }
}
