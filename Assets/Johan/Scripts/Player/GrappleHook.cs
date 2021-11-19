using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrappleHook : MonoBehaviour
{

    public CharacterController character;
    public LayerMask hookMask;
    public new Camera camera;
    public Color shadedColor;
    public GameObject crosshair;
    public float range = 20f;
    public float speed = 6f;
    public float minFlyRange = 3f;
    public float minHookRange = 10f;
    public LineRenderer lineRenderer;
    public PlayerMovement playerMovement;
    public float jumpModifier = 0.5f;

    public float TIME_TO_HOOK = 3f;

    bool isHooked = false;
    bool isFiring = false;
    float hookTimer = 0f;
    RaycastHit hookedTo;

    // Update is called once per frame
    void Update()
    {
        // Check if the grappling-button is held down
        if (Input.GetKey("mouse 0"))
        {
            isFiring = true;
        }
        else
        {
            isFiring = false;
        }

        // If the player is grappled, drag them closer
        if(isHooked && isFiring)
        {
            if(Vector3.Distance(character.transform.position, hookedTo.point) < minFlyRange)
            {
                isHooked = false;
                hookTimer = 0f;
                lineRenderer.enabled = false;
            }
            else
            {
                character.Move(speed * Time.deltaTime * (hookedTo.point - transform.position));
            }
        }
        else if (!isFiring)
        {
            isHooked = false;
            hookTimer = 0f;
            lineRenderer.enabled = false;
        }

        if(hookTimer == 0f)
        {
            lineRenderer.enabled = false;
        }

    }

    private void FixedUpdate()
    {

        RaycastHit hit;

        // Try to grapple something
        if (isFiring && !isHooked)
        {
            
            if (Physics.Raycast(camera.transform.position, camera.transform.forward * range, out hookedTo, range))
            {
                if (!(Vector3.Distance(character.transform.position, hookedTo.point) < minHookRange) && 
                     hookMask == (hookMask | (1 << hookedTo.transform.gameObject.layer)))
                {
                    hookTimer += 0.1f;
                    if (hookTimer > TIME_TO_HOOK)
                    {
                        isHooked = true;
                        playerMovement.forceJump(jumpModifier);
                    }
                }
                else
                {
                    hookTimer = 0f;
                }
            }
            else
            {
                hookTimer = 0f;
            }

        } 
        
        // Shade if hooked
        if (isHooked)
        {
            try
            {
                hookedTo.transform.GetComponent<GrappleShader>().Shade();
                crosshair.GetComponent<Image>().color = shadedColor;
                lineRenderer.enabled = true;
                lineRenderer.startWidth = 0.3f;
                lineRenderer.endWidth = 0.3f;
                lineRenderer.SetPosition(0, transform.position - new Vector3(0f, 1f, 0f));
                lineRenderer.SetPosition(1, hookedTo.point);
            }
            catch
            {

            }
            
        }
        // Shade the crosshair when hovering over something
        else
        {
            if (Physics.Raycast(camera.transform.position, camera.transform.forward * range, out hookedTo, range))
            {
                if (hookMask == (hookMask | (1 << hookedTo.transform.gameObject.layer)))
                {
                    try
                    {
                        hookedTo.transform.GetComponent<GrappleShader>().Shade();
                        crosshair.GetComponent<Image>().color = shadedColor;
                        lineRenderer.enabled = true;
                        lineRenderer.startWidth = 0.3f;
                        lineRenderer.endWidth = 0.3f;
                        lineRenderer.SetPosition(0, transform.position - new Vector3(0f, 1f, 0f));
                        lineRenderer.SetPosition(1, Vector3.Lerp(transform.position - new Vector3(0f, 1f, 0f), hookedTo.point, hookTimer / TIME_TO_HOOK));
                    }
                    catch
                    {

                    }
                }
                else
                {
                    crosshair.GetComponent<Image>().color = Color.white;
                }
            } 
            else
            {
                crosshair.GetComponent<Image>().color = Color.white;
            }
            
        }

    }

}
