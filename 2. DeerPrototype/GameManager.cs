using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int score = 0;
    int lives = 0;
     
    public void AddLives (int value)
    {
        lives += value;
        if (lives <= 0)
        {
            Debug.Log("GameOver!");
            lives = 0;
        }
        Debug.Log("Lives: " + lives);
    }

    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
    }

}
