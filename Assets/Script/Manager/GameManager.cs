using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour {
    //[SerializeField] GameObject player;
    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
        //DontDestroyOnLoad(this);
    }

    void Start()
    {

    }

    #region netcode
    [SerializeField] private PlayerController _playerPrefab;
    //[SerializeField] GameObject mainCamera;
    public override void OnNetworkSpawn() {
        SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId);
    }   

    [ServerRpc(RequireOwnership = false)]
    private void SpawnPlayerServerRpc(ulong playerId) {
        var spawn = Instantiate(_playerPrefab);
        spawn.NetworkObject.SpawnWithOwnership(playerId);
    }

    public override void OnDestroy() {
        base.OnDestroy();
        MatchmakingService.LeaveLobby();
        if(NetworkManager.Singleton != null )NetworkManager.Singleton.Shutdown();
    }
    #endregion
}