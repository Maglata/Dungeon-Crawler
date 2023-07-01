using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float Range = 3f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float stoppingDistance = 0.5f;
    [SerializeField] private float MaxHealth = 100f;
    [SerializeField] private float Damage = 45f;
    [SerializeField] float invulnarabilityInterval = 1f;

    [SerializeField] private GameObject point;

    public event Action<GameObject> OnEnemyDestroyed;

    private float CurrentHealth = 0f;
    private bool isWalking = false;
    private bool hasSight = false;
    private float invulTimer = 0f;

    private NavMeshAgent agent;
    private Animator animator;
    public Transform playerTransform;
    private HealthBar healthBar;

    void Awake()
    {
        animator = GetComponent<Animator>();

        healthBar = GetComponentInChildren<HealthBar>();
        CurrentHealth = MaxHealth;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.stoppingDistance = stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        invulTimer += Time.deltaTime;

        // Calculate the direction towards the player
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        direction.y = 0f;

        RaycastHit hit;
        hasSight = false;

        if (Physics.Raycast(transform.position + new Vector3(0f, 0.5f, 0f), direction, out hit, Range))
        {
            if (hit.transform.CompareTag("Player"))
            {
                float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

                if (distanceToPlayer > stoppingDistance)
                {
                    agent.SetDestination(playerTransform.position);

                    transform.LookAt(playerTransform);

                    hasSight = true;
                    isWalking = hasSight;

                    animator.SetBool("isWalking", isWalking);
                    return;
                }
                animator.SetTrigger("Attack");
                playerTransform.GetComponent<CombatController>().TakeDamage(Damage);
            }
        }
        isWalking = hasSight;
        animator.SetBool("isWalking", isWalking);
    }
    public void TakeDamage(float damageAmount)
    {
        if (invulTimer >= invulnarabilityInterval)
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
        Vector3 pointPosition = transform.position;
        pointPosition.y = 0.5f;
        Instantiate(point, pointPosition, Quaternion.identity);

        playerTransform.GetComponentInChildren<AttackRangeDetector>().RemoveEnemy(gameObject);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnEnemyDestroyed?.Invoke(gameObject);
    }

}
