using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public float RayMaxDistance = 50f;
    public Transform Collider;

    public GameObject arrowprefab;
    public Transform firePoint;
    public float Force = 500f;

    public float mouseSensitivity = 300f;

    float xRotation = 0f;
    float yRotation = 0f;

    [SerializeField]
    bool isLocked;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //鎖定
        isLocked = true;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) //&& isTouch == true
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            //在距離Vector3.zero處,創建一個方向Vector3.up的Plane 若沒打這個如果沒點到物件就不能拖動
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            //Ray ray =Camera.main.ScreenPointToRay(滑鼠坐標)

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Debug.DrawRay(ray.origin, ray.direction * 100f, Color.blue);
                Collider.position = hit.point;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
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

        if (Input.GetMouseButtonDown(1)) //&& isTouch == false
        {
            GameObject newarrow = Instantiate(arrowprefab, firePoint.position, firePoint.rotation);//生成一個新物體 
            //weapon = newarrow.GetComponent<WeaponCollision>();//碰撞器=新生成箭<WeaponCollision>的collision
            //weapon.onHit.AddListener((target) => HitTarget(target));//事件同步

            Rigidbody rig = newarrow.GetComponent<Rigidbody>();
            Vector3 fwd = transform.TransformDirection(Vector3.forward);//轉換方向 前方
            rig.AddForce(fwd * Force);//給物加力，發射出去
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
        yRotation = Mathf.Clamp(yRotation, -50f, 50f); // 限制視角左右移動角度

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}