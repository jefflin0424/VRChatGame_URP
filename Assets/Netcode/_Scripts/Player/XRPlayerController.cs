using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class XRPlayerController : NetworkBehaviour {

    public override void OnNetworkSpawn() {
        if (!IsOwner) Destroy(this);
    }
}