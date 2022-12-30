using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Animations;

public class NetworkIkController : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsHost)
        {
            Destroy(GetComponent<ParentConstraint>());
        }
    }
}
