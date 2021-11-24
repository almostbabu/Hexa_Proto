using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeSpikeController : MonoBehaviour
{

    public ParticleSystem spikeSpawner;

    private bool isPlaying;

    [SerializeField]
    private float upperFOV = 60f, lowerFOV = 20f, raiseFOV = 2f, decreaseFOV = 2f;

    private float fovGoal;
    private float fovCurrent;

    public bool IsPlaying { get => isPlaying; set => isPlaying = value; }

    public new Camera camera;

    private void Start()
    {
        fovCurrent = upperFOV;
        fovGoal = upperFOV;
        IsPlaying = false;
        spikeSpawner.Stop();
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
            camera.fieldOfView = fovCurrent;
        }
        else if (fovCurrent > fovGoal)
        {
            fovCurrent -= decreaseFOV * Time.deltaTime;
            if (fovCurrent < fovGoal)
            {
                fovCurrent = fovGoal;
            }
            camera.fieldOfView = fovCurrent;
        }
    }

    public void startSpikes()
    {
        if (!IsPlaying)
        {
            fovGoal = lowerFOV;
            spikeSpawner.Play();
            IsPlaying = true;
        }
    }

    public void stopSpikes()
    {
        if (IsPlaying)
        {
            fovGoal = upperFOV;
            spikeSpawner.Stop();
            IsPlaying = false;
        }
    }

}
