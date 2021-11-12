using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleShader : MonoBehaviour
{

    public Material regularMaterial;
    public Material shadedMaterial;
    public bool remainShaded = false;
    public const float REVERT_DELAY = 0.1f;

    private float remainingTime = 0f;

    // Update is called once per frame
    void Update()
    {

        // Count down the timer
        if (remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;
        }

        // Check if the shading should revert back
        if ((remainingTime < 0f) && !remainShaded)
        {
            remainingTime = 0f;
            //https://forum.unity.com/threads/how-to-change-the-material-of-certain-element-in-mesh-renderer.334089/
            Material[] mats = transform.GetComponent<MeshRenderer>().materials;
            mats[0] = regularMaterial;
            transform.GetComponent<MeshRenderer>().materials = mats;
        }
        else if (remainingTime < 0f)
        {
            remainingTime = 0f;
        }

    }

    // This function is called from the raycast
    public void Shade()
    {
        remainingTime = REVERT_DELAY;
        //https://forum.unity.com/threads/how-to-change-the-material-of-certain-element-in-mesh-renderer.334089/
        Material[] mats = transform.GetComponent<MeshRenderer>().materials;
        mats[0] = shadedMaterial;
        transform.GetComponent<MeshRenderer>().materials = mats;
    }

}
