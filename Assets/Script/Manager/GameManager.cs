using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);    
    }

    void Start()
    {
        var findObjects = FindObjectOfType<DynamicBone>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
