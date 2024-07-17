using UnityEngine;

public class BuffReceiver : MonoBehaviour
{
    private bool isBuffed = false;
    private float originalPlayerDamage;
    private float originalPlayerShootingInterval;

    private bool isTurret = false;
    private enemyTurretBehavior enemyTurret;

    private bool isEnemy = false;
    private float originalDamage;
    private float originalAttackCooldown;

    public float playerDamageMultiplier = 2f;
    public float playerShootingIntervalMultiplier = 0.5f;

    public float turretDamageMultiplier = 2f;
    public float turretShootingIntervalMultiplier = 0.5f;

    public float damageMultiplier = 2f;
    public float attackCooldownMultiplier = 0.5f;

    private void Start()
    {
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

    public void ApplyEnemyBuff(bool apply)
    {
        if (apply && !isBuffed && isEnemy)
        {
            enemyBehavior enemy = GetComponent<enemyBehavior>();
            if (enemy != null)
            {
                enemy.attackDamage *= damageMultiplier;
                enemy.attackCooldown *= attackCooldownMultiplier;
                isBuffed = true;
            }
        }
        else if (!apply && isBuffed && isEnemy)
        {
            enemyBehavior enemy = GetComponent<enemyBehavior>();
            if (enemy != null)
            {
                enemy.attackDamage = originalDamage;
                enemy.attackCooldown = originalAttackCooldown;
                isBuffed = false;
            }
        }
    }
}
