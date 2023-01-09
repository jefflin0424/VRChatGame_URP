using Unity.Netcode;
using UnityEngine;
using UnityEngine.Animations;

public class GameManager : NetworkBehaviour {
    //[SerializeField] GameObject player;
    public static GameManager Instance;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    #region netcode
    [SerializeField] private NetworkPlayerController _playerPrefab;
    [SerializeField] Transform _headTarget, _xrOrigin, _map;
    public override void OnNetworkSpawn() {
        SpawnPlayerServerRpc(NetworkManager.Singleton.LocalClientId);
        if(!RoomScreen.showRoom) _map.gameObject.SetActive(false);
        if(!IsHost)
        {
            Destroy(_headTarget.GetComponent<ParentConstraint>());
            if(RoomScreen.toggleVR) Destroy(_xrOrigin.gameObject);
        }
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