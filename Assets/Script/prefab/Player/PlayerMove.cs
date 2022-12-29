using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //[SerializeField] float move = 0.25f;

    [SerializeField] Transform playerTrans;

    void Start()
    {
        playerTrans = GetComponent<Transform>();
    }

    void Update()
    {
        MoveControl();
    }

    void MoveControl()
    {
        if (playerTrans == null) return;

        var playerEuler = playerTrans.rotation.eulerAngles;

        if (Input.GetKey(KeyCode.W))
        {
            playerTrans.localPosition += playerTrans.forward * Time.deltaTime * 3f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerTrans.localPosition -= playerTrans.forward * Time.deltaTime * 3f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            playerTrans.localPosition -= playerTrans.right * Time.deltaTime * 3f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerTrans.localPosition += playerTrans.right * Time.deltaTime * 3f;
        }
    }
}