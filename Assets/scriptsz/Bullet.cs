using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;

    public float bulletLifetime = 2f; // Lifetime of the bullet before it self-destructs
    public float bulletDamage = 10f; // Amount of damage the bullet deals   


    // Start is called before the first frame update
    void Start()
    {
        

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position -mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,rot+90);

        // Set the layer of this bullet to PlayerBullet for ignoreCollision purposes. 
        gameObject.layer = LayerMask.NameToLayer("PlayerBullet");

        // Ignore collisions between PlayerBullet and EnemyBullet layers
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerBullet"), LayerMask.NameToLayer("EnemyBullet"));

        Destroy(gameObject, 5f);
    }


    // Update is called once per frame
    void Update()
    {
        
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
            enemyTurretBehavior turret = other.gameObject.GetComponent<enemyTurretBehavior>();
            if (turret != null)
            {
                turret.TakeHit(bulletDamage);
            }

            else if (!other.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }

            Destroy(gameObject);
        }
    }
}
