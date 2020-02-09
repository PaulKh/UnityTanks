using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnParticleSystemStopped()
    {
        Debug.Log("Explosion particle system stopped");
        gameObject.SetActive(false);
    }
}
