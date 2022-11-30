using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAbility : MonoBehaviour
{
    [SerializeField]
    Vector3 direction = new Vector3(0, 0, 1);

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fly(0.01f);
    }

    void Fly(float deltaTime)
    {
        var trans = transform;//目前先設此物件，未來再改控父物件
        trans.Translate(direction * deltaTime, Space.Self);
    }
}
