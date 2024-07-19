using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HiddenGem : MonoBehaviour
{
    private GameObject[] slimeStatistics;
    public LayerMask enemyMask;
    public float originalSpeed, originalAttack;

    private Transform targets;

    public float range = 5f;

    slimestats enemy;
    // Start is called before the first frame update
    void Awake()
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

        BuffTarget();
    }

    bool CheckTargetIsInRange(Transform targets)
    {
        Debug.Log(Vector2.Distance(targets.position, transform.position) <= range);
        return Vector2.Distance(targets.position, transform.position) <= range;      
    }

    void BuffTarget()
    {
        slimeStatistics = GameObject.FindGameObjectsWithTag("Enemy");

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, enemyMask);
        if (hits.Length > 0)
        {
            int j = 0;
            targets = hits[j].transform;
            for (int i = 0; i < hits.Length; i++)
            {
                enemy = slimeStatistics[i].GetComponent<slimestats>();
                enemy.moveSpeed = 4f;
                enemy.attack = 2f;
                Debug.Log("buffing!");
            }
            j++;
        }
    }
}
