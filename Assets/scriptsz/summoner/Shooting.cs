using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    public float shootCooldown;
    static public bool canFire;

    public float force = 10f;

    void Start()
    {
        canFire = true;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && canFire == true)
        {
            shoot();
            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer()
    {
        canFire = false;
        yield return new WaitForSeconds(shootCooldown);

        canFire = true;
        yield return null;
    }

    void shoot()
    {
        
        GameObject bullet = Instantiate(bulletPrefab, new Vector3(bulletSpawn.position.x, bulletSpawn.position.y + 0.5f, 0), Quaternion.identity);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletSpawn.up * force, ForceMode2D.Impulse);
    }
}
