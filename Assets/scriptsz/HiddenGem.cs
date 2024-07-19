using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HiddenGem : MonoBehaviour
{
    private GameObject[] slimeStatistics;
    public LayerMask enemyMask;
    public float originalSpeed, originalAttack;

    public float range = 5f;

    slimestats enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<slimestats>();
        originalSpeed = enemy.moveSpeed;
        originalAttack = enemy.attack;
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(new Vector3(transform.position.x, transform.position.y + 2, 0), transform.forward, range);
    }

    // Update is called once per frame
    void Update()
    {
        slimeStatistics = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < slimeStatistics.Length; i++)
        {
            enemy = slimeStatistics[i].GetComponent<slimestats>();
            enemy.moveSpeed = originalSpeed;
            enemy.attack = originalAttack;
        }
        FindTarget();
    }

    void FindTarget()
    {
        slimeStatistics = GameObject.FindGameObjectsWithTag("Enemy");

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                enemy = slimeStatistics[i].GetComponent<slimestats>();
                enemy.moveSpeed = 4f;
                enemy.attack = 2f;
                Debug.Log("buffing!");
            }
        }
    }
}
