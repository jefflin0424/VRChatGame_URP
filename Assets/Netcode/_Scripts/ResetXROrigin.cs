using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetXROrigin : MonoBehaviour
{
    [SerializeField] GameObject mainCam;
    [SerializeField] bool test;

    private void Start()
    {
        //gameObject.transform.localEulerAngles = new Vector3(0, 90 + mainCam.transform.localEulerAngles.y, 0);
    }

    void Update()
    {
        if (test)
        {
            gameObject.transform.position = new Vector3(-mainCam.transform.position.x,0,0);
            gameObject.transform.localEulerAngles = new Vector3(0,-mainCam.transform.localEulerAngles.y,0);
            test = false;
        }
    }
}
