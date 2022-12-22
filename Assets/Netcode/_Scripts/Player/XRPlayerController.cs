using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class XRPlayerController : PlayerController {
    [SerializeField] Vector2 spawnArea = new Vector2(5, 5);

    public override void OnNetworkSpawn() {
        if (!IsOwner) Destroy(this);

        transform.position = new Vector3(Random.Range(spawnArea.x, spawnArea.y), 0, Random.Range(spawnArea.x, spawnArea.y));
    }
}