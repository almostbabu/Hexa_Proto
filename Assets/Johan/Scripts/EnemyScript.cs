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

    float pushForce = 0f;
    public const float PUSH_DECAY = 0.2f;

    Vector3 direction;
    Quaternion lookRotation;
    public float rotationSpeed = 6f;
    public Transform slime;

    // Update is called once per frame
    void Update()
    {
        
        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Look at player
        direction = (player.position - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(direction);
        slime.rotation = Quaternion.Slerp(slime.rotation, lookRotation, Time.deltaTime * rotationSpeed);

        // The enemy has been pushed
        if (pushForce > 0.01f)
        {
            //Debug.Log(pushForce);
            float x = player.position.x - transform.position.x;
            float y = player.position.y - transform.position.y;
            float z = player.position.z - transform.position.z;

            Vector3 move = transform.right * x + transform.forward * z + transform.up * y;
            move = move.normalized;

            character.Move(move * -pushForce);
            pushForce -= PUSH_DECAY * Time.deltaTime;
        }
        // The enemy is recovering from the push
        else if (pushForce > 0f)
        {
            if (Vector3.Distance(player.position, transform.position) < range)
            {
                float x = player.position.x - transform.position.x;
                float z = player.position.z - transform.position.z;

                Vector3 move = transform.right * x + transform.forward * z;
                move = move.normalized;

                character.Move(move * speed * 0.1f * Time.deltaTime);
                pushForce -= PUSH_DECAY * Time.deltaTime * 0.1f;
            }
        }
        // Regular state (full walk-speed)
        else
        {
            if (Vector3.Distance(player.position, transform.position) < range)
            {
                float x = player.position.x - transform.position.x;
                float z = player.position.z - transform.position.z;

                Vector3 move = transform.right * x + transform.forward * z;
                move = move.normalized;

                character.Move(move * speed * Time.deltaTime);
            }
        }

        velocity.y += gravity * Time.deltaTime;

        character.Move(velocity * Time.deltaTime);

        if (Physics.CheckSphere(groundcheck.position, groundDistance, deadMask))
        {
            Destroy(gameObject);
        }

    }

    public void push(float strength)
    {
        // Soft-delay between pushes
        if (pushForce <= 0f)
        {
            pushForce = strength;
        }
    }


}
