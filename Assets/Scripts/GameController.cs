using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject enemyTankPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject enemytank = (GameObject)Instantiate(enemyTankPrefab);
        enemytank.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 20);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
