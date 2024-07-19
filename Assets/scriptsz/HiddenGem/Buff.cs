using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenGemBuff : MonoBehaviour
{
    public float range = 5f, originalMoveSpeed, originalAttack, originalMaxHealth, originalHealth;

    public GameObject hiddenGem;
    slimestats statistics;
    //private List<BuffReceiver> buffedObjects = new List<BuffReceiver>();

    void Awake()
    {
        //manually set buff radius position cause i cant parent it to hiddenGem
        transform.position = new Vector3(hiddenGem.transform.position.x, hiddenGem.transform.position.y + 2, 0);
    }
    // Triggers when components Enter the Field
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            GameObject enemy = collider.gameObject;
            statistics = enemy.GetComponent<slimestats>();

            //store all unchanged stats
            originalAttack = statistics.attack;
            originalMaxHealth = statistics.maxHealth;
            originalHealth = statistics.health;
            originalMoveSpeed = statistics.moveSpeed;

            //buffing stats
            statistics.attack = 2f;
            statistics.maxHealth = 15f;
            statistics.health += 5;
            statistics.moveSpeed = 4;

            Debug.Log("IM BUFFING THIS DUDE");
        }
    }
    // Triggers when components Exit the field (goes back to original values) 
    private void OnTriggerExit2D(Collider2D collider)
    {
        
    }

    // This is just a radius indicator that can be adjusted and also for the range of Hidden Gem. 
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range); // Visualize the range in the editor
    }
}
