using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenGem : MonoBehaviour
{
    public float range = 5f;
    public LayerMask enemy; 
    public float buffedMoveSpeed = 4f; 
    public float buffedAttack = 2f;

    private List<slimestats> buffedEnemies = new List<slimestats>();

    private void Awake()
    {
        CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
        collider.radius = range;
        collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to an enemy
        if (((1 << other.gameObject.layer) & enemy) != 0)
        {
            slimestats enemy = other.GetComponent<slimestats>();
            if (enemy != null && !buffedEnemies.Contains(enemy))
            {
                // Save the original values to revert them later
                enemy.originalMoveSpeed = enemy.moveSpeed;
                enemy.originalAttack = enemy.attack;

                // Apply the buff
                enemy.moveSpeed = buffedMoveSpeed;
                enemy.attack = buffedAttack;
                buffedEnemies.Add(enemy);

                Debug.Log("Buffed enemy!");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collider belongs to an enemy
        if (((1 << other.gameObject.layer) & enemy) != 0)
        {
            slimestats enemy = other.GetComponent<slimestats>();
            if (enemy != null && buffedEnemies.Contains(enemy))
            {
                // Revertbuff
                enemy.moveSpeed = enemy.originalMoveSpeed;
                enemy.attack = enemy.originalAttack;
                buffedEnemies.Remove(enemy);

                Debug.Log("Debuffed enemy!");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
