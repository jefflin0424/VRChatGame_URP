using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class XRPlayerController : NetworkBehaviour {
    private void Awake()
    {
        if (!IsHost) Destroy(gameObject);
    }
}