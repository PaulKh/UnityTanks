using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

public class TankShootControl : MonoBehaviour
{
    public Turret turret;

    void FixedUpdate()
    {
        if (Input.GetKey("q"))
        {
            turret.turnTurretLeft();
        }
        else if (Input.GetKey("e"))
        {
            turret.turnTurretRight();
        }
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            turret.shoot();
        }
    }
}
