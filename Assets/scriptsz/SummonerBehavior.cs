using UnityEngine;

public class SummonerBehavior : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health of the player
    public float currentHealth; // Current health of the player
    public float attackDamage = 10f; // Amount of damage the player deals per attack
    public float attackRange = 1.5f; // Range within which the player can attack
    public Transform attackPoint; // Point where the attack originates (e.g., player's weapon)
    public LayerMask enemyLayer; // Layer that contains enemies

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to max health
    }

    void Update()
    {
        
    }

    // UNDER WORK. DIDNT MANAGE THIS YET, 
    /*void Attack()
    {
        // Example: Perform attack logic
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<enemyBehavior>().TakeHit(attackDamage);
        }
    }*/

    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // Reduce current health by the amount of damage

        // Check if player health is zero or below
        if (currentHealth <= 0)
        {
            Die(); // Call the Die() method if health is depleted
        }
    }

    void Die()
    {
        
    }
}
