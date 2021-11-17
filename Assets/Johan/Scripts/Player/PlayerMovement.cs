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


    public float speed = 6f;
    public float sprintModifier = 3f;
    public float gravity = -20f;
    public float jumpHeight = 20f;

    Vector3 velocity;
    bool isGrounded;
    bool isSprinting;
    
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
        }

        if (isSprinting)
        {
            character.Move(move * speed * sprintModifier * Time.deltaTime);
        }
        else
        {
            character.Move(move * speed * Time.deltaTime);
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        velocity.y += gravity * Time.deltaTime;

        character.Move(velocity * Time.deltaTime);
        
        if(Physics.CheckSphere(groundcheck.position, groundDistance, deadMask))
        {
            transform.position = respawn.position;
        }

        if (Physics.CheckSphere(enemycheck.position, enemyDistance, enemyMask))
        {
            transform.position = respawn.position;
        }

    }

}
