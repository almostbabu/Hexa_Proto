using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// I don't know if anyone will read this, but MAH means WHAT in Sami iirc, lol!
// For reference, "mah sakan" can be translated as "what's up".
public class MAH_LAZOR : MonoBehaviour
{

    //public CharacterController character;
    public LayerMask destroyMask;
    public new Camera camera;
    public Transform laserOrigin;
    //public Color shadedColor;
    //public GameObject crosshair;
    public float range = 20f;
    //public float speed = 6f;
    public LineRenderer lineRenderer;

    bool isFiring = false;

    // Update is called once per frame
    void Update()
    {
        // Check if the shooting-button is held down
        if (Input.GetKey("mouse 1"))
        {
            isFiring = true;
        }
        else
        {
            isFiring = false;
            lineRenderer.enabled = false;
        }
    }

    private void FixedUpdate()
    {

        RaycastHit hit;

        // Try to grapple something
        if (isFiring)
        {

            if (Physics.Raycast(camera.transform.position, camera.transform.forward * range, out hit, range, destroyMask))
            {
                try
                {
                    //hit.transform.GetComponent<GrappleShader>().Shade();
                    //crosshair.GetComponent<Image>().color = shadedColor;
                    lineRenderer.enabled = true;
                    lineRenderer.startWidth = 0.3f;
                    lineRenderer.endWidth = 0.3f;
                    lineRenderer.SetPosition(0, laserOrigin.position);
                    lineRenderer.SetPosition(1, hit.point);

                    hit.transform.GetComponent<TakesDamage>().takeDamage();
                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    //hit.transform.GetComponent<GrappleShader>().Shade();
                    //crosshair.GetComponent<Image>().color = shadedColor;
                    lineRenderer.enabled = true;
                    lineRenderer.startWidth = 0.3f;
                    lineRenderer.endWidth = 0.3f;
                    lineRenderer.SetPosition(0, laserOrigin.position);
                    lineRenderer.SetPosition(1, camera.transform.position + camera.transform.forward * range);
                }
                catch
                {

                }
            }
        }

    }

}
