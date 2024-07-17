using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float attackDamage = 10f;
    public float attackRange = 1.5f;
    public float timeBetweenFiring = 1f; // Original time between firing
    public float originalTimeBetweenFiring;

    private Camera mainCam;
    private Vector3 mousePos;

    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;

    public Animator animator;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        originalTimeBetweenFiring = timeBetweenFiring;
    }

    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            animator.SetBool("onClick", false);
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButton(0) && canFire)
        {
            animator.SetBool("onClick", true);
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
    }

    public void UpdateShootingInterval(float multiplier)
    {
        timeBetweenFiring = originalTimeBetweenFiring * multiplier;
    }
}
