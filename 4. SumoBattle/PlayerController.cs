using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PowerUp;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerupStrength = 20.0f;
    private float timer = 0.5f;
    private Coroutine powerupCountdown;

    public PowerUpType currentPowerUp = PowerUpType.None;

    public GameObject powerupIndicator;
    public GameObject bulletPrefab;
    public GameObject[] enemies;

    public float speed = 10.0f;
    public float jumpForce = 10;
    public float hangTime;
    public float smashSpeed;
    public float explosionForce;
    public float explosionRadius;
    public float floorY;
    
    public bool hasPowerup;
    public bool smashing = false;

    
    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput;
        verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * verticalInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if (currentPowerUp == PowerUpType.Shot) HomingRockets();
        if (currentPowerUp == PowerUpType.Smash && Input.GetKeyDown(KeyCode.Space) && smashing==false)
        {
            StartCoroutine(Smash());
            smashing = true;
        }
           
        if (transform.position.y < -10)
        {
            Debug.Log("GAMEOVER");
            Destroy(gameObject);
        }

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDownRutine());
            powerupIndicator.SetActive(true);

            if (powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }

            powerupCountdown = StartCoroutine(PowerUpCountDownRutine());
        }

    }

    
    IEnumerator PowerUpCountDownRutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        currentPowerUp = PowerUpType.None;
        powerupIndicator.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Push)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position-transform.position ;
            enemyRigidbody.AddForce(awayFromPlayer*powerupStrength, ForceMode.Impulse);
        }


    }


    void HomingRockets()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                Vector3 positionFromEnemy = (enemies[i].transform.position - transform.position).normalized;

                bulletPrefab.GetComponent<MoveForwards>().enemy = enemies[i];

                Instantiate(bulletPrefab, transform.position + positionFromEnemy, bulletPrefab.transform.rotation);
            
            }

            timer = 0.7f;
        }

    }

    IEnumerator Smash()
    {
        
        var enemies = FindObjectsOfType<Enemy>();

        floorY = transform.position.y;
        float jumpTime = Time.time + hangTime;

        while (Time.time < jumpTime)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, smashSpeed);
            yield return null;

        }

        while (transform.position.y > floorY)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, -smashSpeed * 2);
            yield return null;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies != null)
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
        }

        smashing = false;
    }
    
    



}
