using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject[] bossPrefab;
    public GameObject[] powerupPrefab;
    private float spawnRange = 9.0f;
    private int countEnemy;
    public int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemieWave(waveNumber);
        SpawnPowerup();
    }

    // Update is called once per frame
    void Update()
    {
        countEnemy = GameObject.FindObjectsOfType<Enemy>().Length;

        if (countEnemy == 0)
        {
            waveNumber++;

            if (waveNumber % 4 == 0) SpawnBoss();
            else SpawnEnemieWave(waveNumber);
            SpawnPowerup();
            
        }


        
    }

    void SpawnEnemieWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int indexRandom = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[indexRandom], GenerateSpawnPos(), enemyPrefab[indexRandom].transform.rotation);
        }
    }

    void SpawnBoss()
    {
        for (int i = 0; i < 1; i++)
        {
            int indexRandom = Random.Range(0, bossPrefab.Length);
            Instantiate(bossPrefab[indexRandom], GenerateSpawnPos(), bossPrefab[indexRandom].transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPos()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 spawnPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return spawnPos;
    }

    void SpawnPowerup()
    {
        int indexRandom = Random.Range(0, powerupPrefab.Length);
        Instantiate(powerupPrefab[indexRandom], GenerateSpawnPos(), powerupPrefab[indexRandom].transform.rotation);
    }

     
}
