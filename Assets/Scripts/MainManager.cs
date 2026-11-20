using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using SaveDataGame;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public GameObject GameOverText;
   
    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    // MIGRATED: InputAction replaces Input.GetKeyDown(KeyCode.Space)
    private InputAction m_LaunchAction;
    private InputAction m_LaunchMenu;

    private SaveData bestPlayer = new SaveData();

    // MIGRATED: bind the Space key as a button action
    void Awake()
    {
        m_LaunchAction = new InputAction("Launch", InputActionType.Button, "<Keyboard>/space");               
        m_LaunchMenu = new InputAction("Launch", InputActionType.Button, "<Keyboard>/m");               
    }

    // MIGRATED: enable the action while the component is active
    void OnEnable()
    {
        m_LaunchAction.Enable();
        m_LaunchMenu.Enable();
    }

    // MIGRATED: disable the action when the component is inactive
    void OnDisable()
    {
        m_LaunchAction.Disable();
        m_LaunchMenu.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }        

        if(MenuMainManager.Instance != null)
        {
            SaveData player = LoadBestPlayer();
            if (player != null)
            {
                bestPlayer = player;
            }             
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (m_LaunchAction.WasPressedThisFrame()) // MIGRATED: was Input.GetKeyDown(KeyCode.Space)
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (m_LaunchAction.WasPressedThisFrame()) // MIGRATED: was Input.GetKeyDown(KeyCode.Space)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if (m_LaunchMenu.WasPerformedThisFrame())
            {                
                SceneManager.LoadScene(0);               
            }
        }
    }

    public void SavePlayer()
    {        
        if (bestPlayer.Score < m_Points)
        {
            SaveData data = new SaveData();
            data.PlayerName = MenuMainManager.Instance.PlayerName;
            data.Score = m_Points;
                       
            SaveDataJson.SaveGameData(data);
        }
    }

    public SaveData LoadBestPlayer()
    {
        SaveData data = SaveDataJson.LoadGameData();
        if (data != null)
        {
            BestScoreText.text = $"Best Score: {data.PlayerName}: {data.Score}";
        }
        else
        {
            BestScoreText.text = "Best Score: : 0";
        }

        return data;
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        SavePlayer();
        LoadBestPlayer();
        m_GameOver = true;
        GameOverText.SetActive(true);        
    }
}
