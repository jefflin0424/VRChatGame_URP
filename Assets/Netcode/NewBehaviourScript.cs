using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Vector3 _mouse;
    void Start()
    {
        _mouse = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
    }

    void Update()
    {
        _mouse += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * 300 * Time.deltaTime;
        transform.rotation = Quaternion.Euler(_mouse);
    }
}
