using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenGem1 : MonoBehaviour
{
    public float range = 5f; // Range of the buff effect
    public LayerMask targetLayer; // Layer to detect player and enemies. Anyways I set this up with everything nlng Lol

    private List<BuffReceiver> buffedObjects = new List<BuffReceiver>();

    // Triggers when components Enter the Field
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
    // Triggers when components Exit the field (goes back to original values) 
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

    // This is just a radius indicator that can be adjusted and also for the range of Hidden Gem. 
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
