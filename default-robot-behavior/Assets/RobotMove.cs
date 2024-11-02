using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float gravity = -9.81f;
    public Animator animator;
    private CharacterController characterController;

    private Vector3 velocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isMovingForward", true);
            Vector3 forward = transform.forward;
            moveDirection = Quaternion.Euler(0, -90, 0) * forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isMovingForward", true);
            moveDirection = Quaternion.Euler(0, 90, 0) * transform.forward;
        }
        else
        {
            animator.SetBool("isMovingForward", false);
        }

        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        if (characterController.isGrounded)
        {
            velocity.y = 0f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(velocity * Time.deltaTime);

        float rotationInput = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            rotationInput = -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotationInput = 1f;
        }


        float rotationAmount = rotationInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotationAmount, 0);
    }
}
