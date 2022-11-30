using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterControl : MonoBehaviour
{
    [SerializeField]
    List<MultiAimConstraint> multiAimConstraintList;

    [SerializeField]
    Transform aimTarget;

    [SerializeField]
    float aimSpeed = 4f;//發現玩家時的瞄準速度(權重)

    [SerializeField]
    float detectionDistance = 3.5f;

    [SerializeField]
    Transform CharacterTrans;
    public Transform lookTrans;//玩家位置
    public Transform headTrans;//射線

    void Start()
    {
        foreach (var multiAimConstraint in multiAimConstraintList)
        {
            multiAimConstraint.weight = 0;
        }
    }

    void LateUpdate()
    {
        FindClosestPlayer();

        if (lookTrans)
        {
            //aimTarget.SetPositionAndRotation(lookTrans.position, lookTrans.rotation);//瞄準目標
            //
            foreach (var multiAimConstraint in multiAimConstraintList)
            {
                multiAimConstraint.weight = Mathf.Clamp01(multiAimConstraint.weight + aimSpeed * Time.deltaTime);
            }
        }
        else
        {
            foreach (var multiAimConstraint in multiAimConstraintList)
            {
                multiAimConstraint.weight = Mathf.Clamp01(multiAimConstraint.weight - aimSpeed * Time.deltaTime);
            }
        }

        Debug.DrawLine(headTrans.position, headTrans.position + 3.5f * headTrans.forward, Color.red);
    }

    void FindClosestPlayer()
    {
        for (int i = 0; i < multiAimConstraintList.Count; i++)
        {
            //multiAimConstraintList[i].data.sourceObjects.Clear();

            //var weightedTransformArray = multiAimConstraintList[i].data.sourceObjects;

            //for (int j = 0; j < weightedTransformArray.Count; j++)
            //{
            //    Debug.Log($"{i}:{weightedTransformArray[j].transform}");
            //}
        }

        GameObject[] objects; //要尋找的物件陣列
        string tagName = "Player";

        objects = GameObject.FindGameObjectsWithTag(tagName);//物件陣列

        GameObject closestObj = null;
        var closestDist = Mathf.Infinity;

        foreach (var obj in objects)//尋找陣列最靠近物件
        {
            var dist = (CharacterTrans.position - obj.transform.position).sqrMagnitude;//得到兩物件向量距離的平方[優點快]
            if (dist < closestDist)//新的距離 < 最近距離就把新的距離和物件存入 最近距離
            {
                closestDist = dist;
                closestObj = obj;
            }
        }

        //Debug.Log($"Distance:{closestDist}");

        if (closestDist <= detectionDistance)
        {
            for (int i = 0; i < multiAimConstraintList.Count; i++)
            {
                var weightedTransform = new WeightedTransform(closestObj.transform, 1);

                multiAimConstraintList[i].data.sourceObjects.Add(weightedTransform);

                var selectmultiAimArray = multiAimConstraintList[i].data.sourceObjects;

                for (int j = 0; j < selectmultiAimArray.Count; j++)//檢查列表
                {
                    Debug.Log($"Print:{selectmultiAimArray[j].transform}");

                    var selectWeightedTransform = selectmultiAimArray[j].transform;//有目標就停止迴圈
                    if (selectWeightedTransform != null) lookTrans = selectWeightedTransform.transform;
                }
            }
        }
        else
        {
            lookTrans = null;
        }
    }
}