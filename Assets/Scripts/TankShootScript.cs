using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TankShootScript : MonoBehaviour
{
    private static string TankBarrelTag = "TankBarrel";
    public GameObject RocketPrefab;
    public double reloadTime = 0; // in milliseconds
    private double shootTimeStamp = 0;

    // Start is called before the first frame update
    void Start()
    {
        ObjectPooler.SharedInstance.initObjectToPool(RocketPrefab, RocketPrefab.tag, true, 5);
    }

    public static double CurrentTime()//time since year 2000
    {
        DateTime epochStart = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        double currentEpochTime = (DateTime.UtcNow - epochStart).TotalMilliseconds;

        return currentEpochTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentTime() <= shootTimeStamp + reloadTime )
        {
            //Cannot shoot yet
            return;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            shootTimeStamp = CurrentTime();
            RaycastHit hit;

            // Does the ray intersect any objects excluding the player layer

            var turretPosition = transform.position;
            turretPosition.y += 1.8f;
            Vector3 rotation = transform.TransformDirection(Vector3.forward) * 1000;
            if (Physics.Raycast(turretPosition, rotation, out hit, Mathf.Infinity))
            {
                Debug.DrawRay(turretPosition, rotation, Color.yellow);
                //Debug.Log("Did Hit" + hit.distance + "   " + hit.point);
                shoot(turretPosition, rotation);
            }
            else
            {
                Debug.DrawRay(transform.position, rotation * 1000, Color.white);
                //Debug.Log("Did not Hit");
            }
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey("o"))
        {
            Quaternion target = transform.rotation * Quaternion.Euler(0, -Time.deltaTime * 200, 0);
            transform.rotation = target;
        }
        else if (Input.GetKey("p"))
        {
            Quaternion target = transform.rotation * Quaternion.Euler(0, Time.deltaTime * 200, 0);
            transform.rotation = target;
        }
    }
    void shoot(Vector3 turretPosition, Vector3 rotation)
    {
        Debug.Log("Shoot!!");
        var rocketGameObject = ObjectPooler.SharedInstance.GetPooledObject(RocketPrefab.tag);
        rocketGameObject.transform.rotation = transform.rotation;
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        var barrelPosition = new Vector3(0, 2, 3); 
        foreach(var childTransform in allChildren)
        {
            if(childTransform.tag == TankBarrelTag)
            {
                barrelPosition = childTransform.position;
            }
        }
        Debug.Log(barrelPosition);
        rocketGameObject.transform.position = barrelPosition;
        Debug.Log(rocketGameObject.transform.position + " " + rocketGameObject.transform.localPosition);
        rocketGameObject.SetActive(true);
        var rocketParticleSystem = rocketGameObject.GetComponentInChildren<ParticleSystem>();
        var rocketParticleSystemMain = rocketGameObject.GetComponentInChildren<ParticleSystem>().main;
        //rocketParticleSystem.Play();
        rocketParticleSystemMain.stopAction = ParticleSystemStopAction.Callback;
    }
}
