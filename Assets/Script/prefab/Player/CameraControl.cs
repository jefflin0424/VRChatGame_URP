using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float Force = 10f;

    public float mouseSensitivity = 300f;

    float xRotation = 0f;
    float yRotation = 0f;

    [SerializeField]
    bool isLocked;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //隱藏遊標

        isLocked = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                isLocked = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                isLocked = true;
            }
        }

        if (isLocked == true) cameraRotation(); //攝影機跟隨滑鼠
    }

    void cameraRotation()
    {
        //旋轉攝影機的視角
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f); // 限制視角上下移動角度

        yRotation += mouseX;
        //yRotation = Mathf.Clamp(yRotation, -50f, 50f); // 限制視角左右移動角度

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}