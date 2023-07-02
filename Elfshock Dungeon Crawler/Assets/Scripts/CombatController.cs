using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] float attackInterval = 1f;
    [SerializeField] float invulInterval = 1f;
    [SerializeField] float MaxHealth = 100f;
    [SerializeField] private float Damage = 25f;

    private float CurrentHealth = 0f;

    public bool enemyInRange = false;

    private float attackTimer = 0f;
    private bool canAttack = true;

    private float invulTimer = 0f;

    private AttackRangeDetector rangeDetector;
    private List<GameObject> enemiesInRange;

    private Animator animator;
    private HealthBar healthBar;

    public event Action<GameObject> OnPlayerDeath;


    void Awake()
    {
        animator = GetComponent<Animator>();
        rangeDetector = GetComponentInChildren<AttackRangeDetector>();

        healthBar = GetComponentInChildren<HealthBar>();
        CurrentHealth = MaxHealth;
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        invulTimer += Time.deltaTime;

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

            // Create a copy of the enemiesInRange list
            List<GameObject> enemiesCopy = new List<GameObject>(enemiesInRange);

            foreach (GameObject enemy in enemiesCopy)
            {
                // Perform enemy damage animation and logic for each enemy here
                //enemy.GetComponent<EnemyController>().TakeDamage(Damage);

                enemy.GetComponent<EnemyControllerSphere>().TakeDamage(Damage);
            }
            attackTimer = 0f;
            canAttack = false;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if(invulTimer >= invulInterval)
        {
            CurrentHealth -= damageAmount;
            healthBar.UpdateHealth(CurrentHealth, MaxHealth);
            invulTimer = 0;
            if (CurrentHealth <= 0)
                Die();        
        }
        
    }
    public void Die()
    {
        Debug.Log("Player Died");
        OnPlayerDeath?.Invoke(gameObject);
    }
}
