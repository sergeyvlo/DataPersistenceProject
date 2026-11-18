using System.IO;
using TMPro;
using UnityEngine;

public class MenuMainManager : MonoBehaviour
{
    const string FileName = "savefile.json";
    public static MenuMainManager Instance;
    public TextMeshProUGUI PlayerName;

    public TextMeshProUGUI BestScoreText;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);        
    }
    
    public void LoadPlayer()
    {
        string path = Application.persistentDataPath + "/" + FileName;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestScoreText.text = $"Best Score: {data.PlayerName}: {data.Score}";
        }
        else
        {
            BestScoreText.text = "Best Score:";
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
