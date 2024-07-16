using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public float damage = 10f;
    public float bulletLifetime = 2f; // Lifetime of the bullet before it self-destructs

    void Start()
    {
        // Destroy the bullet after a certain amount of time to prevent clutter
        Destroy(gameObject, bulletLifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // If the bullet collides with the player, apply damage
            SummonerBehavior playerHealth = collision.gameObject.GetComponent<SummonerBehavior>();
            if (playerHealth != null)
            {
                playerHealth.TakeHit(damage); // Apply damage to the player
            }
            Destroy(gameObject); // Destroy the bullet after hitting the player
        }
        else if (collision.gameObject.CompareTag("enemy"))
        {
            // If the bullet collides with another enemy, just destroy the bullet without applying damage
            Destroy(gameObject);
        }
        else
        {
            // Destroy the bullet on any other collision as well (e.g., walls or obstacles)
            Destroy(gameObject);
        }
    }
}
