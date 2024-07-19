using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyChase : MonoBehaviour
{
    //entire chase AI (aka melee only enemies)

    public float distance;
    public GameObject player;

    playerStats health;
    slimestats slime;

    public bool giveDamageAgain = true, takeDamageAgain = true;

    //getting relevant values outside this script (playerStats, slimeStats, and the summoner herself)
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = player.GetComponent<playerStats>();
        slime = GetComponentInChildren<slimestats>();
    }
    
    //basic code to follow player
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, slime.moveSpeed * Time.deltaTime);
    }

    //handles collision
    void OnTriggerStay2D(Collider2D collision)
    {
        if(giveDamageAgain == true && collision.gameObject.tag.Equals("Player"))
        {
            giveDamageAgain = false;
            StartCoroutine(GivingDamage());
        }
        if (takeDamageAgain == true && collision.gameObject.tag.Equals("PlayerBullet"))
        {
            takeDamageAgain = false;
            StartCoroutine(TakingDamage());
        }
    }

    //so the damage is properly given and you or the slime dont instantly die
    IEnumerator GivingDamage()
    {
        health.health--;
        yield return new WaitForSeconds(1f);
        giveDamageAgain = true;
    }

    IEnumerator TakingDamage()
    {
        slime.health--;
        takeDamageAgain = true;
        yield return null;
    }
}
