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

  

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            SummonerBehavior player = other.gameObject.GetComponent<SummonerBehavior>();

            if (player != null)
            {

                player.TakeHit(damage);
            }

            else if (!other.gameObject.CompareTag("enemy"))
            {
                Destroy(gameObject);
            }

            Destroy(gameObject);
        }
    }
}
