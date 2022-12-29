using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicaColliderManager : MonoBehaviour
{
    [SerializeField]
    DynamicBoneCollider touchDynamicBoneCollider;

    [SerializeField]
    List<DynamicBone> dynamicBoneList;

    void Start()
    {
        var dynamicBoneItems = FindObjectsOfType<DynamicBone>();

        foreach (var item in dynamicBoneItems)
        {
            item.m_Colliders.Add(touchDynamicBoneCollider);

            dynamicBoneList.Add(item);
        }
    }
}