using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] Transform spawnedObjectPrefab; // interchangeable with GameObject
    [SerializeField] float mouseSensitivity = 100f;
    [SerializeField] short moveSpeed = 3;
    private GameObject _camera;
    //[SerializeField] GameObject spawnedObjectPrefab;
    Transform spawnedObjectTransform;

    NetworkVariable<MyCustomData> randomNumber = new NetworkVariable<MyCustomData>(
        new MyCustomData
        {
            _int = 56,
            _bool = true,
        },
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner);
    private object input;
    private Vector3 _mouse;

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

    private void Start()
    {
        _camera = GameObject.Find("Main Camera");
        _mouse = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
    }

    void Update()
    {
        if (!IsOwner) return;

        _camera.transform.position = transform.position;
        _camera.transform.rotation = transform.rotation;

        if (Input.GetKeyDown(KeyCode.T))
        {
            spawnedObjectTransform = Instantiate(spawnedObjectPrefab);
            spawnedObjectTransform.GetComponent<NetworkObject>().Spawn(true);
            /*
            randomNumber.Value = new MyCustomData
            {
                _int = 10,
                _bool = false,
                message = "All your bases belong to us!"
            };
            */
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Destroy(spawnedObjectTransform.gameObject);
        }

        if (Input.GetKey(KeyCode.W)) transform.position += transform.forward * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) transform.position -= transform.forward * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) transform.position -= transform.right * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) transform.position += transform.right * moveSpeed * Time.deltaTime;

        //mouse movemenet
        _mouse += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * mouseSensitivity * Time.deltaTime;
        transform.rotation = Quaternion.Euler(_mouse);
    }
}