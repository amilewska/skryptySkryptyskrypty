using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //intro variables
    [SerializeField] GameObject player;
    [SerializeField] GameObject startplayerPosition;
    float speedIntro = 19;
    //reference script
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    //UI panels
    public static int menuPanel = 0;
    public GameObject startScreen;
    public GameObject endPanelScreen;
    [SerializeField] TextMeshProUGUI endText;
    public GameObject pauseScreen;
    public GameObject instructionPanel;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] public float timer = 60;
    //UI score
    [SerializeField] TextMeshProUGUI scoreText;
    public int score = 0;

    //music
    AudioSource audioBackground;
    public List<AudioSource> audioEffects;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider volumeEffectsSlider;
    [SerializeField] bool showInstruction;

    //others
    public GameObject sunLight;
    public GameObject barrier;
    public GameObject snowball;
    public GameObject ball;
    public GameObject endPanel;

    //bools
    public bool isGameActive = false;
    public bool isGamePaused;

    //level variables
    public int aim = 10;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
        //saving player on sound volume slider
        audioBackground = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        volumeSlider.value = PlayerPrefs.GetFloat("volumePref", 0.2f);
        audioBackground.volume = PlayerPrefs.GetFloat("volumePref", 0.2f);

        volumeEffectsSlider.value = PlayerPrefs.GetFloat("volumeEffectsPref", 0.2f);
        foreach (AudioSource audioEffect in audioEffects)
        {
            audioEffect.volume = PlayerPrefs.GetFloat("volumeEffectsPref", 0.2f);
        }

        endPanelScreen.SetActive(false);
        isGameActive = false;
        showInstruction = PlayerPrefs.GetInt("instruction") == 0 ? false : true;

        
        //to disable or anable menu panel
        if (menuPanel == 1)
        {
            showInstruction = false;
            //startScreen.SetActive(false);
            StartGame();
            
        }
        else
        {
            startScreen.SetActive(true);
            
        }
        

    }

    IEnumerator PlayIntro()
    {
        
        Vector3 startPos = player.transform.position;
        Vector3 endPos = startplayerPosition.transform.position;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;
        float distanceCovered = (Time.time - startTime) * speedIntro;
        float fractionOfJourney = distanceCovered / journeyLength;

        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * speedIntro;
            fractionOfJourney = distanceCovered / journeyLength;
            player.transform.position = Vector3.Lerp(startPos, endPos,
            fractionOfJourney);
            yield return null;
        }
    }
    public void ToggleButton()
    {
        showInstruction = !showInstruction;
        PlayerPrefs.SetInt("instruction", showInstruction ? 0 : 1);
    }

    //When start button is pressed
    public void StartGame()
    {
        StartCoroutine(PlayIntro());
        

        startScreen.SetActive(false);
        instructionPanel.SetActive(showInstruction);

        isGameActive = true;
        isGamePaused = false;
        GameObject.Find("SpawnManager").GetComponent<SpawnManager>().StartCoroutine("SpawnBall");
        StartCoroutine(CountDown());
        audioBackground.enabled = true;
    }


    void Update()
    {
        //escape activate the pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isGamePaused = !isGamePaused;
            
        }

        if (isGamePaused) ActivePauseMenu();
        if (!isGamePaused) DeactivePauseMenu();

        //light is changing over time
        sunLight.transform.Rotate(new Vector3(1, 0, 0), Time.deltaTime * 10);
        
        //after achieving the aim, show end panel and prepare 
        if (score >= aim)
        { 
            GameOver();
            AfterCounting();
        }
    }

    void AfterCounting() //sequence where end screen panel is removing and player can go to right
    {
        if (player.transform.position.z > 36 && endPanel.transform.position.x > -800)
        {
            endPanel.transform.Translate(new Vector3(-player.GetComponent<Rigidbody>().velocity.magnitude * Time.deltaTime * 10, 0, 0));
        }
        
        barrier.GetComponent<Rigidbody>().isKinematic = false;

        snowball.transform.LookAt(barrier.transform.position);
        
        ball.transform.LookAt(barrier.transform.position);

        ball.GetComponent<Rigidbody>().isKinematic = false;
        ball.transform.position = Vector3.Lerp(ball.transform.position, barrier.transform.position, Time.deltaTime);
        
    }

    //showing adequate end panel when game is over
    public void GameOver()
    {
        endPanelScreen.SetActive(true);
        isGameActive = false;
        if (score >= aim) endText.text = "Congrats";
        else endText.text = "GameOver";
        
    }

    //counting 60 seconds descending
    IEnumerator CountDown()
    {
        while (isGameActive && timer >= 0)
        {
            timerText.text = "Time: " + timer;
            yield return new WaitForSeconds(1);

            timer--;
        }

        GameOver();
        
    }

    //when pause menu is activated, stop the time and the audio
    public void ActivePauseMenu()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;
        if (audioBackground.isPlaying) audioBackground.Pause();
    }
    public void DeactivePauseMenu()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        if(!audioBackground.isPlaying) audioBackground.Play();
        isGamePaused = false;
    }

    public void LoadMenu()
    {
        menuPanel = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
    public void Restart()
    {
        menuPanel = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }


    public void SetVolume()
    {
        PlayerPrefs.SetFloat("volumePref", volumeSlider.value);
        audioBackground.volume = PlayerPrefs.GetFloat("volumePref");
    }

    public void SetEffectsVolume()
    {
        PlayerPrefs.SetFloat("volumeEffectsPref", volumeEffectsSlider.value);
        foreach (AudioSource audioEffect in audioEffects)
        {
            audioEffect.volume = PlayerPrefs.GetFloat("volumeEffectsPref");
        }
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = "Balls: " + score.ToString() +"/"+ aim.ToString();
    }

    

}

