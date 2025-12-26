using UnityEngine;

public sealed class GameServices
{
    public static GameServices Instance { get; private set; }

    public GameConfig GameConfig { get; }
    public WorldGenConfig WorldGenConfig { get; }

    private GameServices(GameConfig gameConfig, WorldGenConfig worldGenConfig)
    {
        GameConfig = gameConfig;
        WorldGenConfig = worldGenConfig;
    }

    public static void Initialize(GameConfig gameConfig, WorldGenConfig worldGenConfig)
    {
        if (Instance != null)
        {
            Debug.LogWarning("GameServices already initialized.");
            return;
        }

        Instance = new GameServices(gameConfig, worldGenConfig);
    }
}
