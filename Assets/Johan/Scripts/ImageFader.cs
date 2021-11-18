using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFader : MonoBehaviour
{

    public float timeToFade = 60f;
    public float delay = 10f;
    float timer = 0f;

    public Image image;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer < timeToFade + delay)
        {
            if (timer > delay)
            {
                image.color = new Color(1f, 1f, 1f, 1f - ((timer - delay) / timeToFade));
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
