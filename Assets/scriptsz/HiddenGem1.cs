using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenGem1 : MonoBehaviour
{
    public float range = 5f; // Range of the buff effect
    public LayerMask targetLayer; // Layer to detect player and enemies

    private List<BuffReceiver> buffedObjects = new List<BuffReceiver>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        BuffReceiver buffReceiver = other.GetComponent<BuffReceiver>();
        if (buffReceiver != null)
        {
            buffReceiver.ApplyPlayerBuff(true);
            buffReceiver.ApplyTurretBuff(true);
            buffReceiver.ApplyEnemyBuff(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        BuffReceiver buffReceiver = other.GetComponent<BuffReceiver>();
        if (buffReceiver != null)
        {
            buffReceiver.ApplyPlayerBuff(false);
            buffReceiver.ApplyTurretBuff(false);
            buffReceiver.ApplyEnemyBuff(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range); // Visualize the range in the editor
    }

    private void Awake()
    {
        CircleCollider2D collider = gameObject.AddComponent<CircleCollider2D>();
        collider.radius = range;
        collider.isTrigger = true;
    }
}
