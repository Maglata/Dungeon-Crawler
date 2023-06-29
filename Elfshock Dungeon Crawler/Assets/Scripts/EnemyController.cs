using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float Range = 3f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float stoppingDistance = 0.5f;

    private bool isWalking = false;
    private bool hasSight = false;

    private Animator animator;

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction towards the player
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        direction.y = 0f;

        RaycastHit hit;

        if (Physics.Raycast(transform.position + new Vector3(0f,0.5f,0f), direction, out hit, Range))
        {
            if (hit.transform.CompareTag("Player"))
            {
                float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
                Debug.Log("Distance:" + distanceToPlayer);
                if (distanceToPlayer > stoppingDistance)
                {
                    transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);

                    transform.LookAt(playerTransform);

                    hasSight = true;
                }
                else
                    hasSight = false;
            }
            else
                hasSight = false;
        }
        else
            hasSight = false;

        isWalking = hasSight;
        animator.SetBool("isWalking", isWalking);
    }
}
