using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public float damage = 10f;
    public float bulletLifetime = 2f; //It just destroys the bullet when didnt collide with anything within this seconds. 

    void Start()
    {
        // Set the layer of this bullet to EnemyBullet so that It will ignore the collision with the player bullet
        gameObject.layer = LayerMask.NameToLayer("EnemyBullet");

        // Ignore collisions between EnemyBullet and PlayerBullet layers
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("EnemyBullet"), LayerMask.NameToLayer("PlayerBullet"));
        Destroy(gameObject, bulletLifetime);
    }

  

    void OnTriggerEnter2D(Collider2D other)
    {
        //how enemy bullet hits player
        if (other.gameObject.CompareTag("Player"))
        {

            SummonerBehavior player = other.gameObject.GetComponent<SummonerBehavior>();

            if (player != null)
            {

                player.TakeHit(damage);
            }
            //this will destroy the bullet only so that it wont affect the enemy. Im thinking maybe I should just ignore the collider na din instead if madaming nakaharang na enemy sa harap. 
            else if (!other.gameObject.CompareTag("enemy"))
            {
                Destroy(gameObject);
            }

            Destroy(gameObject);
        }
    }
}
