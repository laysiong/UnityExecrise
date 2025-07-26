using System.IO;
using Microsoft.SqlServer.Server;
using UnityEngine;

public class MainDataManager : MonoBehaviour
{
    public static MainDataManager Instance;

    [Header("Player Data")]
    public string PlayerName;
    public int Score = 0;

    public PlayerData HighScoreData;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        GetHighScore();
    }

    [System.Serializable]
    class SaveData
    {
        public PlayerData Player = new PlayerData();
    }

    [System.Serializable]
    public class PlayerData
    {
        public string PlayerName = "None";
        public int Score = 0;
    }

    public void SaveResult()
    {
        SaveData data = new SaveData();
        data.Player.PlayerName = PlayerName;
        data.Player.Score = Score;
        string json = JsonUtility.ToJson(data, true);
        Debug.Log($"Saving data path: {Application.persistentDataPath}");
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "savefile.json"), json);
    }

    public void GetHighScore()
    {
        string path = Path.Combine(Application.persistentDataPath, "savefile.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            // Ensure HighScoreData is initialized before assigning
            if (HighScoreData == null)
            {
                HighScoreData = new PlayerData();
            }
            
            HighScoreData.PlayerName = data.Player.PlayerName;
            HighScoreData.Score = data.Player.Score;
        }
        else
        {
            Debug.LogWarning("Save file not found!");
        }
    }
    

}
