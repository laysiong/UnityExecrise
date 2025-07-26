using System;
using System.ComponentModel;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private TMP_InputField playerNameInputField;
    [SerializeField] private TMP_Text highScoreInputField;

    void Start()
    {

        playerNameInputField = GameObject.Find("PlayerNameInputField").GetComponent<TMP_InputField>();
        highScoreInputField = GameObject.Find("HighScoreInputField").GetComponent<TMP_Text>();
        
        GetHighScore();
    }

    public void SetPlayerName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            Debug.LogWarning("Player name cannot be empty. Setting to default.");
            name = "Enter Name...";
        }
        MainDataManager.Instance.PlayerName = name;
    }

    private void GetHighScore()
    {
        if (MainDataManager.Instance.HighScoreData == null)
        {
            MainDataManager.Instance.GetHighScore();
        }
        highScoreInputField.text = $"Best Score: {MainDataManager.Instance.HighScoreData.PlayerName} - {MainDataManager.Instance.HighScoreData.Score}";
    }

    public void StartGame()
    {
        // Load the main game scene
        
        SetPlayerName(playerNameInputField.text); // Ensure player name is set before starting the game
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();

        // If running in the editor, stop playing
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    
 
}
