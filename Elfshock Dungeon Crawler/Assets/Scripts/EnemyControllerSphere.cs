using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerSphere : MonoBehaviour
{
    [SerializeField] private float viewRadius = 3f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float stoppingDistance = 0.5f;
    [SerializeField] private float MaxHealth = 100f;
    [SerializeField] private float Damage = 45f;
    [SerializeField] float invulnarabilityInterval = 1f;
    [SerializeField] float attackInterval = 1.5f;

    [SerializeField] private GameObject point;
    [SerializeField] private LayerMask playerMask;

    public event Action<GameObject> OnEnemyDestroyed;

    private float CurrentHealth = 0f;
    private bool isWalking = false;
    private float invulTimer = 0f;
    private float attackTimer = 0f;

    private float distanceToPlayer;

    private NavMeshAgent agent;
    private Animator animator;
    private HealthBar healthBar;

    void Awake()
    {
        animator = GetComponent<Animator>();

        healthBar = GetComponentInChildren<HealthBar>();
        CurrentHealth = MaxHealth;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        invulTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        //RaycastHit hit;
        //if (Physics.SphereCast(transform.position, viewRadius, transform.forward, out hit, 0.01f, playerMask))
        //{
        //    MoveToPlayer(hit.transform);
        //    Debug.DrawLine(transform.position, hit.transform.position, Color.red);
        //}

        Collider[] colliders = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        if (colliders.Length != 0)
        {    
            MoveToPlayer(colliders[0].transform);
        }
        else
        {
            isWalking = false;
            agent.isStopped = true;
        }


        animator.SetBool("isWalking", isWalking);
    }

    private void MoveToPlayer(Transform playerTransform)
    {       

        agent.SetDestination(playerTransform.position);
        transform.LookAt(playerTransform);
        agent.isStopped = false;

        isWalking = true;

        distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        //Debug.Log(distanceToPlayer);

        if (distanceToPlayer <= stoppingDistance)
        {
            //Debug.Log("Reached Player");
            isWalking = false;
            agent.isStopped = true;
            if(attackTimer >= attackInterval)
            {
                attackTimer = 0;
                animator.SetTrigger("Attack");
            }       
        }   
    }

    public void Attack()
    {
        // Check if at the moment of the attack the player is still there
        Collider[] colliders = Physics.OverlapSphere(transform.position, viewRadius / 2, playerMask);

        if (colliders.Length != 0)
            colliders[0].transform.GetComponent<CombatController>().TakeDamage(Damage);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a wire sphere around the enemy
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
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
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        OnEnemyDestroyed?.Invoke(gameObject);
    }

}
