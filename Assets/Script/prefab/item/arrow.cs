using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public DynamicBone[] allDynamicBone;
    public DynamicBoneCollider boneCollider;

    void Start()
    {
        boneCollider = GetComponent<DynamicBoneCollider>();
        allDynamicBone = FindObjectsOfType<DynamicBone>();
        foreach (var obj in allDynamicBone)
        {
            //obj.m_Colliders.foreach (var item in collection)
            //{
            //    if (item == null || item == "none")
            //    {
            //        obj.m_Colliders.Remove(item);
            //    }
            //}

            obj.m_Colliders.Add(boneCollider);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        StartCoroutine(Shoot());

    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(1f);//等1幀

        foreach (var obj in allDynamicBone)
        {
            obj.m_Colliders.Remove(boneCollider);
        }
        Destroy(gameObject, 2f);
    }

}
