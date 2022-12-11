using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //[SerializeField] float move = 0.25f;

    [SerializeField] Transform _transform;

    void Start()
    {

    }

    void Update()
    {
        MoveControl();
    }

    void MoveControl()
    {
        var playerEuler = _transform.rotation.eulerAngles;        

        if (Input.GetKey(KeyCode.W))
        {
            _transform.localPosition += _transform.forward * Time.deltaTime * 3f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _transform.localPosition -= _transform.forward * Time.deltaTime * 3f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _transform.localPosition -= _transform.right * Time.deltaTime * 3f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            _transform.localPosition += _transform.right * Time.deltaTime * 3f;
        }
    }
}