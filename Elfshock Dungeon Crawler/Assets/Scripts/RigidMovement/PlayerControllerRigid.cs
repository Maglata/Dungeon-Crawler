using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerControllerRigid : MonoBehaviour
{
    [Header("Player Variables")]
    public float moveSpeed = 5;
    public int points;

    [Header("Camera Variables")]
    [SerializeField] private Vector3 offset;

    private bool isWalking = false;
    private Animator animator;
    private Rigidbody rb;
    private Camera cam;

    Vector3 playerInput;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        cam = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }


    private void PlayerInput()
    {
        playerInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
    }

    private void MovePlayer()
    {
        playerInput.y = 0f;
        if (playerInput != Vector3.zero)
        {
            isWalking = true;
            rb.velocity = transform.TransformDirection(playerInput) * moveSpeed;
        }
        else
        {
            isWalking = false;
            rb.velocity = Vector3.zero;
        }
        animator.SetBool("isWalking", isWalking);
        MoveCamera();
    }

    private void MoveCamera()
    {
        cam.transform.position = transform.position + offset;
    }
}
