using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeSpikeController : MonoBehaviour
{

    public ParticleSystem spikeSpawner;

    private bool isPlaying = false;

    public void startSpikes()
    {
        if (!isPlaying)
        {
            spikeSpawner.Play();
            isPlaying = true;
        }
    }

    public void stopSpikes()
    {
        if (isPlaying)
        {
            spikeSpawner.Stop();
            isPlaying = false;
        }
    }

}
