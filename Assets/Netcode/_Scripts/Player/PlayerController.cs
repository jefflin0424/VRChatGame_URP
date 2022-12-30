using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour {
    [SerializeField] private float _speed = 3, _mouseSensitivity = 100;
    [SerializeField] Vector2 spawnArea = new Vector2(5, 5);
    private Rigidbody _rb;
    [SerializeField] GameObject _camera, _xrPlayer, _xrPerformer;
    Vector3 _mousePos;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();

        //_camera = GameObject.Find("Main Camera");

        _mousePos = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
    }

    private void Update() {
        //var dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //_rb.velocity = dir * _speed;

        if (Input.GetKey(KeyCode.W)) transform.position += transform.forward * _speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S)) transform.position -= transform.forward * _speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) transform.position -= transform.right * _speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D)) transform.position += transform.right * _speed * Time.deltaTime;

        //_camera.transform.position = transform.position;
        //_camera.transform.rotation = transform.rotation;

        _mousePos += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * _mouseSensitivity * Time.deltaTime;
        transform.rotation = Quaternion.Euler(_mousePos);
    }

    public override void OnNetworkSpawn() {
        if (IsHost)
        {
            Instantiate(_xrPlayer);
            gameObject.SetActive(false);
        }
        else
        {
            Instantiate(_xrPerformer);
        }

        if (!IsOwner)
        {
            Destroy(_camera);
            Destroy(this);
        }
        transform.position = new Vector3(Random.Range(spawnArea.x, spawnArea.y), 0, Random.Range(spawnArea.x, spawnArea.y));
    }
}