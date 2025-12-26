using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private WorldGenConfig worldGenConfig;

    private void Awake()
    {
        GameServices.Initialize(gameConfig, worldGenConfig);
        DontDestroyOnLoad(gameObject);
    }
}
