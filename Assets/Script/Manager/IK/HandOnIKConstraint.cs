using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandOnIKConstraint : MonoBehaviour
{
    [SerializeField]
    Transform IKTarget;

    public Transform traceTarget;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (traceTarget != null)
        {
            IKTarget.position = traceTarget.position;
            IKTarget.rotation = traceTarget.rotation;
        }
            
    }
}
