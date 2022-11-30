using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RagdollControll : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    Animator charaAnimator;

    [SerializeField]
    List<CharacterJoint> CharacterJointList;

    [SerializeField]
    List<Rigidbody> ragdollBodyList;

    [SerializeField]
    List<Collider> ragdollColliderList;

    void Awake()
    {
        
    }

    void Start()
    {
        charaAnimator = GetComponent<Animator>();

        var characterJointList = GetComponentsInChildren<CharacterJoint>();

        for (int i = 0; i < characterJointList.Length; i++)
        {
            var selectCharacterJoint = characterJointList[i];

            var selectRigidbody = selectCharacterJoint.GetComponent<Rigidbody>();
            ragdollBodyList.Add(selectRigidbody);

            var selectCollider = selectCharacterJoint.GetComponent<Collider>();
            ragdollColliderList.Add(selectCollider);
        }

        ToggleRagdoll(false);//布娃娃系統開關切換 一開始關
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            ToggleRagdoll(true);
        }
    }

    public void ToggleRagdoll(bool state)
    {
        

        foreach (var ragdollBodyItem in ragdollBodyList)
        {
            //ragdollBodyItem.isKinematic = !state;//開/關布娃娃系統
        }

        foreach (var ragdollColliderItem in ragdollColliderList)
        {
            ragdollColliderItem.enabled = state;//開/關碰撞collider
        }

        //下一幀開啓/關閉animator
        StartCoroutine(SetCharaAnimator(state));
    }

    IEnumerator SetCharaAnimator(bool enable)
    {
        yield return new WaitForEndOfFrame();//等到這幀結束後執行
        charaAnimator.enabled = !enable;//動畫關/開
    }

    public void DieState()
    {
        Invoke(nameof(DieState), 3f);

        ToggleRagdoll(true);
    }
}