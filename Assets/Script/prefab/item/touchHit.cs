using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchHit : MonoBehaviour
{
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Head")
        {
            collision.gameObject.TryGetComponent<HeadCollider>(out var headCollider);

            headCollider.BeHit();
        }
    }
}