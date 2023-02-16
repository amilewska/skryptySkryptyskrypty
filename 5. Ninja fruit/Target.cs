using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -4;
    private GameManager gameManager;

    public ParticleSystem explosionParticle;
    


    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = SpawnRandomPosition();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        
    }


    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad") && other.gameObject.CompareTag("Bound")) gameManager.UpdateLives(-1);
        if (gameManager.live < 0) gameManager.GameOver();

        if (gameManager.isGameActive && other.name.Equals("Blade"))
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            

            if (name.Contains("Bad 1")) gameManager.UpdateScore(-5);
            if (name.Contains("Good 1")) gameManager.UpdateScore(1);
            if (name.Contains("Good 2")) gameManager.UpdateScore(2);
            if (name.Contains("Good 3")) gameManager.UpdateScore(4);
            if (name.Contains("Good 4")) gameManager.UpdateScore(5);
        }
    }
    

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 SpawnRandomPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
