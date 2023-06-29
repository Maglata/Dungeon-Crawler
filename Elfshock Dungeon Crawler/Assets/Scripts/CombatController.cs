using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] float attackInterval = 1f;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Attack", attackInterval, attackInterval);
        animator = GetComponent<Animator>();
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
