using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeSpikeController : MonoBehaviour
{

    public ParticleSystem spikeSpawner;

    private bool isPlaying = false;

    [SerializeField]
    private float upperFOV = 60f, lowerFOV = 20f, raiseFOV = 2f, decreaseFOV = 2f;

    private float fovGoal;
    private float fovCurrent;

    private void Start()
    {

        fovCurrent = upperFOV;
        fovGoal = upperFOV;
        
        try
        {
            Camera.main.fieldOfView = fovCurrent;
            spikeSpawner.Stop();
        }
        catch
        {

        }
    }

    private void Update()
    {
        if (fovCurrent < fovGoal)
        {
            fovCurrent += raiseFOV * Time.deltaTime;
            if (fovCurrent > fovGoal)
            {
                fovCurrent = fovGoal;
            }
            Camera.main.fieldOfView = fovCurrent;
        }
        else if (fovCurrent > fovGoal)
        {
            fovCurrent -= decreaseFOV * Time.deltaTime;
            if (fovCurrent < fovGoal)
            {
                fovCurrent = fovGoal;
            }
            Camera.main.fieldOfView = fovCurrent;
        }
    }

    public void startSpikes()
    {
        if (!isPlaying)
        {
            fovGoal = lowerFOV;
            spikeSpawner.Play();
            isPlaying = true;
        }
    }

    public void stopSpikes()
    {
        if (isPlaying)
        {
            fovGoal = upperFOV;
            spikeSpawner.Stop();
            isPlaying = false;
        }
    }

}
