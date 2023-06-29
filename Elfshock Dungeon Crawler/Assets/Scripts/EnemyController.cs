using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float Range = 3f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float stoppingDistance = 0.5f;
    [SerializeField] private float MaxHealth = 100f;
    [SerializeField] private float Damage = 45f;
    [SerializeField] float invulnarabilityInterval = 1f;


    private float CurrentHealth = 0f;
    private bool isWalking = false;
    private bool hasSight = false;
    private float invulTimer = 0f;

    private Animator animator;

    private Transform playerTransform;

    private HealthBar healthBar;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        healthBar = GetComponentInChildren<HealthBar>();
        CurrentHealth = MaxHealth;
        healthBar.UpdateHealth(CurrentHealth, MaxHealth);
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

                //Debug.Log("Distance:" + distanceToPlayer);

                if (distanceToPlayer > stoppingDistance)
                {
                    transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
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
        // Drop loot and remove
        playerTransform.GetComponentInChildren<AttackRangeDetector>().RemoveEnemy(gameObject);
        Destroy(gameObject);
    }
}
