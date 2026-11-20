using UnityEngine;

public class MenuMainManager : MonoBehaviour
{
    public static MenuMainManager Instance;
    
    public string PlayerName;          

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
}
