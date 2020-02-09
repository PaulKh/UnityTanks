using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovementControl : MonoBehaviour
{
    Vector3 worldVelocity;

    public Rigidbody tankRigidBody;
    public bool turnStarted = false;
    public CameraScript cameraScript;

    public Tank tank;

    void Start()
    {
        worldVelocity = new Vector3(0, tankRigidBody.velocity.y, 0);
    }

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

    public void TurnAnimationFinished()
    {
        turnStarted = false;
    }
}
