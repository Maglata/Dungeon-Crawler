using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] float attackInterval = 1f;

    public bool enemyInRange = false;

    private float attackTimer = 0f;
    private bool canAttack = true;  

    private AttackRangeDetector rangeDetector;
    private List<GameObject> enemiesInRange;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rangeDetector = GetComponentInChildren<AttackRangeDetector>();
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
            foreach (GameObject enemy in enemiesInRange)
            {
                // Perform attack animation or logic for each enemy here
                Debug.Log("Attacking enemy!");

                // Rotate towards the enemy
                Vector3 direction = enemy.transform.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.15f);

                // Trigger attack animation
                animator.SetTrigger("Attack");
            }
            attackTimer = 0f;
            canAttack = false;
        }
    }
}
