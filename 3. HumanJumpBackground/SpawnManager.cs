using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    Vector3 spawnPos = new Vector3(37, 0, 0);
    float spawnDelay = 2;
    float spawnInterval = 2;
    PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", spawnDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle ()
    {
        int indexPrefab = Random.Range(0, obstaclePrefab.Length);
        if (playerControllerScript.isGameOver == false )
        {
            Instantiate(obstaclePrefab[indexPrefab], spawnPos, obstaclePrefab[indexPrefab].transform.rotation);
        }
        
    }
}
