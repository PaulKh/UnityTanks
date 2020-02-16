using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovementControl : MonoBehaviour
{

    public Tank tank;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("w"))
        {
            tank.moveForward();
        }
        else if (Input.GetKey("s"))
        {
            tank.moveBackward();
        }

        if (Input.GetKey("a"))
        {
            tank.turnLeft();
        }
        else if (Input.GetKey("d"))
        {
            tank.turnRight();
        }


    }
}
