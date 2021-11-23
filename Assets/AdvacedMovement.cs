using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvacedMovement : MonoBehaviour
{

    //Variables
    PlayerMovement basicMovementScript;
    public float speedBoost = 10;
    // Start is called before the first frame update
    void Start()
    {
        basicMovementScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))

            basicMovementScript.speed += speedBoost;
        else if (Input.GetKeyDown(KeyCode.LeftShift))
            basicMovementScript.speed -= speedBoost;

    }
}
