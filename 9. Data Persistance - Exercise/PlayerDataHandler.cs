using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class PlayerDataHandler : MonoBehaviour
{
    public static PlayerDataHandler instance;

    //variables for current session
    public string currentPlayerName;
    public int currentScore;

    //variables for high score session
    public string bestPlayerName;
    public int bestScore;

    
    private void Awake()
    {
        //if there are 2 player data handlers in scene, destroy this
        //happens when the other scene you moved to tries to create its own data handler
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        //but if this is the only one, don't destroy it
        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }

    public void SaveHighScore(int currentScore, string currentPlayerName)
    {
        //CREATE - first create instance of the save data
        SaveData data = new SaveData();

        //SPECIFY INFO - then specify what you want to store
        data.bestScore = currentScore;
        data.bestPlayerName = currentPlayerName;

        //TRANSFORM - transform that instance into json format
        string json = JsonUtility.ToJson(data);

        //SAVE - Finally, use the specify method to write a string to a file
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.bestScore;
            bestPlayerName = data.bestPlayerName;
        }
    }

    [System.Serializable]
    public class SaveData
    {
        public int bestScore;
        public string bestPlayerName;
    }

}
