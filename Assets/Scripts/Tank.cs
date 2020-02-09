using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public float maximumSpeed = 50.0f;
    public float backwardMaximumSpeed = -3.0f;
    //current speed is a number between backward and maximum forward speed
    public float currentSpeed = 0.0f;

    public float turnSpeed = 100.0f;
    public float accelerationPower = 0.05f;
    private float maximumAcceleration = 1.0f;
    private float minimumAcceleration = 0.5f;
    private float currentAcceleration = 2.0f;

    public float explosionPower = 2000.0f;
    public float explosionRadius = 5.0f;


    public Rigidbody tankRigidBody;

    private bool isMoving = false;

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
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoving && !Mathf.Approximately(currentSpeed, 0.0f))
        {
            Debug.Log(currentSpeed + " " + currentAcceleration);
            //If no force applied, stop tank by dropping the speed
            var speedAbs = Mathf.Abs(currentSpeed);
            currentSpeed = Mathf.Sign(currentSpeed) * (speedAbs - speedAbs / 50.0f);
            currentAcceleration += maximumAcceleration / 10.0f;
            currentAcceleration = Mathf.Clamp(currentAcceleration, minimumAcceleration, maximumAcceleration);
            currentSpeed = Mathf.Clamp(currentSpeed, backwardMaximumSpeed, maximumSpeed);
            if (Mathf.Abs(currentSpeed) < 0.5f)
            {
                currentSpeed = 0.0f;
                currentAcceleration = maximumAcceleration;
            }
        }
        //If current speed is not 0 we still can move
        if (!Mathf.Approximately(currentSpeed, 0.0f))
        {
            Vector3 tempVect = transform.forward * currentSpeed * Time.deltaTime;
            tankRigidBody.MovePosition(transform.position + tempVect);
        }
    }

    public void moveForward()
    {
        isMoving = true;
        //Increase reverse speed if it is negative. Cheating
        var cof = (Mathf.Approximately(Mathf.Sign(currentSpeed), -1.0f)) ? 3.0f : 1.0f;
        currentSpeed += currentAcceleration * accelerationPower * cof;
        currentAcceleration -= Time.deltaTime * accelerationPower;
        currentSpeed = Mathf.Clamp(currentSpeed, backwardMaximumSpeed, maximumSpeed);
        currentAcceleration = Mathf.Clamp(currentAcceleration, minimumAcceleration, maximumAcceleration);
        Debug.Log(currentSpeed + " " + currentAcceleration + " " + tankRigidBody.velocity);
    }

    public void moveBackward()
    {
        isMoving = true;
        //Increase reverse speed if it is positive. Cheating
        var cof = (Mathf.Approximately(Mathf.Sign(currentSpeed), 1.0f)) ? 3.0f : 1.0f;
        currentSpeed -= currentAcceleration * accelerationPower * cof;
        currentSpeed = Mathf.Clamp(currentSpeed, backwardMaximumSpeed, maximumSpeed);
        currentAcceleration -= Time.deltaTime * accelerationPower;
        currentAcceleration = Mathf.Clamp(currentAcceleration, minimumAcceleration, maximumAcceleration);
        Debug.Log(currentSpeed + " " + currentAcceleration + " " + tankRigidBody.velocity);
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
}
