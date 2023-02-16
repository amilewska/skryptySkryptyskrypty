using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMenager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    private float spawnRangeX = 20;
    private float spawnRangeZ = 20;
    private float spawnPosZ = 20;
    private float spawnPosX = 20;
    private float spawnDelay = 2;
    private float spawnInterval = 2.5f;
   
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomAnimalUp", spawnDelay, spawnInterval);
        InvokeRepeating("SpawnRandomAnimalLeft", spawnDelay+2, spawnInterval);
        InvokeRepeating("SpawnRandomAnimalRight", spawnDelay+4, spawnInterval);
    }

    void SpawnRandomAnimalUp()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPosUp = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);

        Instantiate(animalPrefabs[animalIndex], spawnPosUp, animalPrefabs[animalIndex].transform.rotation);
    }

    void SpawnRandomAnimalLeft()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPosLeft = new Vector3(-spawnPosX, 0, Random.Range(0, spawnRangeZ));
       
        Instantiate(animalPrefabs[animalIndex], spawnPosLeft, Quaternion.Euler(0,90,0));
    }

    void SpawnRandomAnimalRight()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPosRight = new Vector3(spawnPosX, 0, Random.Range(0, spawnRangeZ));
        Instantiate(animalPrefabs[animalIndex], spawnPosRight, Quaternion.Euler(0, -90, 0));
    }
}
