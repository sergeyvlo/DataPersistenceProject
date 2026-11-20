using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using SaveDataGame;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField InputName;
    public TextMeshProUGUI BestScoreText;
    
    public Button confirmButton; 
    public int minCharacters = 3;

    void Start()
    {
        LoadBestPlayer();
        ValidateInput(InputName.text);

        // Подписываемся на каждое изменение текста
        InputName.onValueChanged.AddListener(ValidateInput);
    }

    void OnDestroy()
    {
        InputName.onValueChanged.RemoveListener(ValidateInput);
    }

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

    public void LoadBestPlayer()
    {
        SaveData data = SaveDataJson.LoadGameData();
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
    
    private void ValidateInput(string text)
    {       
        bool isValid = text.Trim().Length >= minCharacters;

        if (confirmButton)
        {
            confirmButton.interactable = isValid;
        }
    }
}
