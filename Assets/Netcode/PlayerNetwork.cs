using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    NetworkVariable<MyCustomData> randomNumber = new NetworkVariable<MyCustomData>(
        new MyCustomData
        {
            _int = 56,
            _bool = true,
        },
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);

    public struct MyCustomData : INetworkSerializable
    {
        public int _int;
        public bool _bool;
        public FixedString128Bytes message;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _int);
            serializer.SerializeValue(ref _bool);
            serializer.SerializeValue(ref message);
        }
    }
    public override void OnNetworkSpawn() 
    {
        randomNumber.OnValueChanged += (MyCustomData previousValue, MyCustomData newValue) =>
        {
            print(OwnerClientId + "; " + newValue._int + "; " + newValue._bool + "; " + newValue.message);
        };
    }

    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            randomNumber.Value = new MyCustomData
            {
                _int = 10,
                _bool = false,
                message = "All your bases belong to us!"
            };
        }

        Vector3 moveDir = new Vector3(0, 0, 0);
        if (Input.GetKeyDown(KeyCode.W)) moveDir.z = +1f;
        if (Input.GetKeyDown(KeyCode.S)) moveDir.z = -1f;
        if (Input.GetKeyDown(KeyCode.A)) moveDir.x = +1f;
        if (Input.GetKeyDown(KeyCode.D)) moveDir.x = -1f;

        float moveSpeed = 3f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
