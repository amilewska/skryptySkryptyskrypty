using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 10.0f;
    PlayerController playerControllerScript;
    GameManager gameManager;
    float leftBound = -10;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.isGameOver == false)
        {
            if (playerControllerScript.pressedShift) transform.Translate(Vector3.left * Time.deltaTime * speed * 2);
            else transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (transform.position.x < leftBound && !gameObject.CompareTag("Background"))
        {
            if (playerControllerScript.pressedShift) gameManager.AddScore(2);
            else gameManager.AddScore(1);
            Destroy(gameObject);
        }
        if (transform.position.x <= 0 && !gameObject.CompareTag("Background"))
        {
            
        }
    }
}
