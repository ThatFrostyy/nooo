using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBootstrapper : MonoBehaviour
{
    public static GameBootstrapper Instance { get; private set; }


    [Header("Configs")]
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private WorldGenConfig worldGenConfig;

    [Header("Scenes")]
    [SerializeField] private string menuSceneName = "MainMenu";

    private void Awake()
    {
        GameServices.Initialize(gameConfig, worldGenConfig);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (!string.IsNullOrWhiteSpace(menuSceneName) &&
            SceneManager.GetActiveScene().name != menuSceneName)
        {
            SceneManager.LoadSceneAsync(menuSceneName);
        }
    }
}
