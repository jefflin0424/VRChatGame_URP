using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public float RayMaxDistance = 50f;
    public Transform Collider;

    public GameObject arrowprefab;
    public Transform firePoint;

    public bool keepShoot = false;

    float timer = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) //&& isTouch == true
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            //在距離Vector3.zero處,創建一個方向Vector3.up的Plane 若沒打這個如果沒點到物件就不能拖動
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //攝影機參考遺失要在start時重get
            RaycastHit hit;
            //Ray ray =Camera.main.ScreenPointToRay(滑鼠坐標)

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                Debug.DrawRay(ray.origin, ray.direction * 100f, Color.blue);
                Collider.position = hit.point;
            }
        }

        if (Input.GetMouseButtonDown(1)) //&& isTouch == false
        {
            Shoot();
        }
        else if (keepShoot)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (keepShoot) timer += Time.deltaTime;

        if (timer >= 2f && keepShoot)
        {
            GameObject newarrow = Instantiate(arrowprefab, firePoint.position, firePoint.rotation);//生成一個新物體
            timer = 0f;//執行完歸0

            //weapon = newarrow.GetComponent<WeaponCollision>();//碰撞器=新生成箭<WeaponCollision>的collision
            //weapon.onHit.AddListener((target) => HitTarget(target));//事件同步

            //不給子彈力改由子彈往前行
            //Rigidbody rig = newarrow.GetComponent<Rigidbody>();
            //Vector3 fwd = transform.TransformDirection(Vector3.forward);//轉換方向 前方
            //rig.AddForce(fwd * Force);//給物加力，發射出去
        }
        else if (!keepShoot)
        {
            GameObject newarrow = Instantiate(arrowprefab, firePoint.position, firePoint.rotation);//生成一個新物體
        }
    }
}
