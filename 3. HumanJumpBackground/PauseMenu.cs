using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    bool isPaused;
    AudioSource audioSos;
    // Start is called before the first frame update
    void Start()
    {
        audioSos = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
        //when escape is pressed go to pause menu
        if (isPaused) ActivateMenu();
        //when escape is pressed leave pause menu
        else DeactivateMenu();
    }

    void ActivateMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        audioSos.enabled = false;

    }
    void DeactivateMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        audioSos.enabled=true;
    }

    public void ContinueGame()
    {
        DeactivateMenu();
    }

    public void StartAgain()
    {
        //Application.Quit();
        SceneManager.LoadScene(0);
        
    }

    public void ExitMenu()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}


