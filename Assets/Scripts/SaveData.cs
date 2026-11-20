using System.IO;
using UnityEngine;

namespace SaveDataGame
{
    [System.Serializable]
    public class SaveData
    {
        public string PlayerName = "";
        public int Score = 0;
    }

    public class SaveDataJson
    {
        const string FileName = "savefile.json";    
        public static SaveData LoadGameData()
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
    
        public static void SaveGameData(SaveData data)
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/" + FileName, json);
        }      
    
    }
}
 
