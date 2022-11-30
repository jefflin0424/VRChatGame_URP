using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float move;

    [SerializeField]
    Transform playerControl;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveControl();
    }

    void MoveControl()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerControl.position = playerControl.position + playerControl.forward * move;
        }  // 按住 上鍵 時，物件每個 frame 朝自身 z 軸方向移動 0.1 公尺
        else if (Input.GetKey(KeyCode.S))
        {
            playerControl.position = playerControl.position + playerControl.forward * -move;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            playerControl.position = playerControl.position + playerControl.right * -move;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerControl.position = playerControl.position + playerControl.right * move;
        }
    }
}
