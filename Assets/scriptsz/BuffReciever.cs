using UnityEngine;


// This is Script is triggered the behaviors here when the components enters & Exits the Hidden Gem Area
// I adjust this script when there are buffed behaviors to modify. 
public class BuffReceiver : MonoBehaviour
{
    // declared for player
    private bool isBuffed = false;
    private float originalPlayerDamage;
    private float originalPlayerShootingInterval;

    // Declared for enemyTurret
    private bool isTurret = false;
    private enemyTurretBehavior enemyTurret;

    //Declared for enemy
    private bool isEnemy = false;
    private float originalDamage;
    private float originalAttackCooldown;
    
    // declared for player multiplier
    public float playerDamageMultiplier = 2f;
    public float playerShootingIntervalMultiplier = 0.5f;

    // declared for enemyturret multiplier
    public float turretDamageMultiplier = 2f;
    public float turretShootingIntervalMultiplier = 0.5f;

    // declared for Enemy multiplier (Melee)
    public float damageMultiplier = 2f;
    public float attackCooldownMultiplier = 0.5f;
    public float damageReductionFactor = 0.75f; // 25% damage reduction (tried applying the effect)
    private float originalDamageReductionFactor = 1f; // Default is no reduction

    private void Start()
    {
        //declared Original state before players enter the field.
        // Attempt to find Shooting component in children if not found directly
        if (!TryGetComponent(out Shooting playerShooting))
        {
            playerShooting = GetComponentInChildren<Shooting>();
        }

        if (playerShooting != null)
        {
            originalPlayerDamage = playerShooting.attackDamage;
            originalPlayerShootingInterval = playerShooting.timeBetweenFiring;
        }
        else if (TryGetComponent(out enemyTurretBehavior turret))
        {
            isTurret = true;
            enemyTurret = turret;
            originalPlayerDamage = turret.attackDamage;
            originalPlayerShootingInterval = turret.shootingInterval;
        }
        else if (TryGetComponent(out enemyBehavior enemy))
        {
            isEnemy = true;
            originalDamage = enemy.attackDamage;
            originalAttackCooldown = enemy.attackCooldown;
        }
    }

    //Player Buff
    public void ApplyPlayerBuff(bool apply)
    {
        if (apply && !isBuffed && !isTurret && !isEnemy)
        {
            if (TryGetComponent(out Shooting playerShooting) || (playerShooting = GetComponentInChildren<Shooting>()) != null)
            {
                playerShooting.attackDamage *= playerDamageMultiplier;
                playerShooting.timeBetweenFiring *= playerShootingIntervalMultiplier;
                isBuffed = true;
            }
        }
        else if (!apply && isBuffed && !isTurret && !isEnemy)
        {
            if (TryGetComponent(out Shooting playerShooting) || (playerShooting = GetComponentInChildren<Shooting>()) != null)
            {
                playerShooting.attackDamage = originalPlayerDamage;
                playerShooting.timeBetweenFiring = originalPlayerShootingInterval;
                isBuffed = false;
            }
        }
    }

    //Enemy Turret Buff
    public void ApplyTurretBuff(bool apply)
    {
        if (apply && !isBuffed && isTurret)
        {
            if (enemyTurret != null)
            {
                enemyTurret.attackDamage *= turretDamageMultiplier;
                enemyTurret.UpdateShootingInterval(originalPlayerShootingInterval * turretShootingIntervalMultiplier);
                isBuffed = true;
            }
        }
        else if (!apply && isBuffed && isTurret)
        {
            if (enemyTurret != null)
            {
                enemyTurret.attackDamage = originalPlayerDamage;
                enemyTurret.UpdateShootingInterval(originalPlayerShootingInterval);
                isBuffed = false;
            }
        }
    }

    //Enemy Melee Buffs
    public void ApplyEnemyBuff(bool apply)
    {
        if (isEnemy)
        {
            enemyBehavior enemy = GetComponent<enemyBehavior>();
            if (enemy != null)
            {
                if (apply && !isBuffed)
                {
                    // Apply buffs
                    enemy.attackDamage *= damageMultiplier;
                    enemy.attackCooldown *= attackCooldownMultiplier;
                    enemy.damageReductionFactor = damageReductionFactor;

                    isBuffed = true;
                }
                else if (!apply && isBuffed)
                {
                    // Revert buffs
                    enemy.attackDamage = originalDamage;
                    enemy.attackCooldown = originalAttackCooldown;
                    enemy.damageReductionFactor = originalDamageReductionFactor;

                    isBuffed = false;
                }
            }
        }
    }
}
