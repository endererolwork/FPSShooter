using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    private Vector3 playerVelocity;
    public float speed = 5f;
    private bool isGrounded;
    public float gravity = -9.8f;
    private bool crouching;
    private float crouchTimer = 1;
    private bool lerpCrouch;
    private bool sprinting;
    public float sprintSpeed = 8f;
    public float jumpHeight = 1.4f;
    public Transform gunBarrel;

    public Enemy enemy;
    private PlayerUI playerUI;

    public GameObject TalentUI;
    private LevelSystem levelSystem;

    public void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerUI = GetComponent<PlayerUI>();
        levelSystem = GetComponent<LevelSystem>();
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
            speed = sprintSpeed;
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

    public void Shoot()
    {
        RaycastHit hit;
        
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance,
                Color.yellow);
            Debug.Log("Did Hit");


            if (hit.collider.CompareTag("Enemy"))
            {
                Enemy enemy = hit.collider.GetComponentInParent<Enemy>();
                enemy.EnemyDie();
                GetComponent<PlayerAttack>().DecrementAmmo();
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    public void PierceShot()
    {
        if (levelSystem.level < 3)
        {
            return; // Check level 
        }
        RaycastHit[] hits;
        float rayDistance = 100f;
        Vector3 direction = transform.forward;

        hits = Physics.RaycastAll(transform.position, direction, rayDistance);

        Debug.DrawRay(transform.position, direction * rayDistance, Color.cyan, 1f);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Enemy enemy = hit.collider.GetComponentInParent<Enemy>();
                if (enemy != null)
                {
                    enemy.EnemyDie();
                    GetComponent<PlayerAttack>().DecrementPierceShot();
                }
            }
        }
    }

    public void TalentSystem()
    {
        TalentUI.GameObject().SetActive(true);
    }

    public void IncreaseJumpHeight()
    {
        jumpHeight = jumpHeight + 0.2f;
    }

    public void IncreaseSprintSpeed()
    {
        sprintSpeed++;
    }

    public void IncreaseMovementSpeed()
    {
        speed++;
    }
}