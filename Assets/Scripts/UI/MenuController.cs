using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private NetworkGameManager networkGameManager;
    [SerializeField] private string gameSceneName = "Game";
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private Button serverButton;

    private void Awake()
    {
        if (networkGameManager == null)
        {
            networkGameManager = FindFirstObjectByType<NetworkGameManager>();
        }

        if (hostButton != null)
        {
            hostButton.onClick.AddListener(StartHost);
        }

        if (clientButton != null)
        {
            clientButton.onClick.AddListener(StartClient);
        }

        if (serverButton != null)
        {
            serverButton.onClick.AddListener(StartServer);
        }
    }

    public void StartHost()
    {
        if (networkGameManager == null)
        {
            Debug.LogError("MenuController requires a NetworkGameManager.");
            return;
        }

        networkGameManager.StartHost();
        LoadGameSceneOnServer();
    }

    public void StartClient()
    {
        if (networkGameManager == null)
        {
            Debug.LogError("MenuController requires a NetworkGameManager.");
            return;
        }

        networkGameManager.StartClient();
    }

    public void StartServer()
    {
        if (networkGameManager == null)
        {
            Debug.LogError("MenuController requires a NetworkGameManager.");
            return;
        }

        networkGameManager.StartServer();
        LoadGameSceneOnServer();
    }

    private void LoadGameSceneOnServer()
    {
        if (string.IsNullOrWhiteSpace(gameSceneName))
        {
            Debug.LogError("MenuController missing game scene name.");
            return;
        }

        NetworkManager networkManager = NetworkManager.Singleton;
        if (networkManager == null)
        {
            Debug.LogError("NetworkManager is required to load the game scene.");
            return;
        }

        if (networkManager.SceneManager != null)
        {
            networkManager.SceneManager.LoadScene(gameSceneName, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }
}
