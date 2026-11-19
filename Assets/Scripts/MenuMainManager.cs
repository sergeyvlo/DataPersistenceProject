using System.IO;
using TMPro;
using UnityEngine;
using SaveDataGame;

public class MenuMainManager : MonoBehaviour
{
    public static MenuMainManager Instance;
    
    public string PlayerName;
    public TMP_InputField InputName;   
    public TextMeshProUGUI BestScoreText;

    const string FileName = "savefile.json";    

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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        LoadBestPlayer();
    }


    public SaveData LoadGameData()
    {
        string path = Application.persistentDataPath + "/" + FileName;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            return data;
        }
        return null;
    }

    public void SaveGameData(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/" + FileName, json);
    }
            
   public void LoadBestPlayer()
    {
        SaveData data = LoadGameData();
        if (data != null)
        {
            BestScoreText.text = $"Best Score: {data.PlayerName}: {data.Score}";
            InputName.text = data.PlayerName;            
        }
        else
        {
            BestScoreText.text = "Best Score:";
        }    
    }
}
