using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Tank tank;
    public Turret tankTurret;

    private Tank playerTank;
    // Start is called before the first frame update
    void Start()
    {
        var playerTankGameObject = GameObject.Find("PlayerTank");
        playerTank = playerTankGameObject.GetComponent<Tank>();
    }

    void FixedUpdate()
    {
        var angleBeforePerfectAim = angleBetweenTurretAndPlayer();
        var distanceToEnemy = distanceBetweenTanks();
        Debug.Log(angleBeforePerfectAim);
        if (Mathf.Abs(angleBeforePerfectAim) < 5)
        {
            tankTurret.shoot();
        }
        else if(angleBeforePerfectAim < 0)
        {
            tankTurret.turnTurretLeft();
        }
        else
        {
            tankTurret.turnTurretRight();
        }
    }

    float distanceBetweenTanks()
    {
        return Vector3.Distance(tank.tankRigidBody.transform.position, playerTank.transform.position);
    }
    float angleBetweenPlayerTurretAndEnemyTank()
    {
        return angleBetweenTwoVectors(playerTank.turret.transform.forward,
            transform.position - playerTank.transform.position, playerTank.transform.up);
    }

    float angleBetweenTurretAndPlayer()
    {
        //Debug.Log("Vec1 " + tankTurret.transform.forward +
        //    " Vec2 " + (playerTank.transform.position - transform.position) +
        //    " Up " + tankTurret.transform.up);
        return angleBetweenTwoVectors(tankTurret.transform.forward,
            playerTank.transform.position - transform.position, tankTurret.transform.up);
    }

    float angleBetweenTwoVectors(Vector3 vectorA, Vector3 vectorB, Vector3 axis)
    {
        var angle = Vector3.Angle(vectorA, vectorB);
        var cross = Vector3.Cross(vectorA, vectorB);
        Debug.Log(cross);
        if (cross.y < 0) angle = -angle;
        return angle;
    }
}
