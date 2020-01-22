using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public float turnSpeed = 100.0f;
    public float turretTurnSpeed = 100.0f;
    public float reloadTime = 2.0f;

    public float explosionPower = 2000.0f;
    public float explosionRadius = 5.0f;

    public Rigidbody tankRigidBody;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveForward()
    {
        Vector3 tempVect = transform.forward * movementSpeed * Time.deltaTime;
        tankRigidBody.MovePosition(transform.position + tempVect);
    }

    public void moveBackward()
    {
        Vector3 tempVect = -transform.forward * movementSpeed * Time.deltaTime;
        tankRigidBody.MovePosition(transform.position + tempVect);
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
