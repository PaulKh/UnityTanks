using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovementControl : MonoBehaviour
{
    Vector3 zeroVelocity = new Vector3(0, 0, 0);
    public Rigidbody tankRigidBody;
    public bool turnStarted = false;

    public GameObject turret;


    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tankRigidBody.velocity = zeroVelocity;
        tankRigidBody.angularVelocity = zeroVelocity;

        if (Input.GetKey("w"))
        {
            tankRigidBody.velocity = transform.forward * Time.deltaTime * 1000;
        }
        else if (Input.GetKey("s"))
        {
            tankRigidBody.velocity = -transform.forward * Time.deltaTime * 1000;
        }
        else if(Input.GetKey("a") && !turnStarted)
        {
            animator.SetTrigger("turnLeftTrigger");
            turnStarted = true;
        }
        else if (Input.GetKey("d") && !turnStarted)
        {
            animator.SetTrigger("turnRightTrigger");
            turnStarted = true;
        }
        if (Input.GetKey("o"))
        { 
            Quaternion target = turret.transform.rotation * Quaternion.Euler(0, 0, -Time.deltaTime * 200);
            turret.transform.rotation = target;
        }
        else if (Input.GetKey("p"))
        {
            Quaternion target = turret.transform.rotation * Quaternion.Euler(0, 0, Time.deltaTime * 200);
            turret.transform.rotation = target;
        }
    }

    public void TurnAnimationFinished()
    {
        turnStarted = false;
    }
}
