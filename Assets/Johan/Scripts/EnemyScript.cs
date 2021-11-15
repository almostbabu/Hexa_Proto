using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public CharacterController character;

    public Transform groundcheck;
    float groundDistance = 0.4f;
    public LayerMask groundMask;
    public LayerMask deadMask;

    public float speed = 6f;
    public float gravity = -20f;

    Vector3 velocity;
    bool isGrounded;

    public Transform player;
    public float range;

    // Update is called once per frame
    void Update()
    {
        
        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Vector3.Distance(player.position, transform.position) < range)
        {
            float x = player.position.x - transform.position.x;
            float z = player.position.z - transform.position.z;

            Vector3 move = transform.right * x + transform.forward * z;
            move = move.normalized;

            character.Move(move * speed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;

        character.Move(velocity * Time.deltaTime);

        if (Physics.CheckSphere(groundcheck.position, groundDistance, deadMask))
        {
            Destroy(gameObject);
        }

    }

}
