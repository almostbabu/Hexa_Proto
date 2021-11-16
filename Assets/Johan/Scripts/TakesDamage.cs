using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakesDamage : MonoBehaviour
{

    public float hp = 20f;

    // Standard health-stuff
    public void takeDamage()
    {
        hp -= 5f * Time.deltaTime;
        if (hp < 0f)
        {
            Destroy(gameObject);
        }
    }
}
