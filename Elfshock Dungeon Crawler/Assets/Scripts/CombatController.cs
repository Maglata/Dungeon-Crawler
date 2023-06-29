using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] float attackInterval = 1f;
    [SerializeField] float MaxHealth = 100f;

    private float CurrentHealth = 0f;

    public bool enemyInRange = false;

    private float attackTimer = 0f;
    private bool canAttack = true;  

    private AttackRangeDetector rangeDetector;
    private List<GameObject> enemiesInRange;

    private Animator animator;
    private HealthBar healthBar;


    void Awake()
    {
        animator = GetComponent<Animator>();
        rangeDetector = GetComponentInChildren<AttackRangeDetector>();

        healthBar = GetComponentInChildren<HealthBar>();
        CurrentHealth = MaxHealth;
        healthBar.UpdateHealth(CurrentHealth, MaxHealth);
    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        // Check if enough time has passed to allow another attack
        if (attackTimer >= attackInterval)
            canAttack = true;

        if (canAttack && enemyInRange)
            Attack();
    }

    private void Attack()
    {
        enemiesInRange = rangeDetector.GetEnemiesInRange();

        if (enemiesInRange.Count > 0)
        {
            // Trigger attack animation
            animator.SetTrigger("Attack");

            foreach (GameObject enemy in enemiesInRange)
            {
                // Perform enemy damage animation and logic for each enemy here
              
            }
            attackTimer = 0f;
            canAttack = false;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        healthBar.UpdateHealth(CurrentHealth, MaxHealth);
        if(CurrentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Debug.Log("Player Died");
    }
}
