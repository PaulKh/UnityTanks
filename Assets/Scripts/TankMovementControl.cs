using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovementControl : MonoBehaviour
{
    Vector3 worldVelocity;

    public Rigidbody tankRigidBody;
    public bool turnStarted = false;

    public GameObject turret;


    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        worldVelocity = new Vector3(0, tankRigidBody.velocity.y, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("w"))
        {
            Vector3 tempVect = transform.forward * 10 * Time.deltaTime;
            tankRigidBody.MovePosition(transform.position + tempVect);
        }
        else if (Input.GetKey("s"))
        {
            Vector3 tempVect = -transform.forward * 10 * Time.deltaTime;
            tankRigidBody.MovePosition(transform.position + tempVect);
        }

        if (Input.GetKey("a") && !turnStarted)
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
