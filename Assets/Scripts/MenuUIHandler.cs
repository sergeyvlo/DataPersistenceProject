using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField InputName;

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    
    public void StartGame()
    {
        MenuMainManager.Instance.PlayerName = InputName.text;
        SceneManager.LoadScene(1);        
    }
}
