using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkGameManager : MonoBehaviour
{
    [Header("Netcode")]
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private NetworkObject playerPawnPrefab;

    private readonly Dictionary<ulong, PlayerConnection> connections = new();

    public IReadOnlyDictionary<ulong, PlayerConnection> Connections => connections;

    private void Awake()
    {
        if (networkManager == null)
        {
            networkManager = NetworkManager.Singleton;
        }

        if (networkManager == null)
        {
            Debug.LogError("NetworkGameManager requires a NetworkManager in the scene.");
            enabled = false;
            return;
        }

        networkManager.OnClientConnectedCallback += HandleClientConnected;
        networkManager.OnClientDisconnectCallback += HandleClientDisconnected;
    }

    private void OnDestroy()
    {
        if (networkManager == null)
        {
            return;
        }

        networkManager.OnClientConnectedCallback -= HandleClientConnected;
        networkManager.OnClientDisconnectCallback -= HandleClientDisconnected;
    }

    public void StartHost()
    {
        EnsureNetworkManager();
        if (!networkManager.IsListening)
        {
            networkManager.StartHost();
        }
    }

    public void StartServer()
    {
        EnsureNetworkManager();
        if (!networkManager.IsListening)
        {
            networkManager.StartServer();
        }
    }

    public void StartClient()
    {
        EnsureNetworkManager();
        if (!networkManager.IsListening)
        {
            networkManager.StartClient();
        }
    }

    public NetworkObject SpawnServerOwned(NetworkObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!networkManager.IsServer)
        {
            Debug.LogWarning("SpawnServerOwned should only be called on the host/server.");
            return null;
        }

        if (prefab == null)
        {
            Debug.LogError("SpawnServerOwned requires a prefab.");
            return null;
        }

        NetworkObject instance = Instantiate(prefab, position, rotation);
        instance.Spawn();
        return instance;
    }

    private void HandleClientConnected(ulong clientId)
    {
        if (!networkManager.IsServer)
        {
            return;
        }

        SpawnPlayerPawn(clientId);
    }

    private void HandleClientDisconnected(ulong clientId)
    {
        if (!networkManager.IsServer)
        {
            return;
        }

        if (networkManager.ConnectedClients.TryGetValue(clientId, out NetworkClient client) &&
            client.PlayerObject != null)
        {
            client.PlayerObject.Despawn(true);
        }

        connections.Remove(clientId);
    }

    private void SpawnPlayerPawn(ulong clientId)
    {
        if (playerPawnPrefab == null)
        {
            Debug.LogError("NetworkGameManager missing player pawn prefab.");
            return;
        }

        NetworkObject pawnInstance = Instantiate(playerPawnPrefab);
        pawnInstance.SpawnAsPlayerObject(clientId, true);

        connections[clientId] = new PlayerConnection(clientId, pawnInstance.NetworkObjectId);
    }

    private void EnsureNetworkManager()
    {
        if (networkManager == null)
        {
            Debug.LogError("NetworkManager not set on NetworkGameManager.");
        }
    }
}
