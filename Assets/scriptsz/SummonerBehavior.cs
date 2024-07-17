using UnityEngine;

public class SummonerBehavior : MonoBehaviour
{
    //Player Health Mechanics & TakeHit

    [SerializeField] HealthbarBehavior Healthbar;
    public float health, maxHealth;
    Rigidbody2D rb;

    void Start()
    {
        health = maxHealth;
        Healthbar.UpdateHealthBar(health, maxHealth);
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Healthbar = GetComponentInChildren<HealthbarBehavior>();
    }

    void Update() 
    {
        
    }

    public void TakeHit(float damage)
    {
        health -= damage;
        Healthbar.UpdateHealthBar(health, maxHealth);
        if (health <= 0) 
        { 
                Die(); 
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
