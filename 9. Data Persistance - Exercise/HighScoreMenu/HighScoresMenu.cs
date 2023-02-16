using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScoresMenu : MonoBehaviour
{
    public Text firstScore;
    public Text secondScore;
    public Text thirdScore;

    public void SetPlaceScore()
    {
        PlayerDataHandler.instance.LoadHighScore();
        firstScore.text = "1. "  +PlayerDataHandler.instance.bestScore+ PlayerDataHandler.instance.bestPlayerName;

        secondScore.text = "2. " + PlayerDataHandler.instance.bestScore;
    }



}
