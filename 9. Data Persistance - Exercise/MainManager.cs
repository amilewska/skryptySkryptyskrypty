using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;

    //my variables
    private string playerName;
    private int score;
    private string bestPlayer;
    public Text bestScoreWithName;
    //my variables

    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerDataHandler.instance.LoadHighScore();
        //on the top show the best score to know what to achieve
        


        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (PlayerDataHandler.instance != null)
        {
            playerName = PlayerDataHandler.instance.currentPlayerName;
            bestPlayer = PlayerDataHandler.instance.bestPlayerName;
            score = PlayerDataHandler.instance.bestScore;

            if (PlayerDataHandler.instance.bestScore != 0)
            {
                bestScoreWithName.text = "Hi, " + playerName + "! Can you beat the best score by " + bestPlayer + " : " + score + "?";
            }
            else
            {
                bestScoreWithName.text = playerName + ", set a high score!";
            }
        }
        else bestScoreWithName.text = "Hello, set a high score!";

        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        //AFTER FIRST GAME SAVE POINTS AND SHOW BEST SCORE
        m_GameOver = true;
        GameOverText.SetActive(true);
        CheckBestPlayer();
    }

    void CheckBestPlayer()
    {
        //IF PLAYER BEATS HIGH SCORE, SAVE THEIR NAME AND SCORE
        if(m_Points > PlayerDataHandler.instance.bestScore)
        {
            //set in player data handler the new high score and name of player
            PlayerDataHandler.instance.bestScore = m_Points;
            PlayerDataHandler.instance.bestPlayerName = PlayerDataHandler.instance.currentPlayerName;

            //then save this high score and player's name into file

            PlayerDataHandler.instance.SaveHighScore(PlayerDataHandler.instance.bestScore, PlayerDataHandler.instance.bestPlayerName);

            //testing
            Debug.Log("New high score " + PlayerDataHandler.instance.bestScore);

        }
    }

}
