using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public TextMeshProUGUI nameInput;
    public TextMeshProUGUI bestScoreText;
    public string nameInputText;
    public string bestScoreName;
    public int bestScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadData();
    }

    private void Start()
    {
        bestScoreText.text = "Best Score: " + bestScoreName + " : " + bestScore;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void UpdateName()
    {
        nameInputText = nameInput.text;
    }

    //Saving:

    [System.Serializable]
    class SaveData
    {
        public string bestScoreName;
        public int bestScore;
    }

    public void SaveNameAndScore()
    {
        SaveData data = new SaveData();
        data.bestScoreName = bestScoreName;
        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScoreName = data.bestScoreName;
            bestScore = data.bestScore;
        }
    }

    private void OnApplicationQuit()
    {
        SaveNameAndScore();
    }
}
