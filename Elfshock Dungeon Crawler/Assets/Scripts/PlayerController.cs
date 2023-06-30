using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5;

    public int points;

    Vector3 playerInput;
    private bool isWalking = false;

    private Animator animator;
    private CharacterController Controller;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        Controller = GetComponent<CharacterController>();
        Camera.main.GetComponent<CameraControl>().target = gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();    
    }


    private void PlayerInput()
    {
        playerInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        MovePlayer();
    }

    private void MovePlayer()
    {
        if(playerInput != Vector3.zero)
        {
            isWalking = true;
            Vector3 moveVector = transform.TransformDirection(playerInput);

            Controller.Move(moveSpeed * Time.deltaTime * moveVector);
        }
        else
        {
            isWalking = false;
        }
        animator.SetBool("isWalking", isWalking);
    }
}
