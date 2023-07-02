using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CombatController : MonoBehaviour
{
    [SerializeField] float attackInterval = 1f;
    [SerializeField] float invulInterval = 1f;
    [SerializeField] float MaxHealth = 100f;
    [SerializeField] private float Damage = 25f;

    [SerializeField] float attackRange = 1f;
    [SerializeField] float attackRadius = 1f;

    [SerializeField] LayerMask enemyMask;

    private float CurrentHealth = 0f;

    private float attackTimer = 0f;
    private bool canAttack = true;

    private float invulTimer = 0f;

    private Animator animator;
    private HealthBar healthBar;

    public event Action<GameObject> OnPlayerDeath;

    private RaycastHit hit;


    void Awake()
    {
        animator = GetComponent<Animator>();

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
    }

    private void FixedUpdate()
    {
        bool enemyinrange = Physics.SphereCast(transform.position, attackRadius,transform.forward,out hit, attackRange, enemyMask);

        if (enemyinrange && canAttack)
        {
            canAttack = false;
            attackTimer = 0f;
            animator.SetTrigger("Attack");
        }
    }

    public void Attack()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, attackRadius, transform.forward, attackRange, enemyMask);
        
        if(hits.Length != 0)
            for (int i = 0; i < hits.Length; i++)
            {
                hits[i].collider.GetComponent<EnemyControllerSphere>().TakeDamage(Damage);
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
