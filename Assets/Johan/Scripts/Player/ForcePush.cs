using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePush : MonoBehaviour
{
    //public CharacterController character;
    public LayerMask pushMask;
    public new Camera camera;
    public float range = 20f;
    public float strength = 20f;

    bool isFiring = false;

    // Update is called once per frame
    void Update()
    {
        // Check if the push-button is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFiring = true;
        }

    }

    private void FixedUpdate()
    {

        RaycastHit hit;

        // Try to push something
        if (isFiring)
        {

            //character.Move(camera.transform.forward * 0.5f * -strength);

            if (Physics.Raycast(camera.transform.position, camera.transform.forward * range, out hit, range, pushMask))
            {
                try
                {
                    hit.transform.GetComponent<EnemyScript>().push(strength * 10f);
                }
                catch
                {

                }
            }
            isFiring = false;
        }

    }
}
