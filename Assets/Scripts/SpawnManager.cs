using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] obstaclePrefab;
    private Vector3 spawnPos= new Vector3(25, 0, 0);
    private float startDelay = 2.0f;
    private float repeatRate = 2.0f;
    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent <PlayerController>();
        InvokeRepeating("spawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnObstacle()
    {
        int index = Random.Range(0, obstaclePrefab.Length);
        if(playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab[index], spawnPos, obstaclePrefab[index].transform.rotation);
        }
    }
}
