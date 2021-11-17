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
    public LineRenderer lineRenderer;

    bool isHooked = false;
    bool isFiring = false;
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
            character.Move(speed * Time.deltaTime * (hookedTo.point - transform.position));
        }
        else
        {
            isHooked = false;
            lineRenderer.enabled = false;
        }

    }

    private void FixedUpdate()
    {

        RaycastHit hit;

        // Try to grapple something
        if (isFiring && !isHooked)
        {
            
            if (Physics.Raycast(camera.transform.position, camera.transform.forward * range, out hookedTo, range, hookMask))
            {
                isHooked = true;
            }
        } 
        // Shade if hooked
        else if (isHooked)
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
            if (Physics.Raycast(camera.transform.position, camera.transform.forward * range, out hookedTo, range, hookMask))
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
            else
            {
                crosshair.GetComponent<Image>().color = Color.white;
            }
            
        }

    }

}
