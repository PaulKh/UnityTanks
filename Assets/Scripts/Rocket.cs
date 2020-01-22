using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    ParticleSystem rocketParticleSystem;
    List<ParticleCollisionEvent> collisionEvents;
    public GameObject ExplosionPrefab;
    private Vector3 startPosition;

    void Start()
    {
        ObjectPooler.SharedInstance.initObjectToPool(ExplosionPrefab, ExplosionPrefab.tag, true, 5);
        rocketParticleSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    public void OnParticleSystemStopped()
    {
        Debug.Log("Particle system stopped");
        gameObject.SetActive(false);
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Collision");
        int numCollisionEvents = rocketParticleSystem.GetCollisionEvents(other, collisionEvents);
        Debug.Log(numCollisionEvents);
        if(numCollisionEvents > 0)
        {
            //if(other.tag == "Obstacle")
            //{
                //Debug.Log("obstacle shot");
                //Debug.Log(this.startPosition);
                //Debug.Log(collisionEvents[0].intersection);
                //var normalizedDirection = -Vector3.Normalize(this.startPosition - collisionEvents[0].intersection + new Vector3(0, -1, 0));
                //Debug.Log(normalizedDirection); 
                //other.GetComponent<Rigidbody>().AddForceAtPosition(normalizedDirection * 3000, collisionEvents[0].intersection);
            //other.GetComponent<Rigidbody>().AddExplosionForce(3000, collisionEvents[0].intersection, 5);
            ExplosionDamage(collisionEvents[0].intersection, 5, 3000);
            //}
            var intersectionPoint = collisionEvents[0].intersection;

            var explosionObject = ObjectPooler.SharedInstance.GetPooledObject(ExplosionPrefab.tag);
            explosionObject.transform.position = intersectionPoint;
            explosionObject.SetActive(true);

        }
    }

    public void setStartPosition(Vector3 startShootPosition)
    {
        this.startPosition = startShootPosition;
    }

    void ExplosionDamage(Vector3 explosionPos, float radius, float power)
    {

        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius);
        }
    }
}
