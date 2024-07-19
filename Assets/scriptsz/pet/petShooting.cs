using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class petShooting : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    public float shootCooldown, range, rateOfFire; //ROF = rate of fire
    public LayerMask enemyMask;
    private Transform target;

    public float force = 10f;

    public Animator animator;

    petStats stats;

    void Awake()
    {
        stats = GetComponentInChildren<petStats>();
    }
    //range only visible on scene view and if you are clicked on the pet
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, range);
    }

    void Update()
    {
        range = stats.petRange;
        rateOfFire = stats.petROF;

        if (target == null)
        {
            FindTarget();
            return;
        }

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            shootCooldown += Time.deltaTime;
            if (shootCooldown >= 1f / rateOfFire)
            {
                Debug.Log("attempting to shoot");
                shoot();
                shootCooldown = 0f;
            }
        }
    }

    bool CheckTargetIsInRange()
    {
        Debug.Log(Vector2.Distance(target.position, transform.position) <= range);
        return Vector2.Distance(target.position, transform.position) <= range;
    }

    void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, enemyMask);
        if(hits.Length > 0)
        {
            target = hits[0].transform;
            Debug.Log("I FOUND SOMEONE!!!");
        }
    }

    void shoot()
    {
        Debug.Log("CHECK YOSELF FOOL");
        //where all the shooting happens
        GameObject bullet = Instantiate(bulletPrefab, new Vector3(bulletSpawn.position.x, bulletSpawn.position.y + 0.5f, 0), Quaternion.identity);
        NPCBullet bulletscript = bullet.GetComponent<NPCBullet>();
        bulletscript.SetTarget(target);
    }
}
