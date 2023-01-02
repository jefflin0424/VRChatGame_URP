using UnityEngine;
using Unity.Netcode;

public class NetworkPlayerAttack : NetworkBehaviour
{
    [Header("ShootModeSetting")]
    [SerializeField] float RayMaxDistance = 50f, bulletDuration = 1.2f, timer = 0f;
    [SerializeField] Transform _touchCollider, arrowprefab, firePoint;

    [Header("TouchModeSetting")]
    [SerializeField]
    Texture2D cursorTexture, cursorTexture2;

    public CursorMode cursorMode = CursorMode.Auto;

    public Vector2 hotSpot = Vector2.zero;

    [SerializeField]
    LayerMask targetMark;

    /*
    private void Awake()
    {
        touchCollider = GameObject.Find("TouchCollider").transform;
    }
    */

    public override void OnNetworkSpawn()
    {
        timer = 0f;
    }

    void Update()
    {
        if (!IsOwner) return;

        //TouchStateDetection();

        if (Input.GetMouseButton(1))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                ShootServerRpc(); //request a clinetRpc shoot
                Shoot(); //shoot locally
            }
        }
        else
        {
            timer = 0f; //放開射擊計時歸0
        }
    }
    /*
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
                    _touchCollider.position = hit.point;
                }
            }
        }
        else if (Cursor.lockState == CursorLockMode.Locked) Cursor.SetCursor(null, hotSpot, cursorMode);//恢復預設遊標
    }
    */
    [ServerRpc]
    void ShootServerRpc()
    {
        ShootClientRpc();
    }

    [ClientRpc]
    void ShootClientRpc()
    {
        if (!IsOwner) Shoot();
    }

    private void Shoot()
    {
        if (timer <= 0f)
        {
            GameObject newarrow = Instantiate(arrowprefab.gameObject, firePoint.position, firePoint.rotation);
            timer = bulletDuration;
        }
        timer -= Time.deltaTime;
    }
}