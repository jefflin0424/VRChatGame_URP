using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("ShootModeSetting")]
    public float RayMaxDistance = 50f;
    public Transform touchCollider;

    public GameObject arrowprefab;
    public Transform firePoint;

    [SerializeField]
    float timer = 0f;

    [Header("TouchModeSetting")]
    [SerializeField]
    Texture2D cursorTexture;

    [SerializeField]
    Texture2D cursorTexture2;

    public CursorMode cursorMode = CursorMode.Auto;

    public Vector2 hotSpot = Vector2.zero;

    [SerializeField]
    LayerMask targetMark;

    private void Awake()
    {
        touchCollider = GameObject.Find("TouchCollider").transform;
    }

    void Start()
    {
        timer = 0f;//初始化秒數
    }

    void Update()
    {
        TouchStateDetection();

        if (Input.GetMouseButton(1))
        {
            Shoot();
        }
        else
        {
            timer = 0f;//放開射擊計時歸0
        }
    }

    void TouchStateDetection()
    {
        if (Cursor.lockState == CursorLockMode.None)
        {
            Ray rayTarget = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitTarget;

            if (Physics.Raycast(rayTarget.origin, rayTarget.direction * 100f, out hitTarget, targetMark))
            {
                Debug.DrawRay(rayTarget.origin, rayTarget.direction * 100f, Color.red);

                if (hitTarget.collider.gameObject.layer == LayerMask.NameToLayer("Target"))
                {
                    Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);//設定觸摸遊標
                }
                else
                {
                    Cursor.SetCursor(cursorTexture2, hotSpot, cursorMode);
                }
            }

            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //Ray ray =Camera.main.ScreenPointToRay(滑鼠坐標)
                //攝影機參考遺失要在start時重get
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    Debug.DrawRay(ray.origin, ray.direction * 100f, Color.blue);
                    touchCollider.position = hit.point;
                }
            }
        }
        else if (Cursor.lockState == CursorLockMode.Locked) Cursor.SetCursor(null, hotSpot, cursorMode);//恢復預設遊標
    }

    void Shoot()
    {
        if (timer <= 0f)
        {
            GameObject newarrow = Instantiate(arrowprefab, firePoint.position, firePoint.rotation);//生成一個新物體
            timer = 1.5f;//執行完歸0
        }

        timer -= Time.deltaTime;
    }
}