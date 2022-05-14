using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

public class BestScore : MonoBehaviour
{
    public static BestScore Instance;
    public string userName;
    public string currentUserName;
    public int userScore;
    public TMP_InputField userNameText;
    public TextMeshProUGUI hiScoreText;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        LoadHiScore();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    public void SaveName()
    {
        currentUserName = userNameText.text;
    }

    [System.Serializable]
    class SaveData
        {
        public string name;
        public int hiScore;
        }

    public void SaveHiScore()
    {
        SaveData data = new SaveData();
        data.hiScore = userScore;
        data.name = userName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHiScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            userName = data.name;
            userScore = data.hiScore;

            hiScoreText.text = "Hiscore: " + userName + " : " + userScore;
        }
    }
}
