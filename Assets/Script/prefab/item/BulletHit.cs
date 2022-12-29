using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    [SerializeField]
    bool oneShoot = true;

    void Start()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (oneShoot == false) return;

        if (collision.gameObject.tag == "Breast")
        {
            //Debug.Log($"OnCollision");
            //ChangeRenderMaterial();//衣服透明函式
            collision.gameObject.TryGetComponent<BreastCollider>(out var breastCollider);

            breastCollider.BeHit();

            oneShoot = false;
        }
    }
}