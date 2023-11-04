using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController controller;

    private Vector3 playerVelocity;
    public float speed = 5f;
    private bool isGrounded;
    public float gravity = -9.8f;
    private bool crouching ;
    private float crouchTimer = 1;
    private bool lerpCrouch ;
    private bool sprinting ;
    public float jumpHeight = 1.4f;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;

        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float var = crouchTimer / 1;
            var *= var;

            if (crouching)
            {
                controller.height = Mathf.Lerp(controller.height, 1, var);
            }
            else
            {
                controller.height = Mathf.Lerp(controller.height, 2, var);
            }

            if (var > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        controller.Move(transform.TransformDirection(moveDirection) * (speed * Time.deltaTime));
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move((playerVelocity * Time.deltaTime));
        
    }

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
        Debug.Log(("Crouching"));
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
        {
            speed = speed + 3;
            Debug.Log((speed));
        }
        else
        {
            speed = 5f;
            Debug.Log((speed));
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}