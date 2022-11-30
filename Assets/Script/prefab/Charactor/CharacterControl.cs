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
    float aimSpeed = 4f;//�o�{���a�ɪ��˷ǳt��(�v��)

    [SerializeField]
    float detectionDistance = 3.5f;

    [SerializeField]
    Transform CharacterTrans;
    public Transform lookTrans;//���a��m
    public Transform headTrans;//�g�u

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
            //aimTarget.SetPositionAndRotation(lookTrans.position, lookTrans.rotation);//�˷ǥؼ�
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

        GameObject[] objects; //�n�M�䪺����}�C
        string tagName = "Player";

        objects = GameObject.FindGameObjectsWithTag(tagName);//����}�C

        GameObject closestObj = null;
        var closestDist = Mathf.Infinity;

        foreach (var obj in objects)//�M��}�C�̾a�񪫥�
        {
            var dist = (CharacterTrans.position - obj.transform.position).sqrMagnitude;//�o��⪫��V�q�Z��������[�u�I��]
            if (dist < closestDist)//�s���Z�� < �̪�Z���N��s���Z���M����s�J �̪�Z��
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

                for (int j = 0; j < selectmultiAimArray.Count; j++)//�ˬd�C��
                {
                    Debug.Log($"Print:{selectmultiAimArray[j].transform}");

                    var selectWeightedTransform = selectmultiAimArray[j].transform;//���ؼдN����j��
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