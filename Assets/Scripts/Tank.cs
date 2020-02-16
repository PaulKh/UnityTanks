using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public float maximumSpeed = 50.0f;
    public float backwardMaximumSpeed = -3.0f;
    //current speed is a number between backward and maximum forward speed

    public float turnSpeed = 100.0f;
    public float accelerationPower = 0.05f;
    private float maximumAcceleration = 2.0f;
    private float minimumAcceleration = -1.5f;
    private float currentAcceleration = 2.0f;

    public float explosionPower = 2000.0f;
    public float explosionRadius = 5.0f;


    public Rigidbody tankRigidBody;
    public Turret turret;

    public Transform groundTouchPosition;

    private bool isForceApplied = false;

    private void Awake()
    {
        currentAcceleration = maximumAcceleration;
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    void FixedUpdate()
    {
        isForceApplied = false;
    }

    // Update is called once per frame
    void Update()
    {
        //If current speed is not 0 we still can move
        var speed = tankRigidBody.velocity.magnitude;
        if (isForceApplied && (speed < maximumSpeed) && isOnTheGround())
        {
            Vector3 tempVect = transform.forward * currentAcceleration * Time.deltaTime * 500;
            tankRigidBody.AddForce(tempVect, ForceMode.Acceleration);
        }
    }

    public void moveForward()
    {
        isForceApplied = true;
        //Increase reverse speed if it is negative.
        currentAcceleration = maximumAcceleration;
    }

    public void moveBackward()
    {
        isForceApplied = true;
        //Increase reverse speed if it is positive.
        currentAcceleration = minimumAcceleration;
    }

    //Move based on current acceleration
    private void move()
    {
        
    }
    public void turnRight()
    {
        Quaternion target = transform.rotation * Quaternion.Euler(0, Time.deltaTime * turnSpeed, 0);
        transform.rotation = target;
    }

    public void turnLeft()
    {
        Quaternion target = transform.rotation * Quaternion.Euler(0, -Time.deltaTime * turnSpeed, 0);
        transform.rotation = target;
    }
    private bool isOnTheGround()
    {
        float radius = 0.5f;
        Collider[] colliders = Physics.OverlapSphere(groundTouchPosition.position, radius);
        foreach (Collider hit in colliders)
        {
            if (hit.tag == "Ground")
            {
                return true;
            }
        }
        return false;
    }
}
