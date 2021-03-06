using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController character;

    public Transform groundcheck;
    public Transform enemycheck;
    public Transform respawn;
    float groundDistance = 0.4f;
    float enemyDistance = 0.8f;
    public LayerMask groundMask;
    public LayerMask deadMask;
    public LayerMask enemyMask;

    [HideInInspector]
    public Vector3 moveDirection = Vector3.zero;

    public float speed = 6f;
    public float sprintModifier = 3f;
    public float gravity = -20f;
    public float jumpHeight = 20f;

    Vector3 velocity;
    bool isGrounded;
    bool isSprinting;
    bool isForcingJump = false;
    float jumpModifier = 1f;

    public AnimeSpikeController asc;
    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // Sprint
        if (isGrounded && Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else if (!isGrounded && isSprinting)
        {
            isSprinting = true;
            
        }
        else
        {
            isSprinting = false;
            asc.stopSpikes();
        }

        if (isSprinting)
        {
            character.Move(move * speed * sprintModifier * Time.deltaTime);
            asc.startSpikes();
        }
        else
        {
            character.Move(move * speed * Time.deltaTime);
        }

        // Jumping
        if ((Input.GetButtonDown("Jump") || isForcingJump) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity * jumpModifier);
            jumpModifier = 1f;
            isForcingJump = false;
        }
        
        velocity.y += gravity * Time.deltaTime;

        character.Move(velocity * Time.deltaTime);
        
        // Respawning
        if(Physics.CheckSphere(groundcheck.position, groundDistance, deadMask))
        {
            transform.position = respawn.position;
        }

        if (Physics.CheckSphere(enemycheck.position, enemyDistance, enemyMask))
        {
            transform.position = respawn.position;
        }

        moveDirection = move;
        moveDirection.y = velocity.y;

    }

    public void forceJump(float heightModifier)
    {
        isForcingJump = true;
        jumpModifier = heightModifier;
    }

}
