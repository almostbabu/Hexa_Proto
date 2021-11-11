using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{

    public CharacterController character;
    public LayerMask hookMask;
    public Camera camera;
    public float range = 20f;
    public float speed = 6f;

    bool isHooked = false;
    bool isFiring = false;
    Transform hookedTo;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("mouse 0"))
        {
            isFiring = true;
        }
        else
        {
            isFiring = false;
        }

        if(isHooked && isFiring)
        {
            character.Move(speed * Time.deltaTime * (hookedTo.position - transform.position));
        }
        else
        {
            isHooked = false;
        }

    }

    private void FixedUpdate()
    {
        if (isFiring && !isHooked)
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position, camera.transform.forward * range, out hit, range, hookMask))
            {
                hookedTo = hit.transform;
                isHooked = true;
            }
        }
    }

}
