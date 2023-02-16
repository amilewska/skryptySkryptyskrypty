using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1;
    private int score;
    private AudioSource soundEffect;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI liveText;
    public TextMeshProUGUI gameOverText;
    public bool isGameActive;
    public Button button;
    public GameObject titleScreen;
    public GameObject pausedScreen;
    public float timer = 30;
    public int live = 3;

    public Slider volumeSlider;
    public AudioSource music;

    public bool gameIsPaused;
    public GameObject blade;

    public AudioClip clickEffect;

    public void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volumePref", 1);
        music.volume = PlayerPrefs.GetFloat("volumePref", 1);
        soundEffect = GetComponent<AudioSource>();
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        titleScreen.gameObject.SetActive(false);
        score = 0;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(0);
        StartCoroutine(StartCountdown());
        pausedScreen.SetActive(false);

    }
    


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            soundEffect.PlayOneShot(clickEffect);
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
        
    }

    public void PauseGame()
    {
        if (gameIsPaused && isGameActive)
        {
            Time.timeScale = 0;
            pausedScreen.SetActive(true);
            music.Pause();
            blade.SetActive(false);

        }
        else
        {
            Time.timeScale = 1;
            pausedScreen.SetActive(false);
            music.Play(0);
            blade.SetActive(true);
        }

    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
        
    }

    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int value)
    {
        live += value;
        if(live>=0)liveText.text = "Lives: " + live;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        button.gameObject.SetActive(true);
        isGameActive = false;

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator StartCountdown()
    {

        while (isGameActive && timer >= 0)
        {
            timerText.text = "time: " + timer;
            yield return new WaitForSeconds(1);
            timer--;
        }

        GameOver();

    }

    public void SetVolume()
    {
        PlayerPrefs.SetFloat("volumePref", volumeSlider.value);
        music.volume = PlayerPrefs.GetFloat("volumePref");
        
    }
}
