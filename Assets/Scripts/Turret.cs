using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Turret : MonoBehaviour
{
    private static string TankBarrelTag = "TankBarrel";
    public float turretTurnSpeed = 100.0f;
    public float reloadTime = 2000.0f;

    private double shootTimeStamp = 0;
    public GameObject RocketPrefab;



    void Start()
    {
        ObjectPooler.SharedInstance.initObjectToPool(RocketPrefab, RocketPrefab.tag, true, 5);
    }

    public void shoot()
    {
        if (Utils.CurrentTime() <= shootTimeStamp + reloadTime)
        {
            //Cannot shoot yet
            return;
        }
        shootTimeStamp = Utils.CurrentTime();
        var turretPosition = transform.position;
        turretPosition.y += 1.8f;
        Vector3 rotation = transform.TransformDirection(Vector3.forward) * 1000;
        shoot(turretPosition, rotation);

    }

    void raycast()
    {
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

    public void turnTurretLeft()
    {

        Quaternion target = transform.rotation * Quaternion.Euler(0, -Time.deltaTime * turretTurnSpeed, 0);
        transform.rotation = target;
    }

    public void turnTurretRight()
    {
        Quaternion target = transform.rotation * Quaternion.Euler(0, Time.deltaTime * turretTurnSpeed, 0);
        transform.rotation = target;
    }

    void shoot(Vector3 turretPosition, Vector3 rotation)
    {
        Debug.Log("Shoot!!");
        var rocketGameObject = ObjectPooler.SharedInstance.GetPooledObject(RocketPrefab.tag);

        rocketGameObject.transform.rotation = transform.rotation;
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        var barrelPosition = new Vector3(0, 2, 3);
        foreach (var childTransform in allChildren)
        {
            if (childTransform.tag == TankBarrelTag)
            {
                barrelPosition = childTransform.position;
            }
        }
        var rocketScript = rocketGameObject.GetComponent<Rocket>();
        rocketScript.setStartPosition(barrelPosition);


        rocketGameObject.transform.position = barrelPosition;
        Debug.Log(rocketGameObject.transform.position + " " + rocketGameObject.transform.localPosition);
        rocketGameObject.SetActive(true);
    }
}
