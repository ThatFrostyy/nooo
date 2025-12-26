using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBootstrapper : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private WorldGenConfig worldGenConfig;

    [Header("Scenes")]
    [SerializeField] private string menuSceneName = "MainMenu";

    private void Awake()
    {
        GameServices.Initialize(gameConfig, worldGenConfig);
        DontDestroyOnLoad(gameObject);
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
