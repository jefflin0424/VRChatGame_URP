using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    float move = 0.25f;

    [SerializeField]
    Transform transform;

    void Start()
    {

    }

    void Update()
    {
        MoveControl();
    }

    void MoveControl()
    {
        var playerEuler = transform.rotation.eulerAngles;        

        if (Input.GetKey(KeyCode.W))
        {
            transform.localPosition += transform.forward * Time.deltaTime * 3f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition -= transform.forward * Time.deltaTime * 3f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition -= transform.right * Time.deltaTime * 3f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += transform.right * Time.deltaTime * 3f;
        }
    }
}