using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    [SerializeField]
    bool oneShoot = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (oneShoot) Shoot();
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 2f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //Vector3.forward =方向
            Debug.Log($"{hit.collider.tag}");
            //if (hit.collider.tag == "Breast")
            //{
            //    var clothColliderControl = hit.collider.GetComponent<ClothBlendShapeWeight>();
            //    clothColliderControl.SetValue(10);
            //}
        }

        oneShoot = false;
    }
}
