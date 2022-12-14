
using System;
using System.Collections;
using UnityEngine;

public class BreastCollider : MonoBehaviour
{
    [SerializeField]
    Transform parentTran;

    [SerializeField]
    Animator playerAnimator;

    public SkinnedMeshRenderer meshRenderer;

    [SerializeField]
    ClothBlendShapeWeight clothBlendShapeWeight;

    public Action HitCallback;

    void Start()
    {
        playerAnimator = parentTran.GetComponent<Animator>();
    }
    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Bullet")
    //        {
    //        Debug.Log($"OnCollision");

    //        clothBlendShapeWeight.SetValue(20);
    //        //ChangeRenderMaterial();//衣服透明函式
    //    }
    //}

    void ChangeRenderMaterial()
    {
        var count = meshRenderer.materials.Length;
        for (int i = 0; i < count; i++)
        {
            var material = meshRenderer.materials[i];
            var color = material.color;
            color.a -= 0.02f;

            if (color.a <= 0.02f)
            {
                //透明值<=0.2 :透明
                color.a = 0f;//寫入參數
                material.color = color;
            }
            else
            {
                material.color = color;//新color.alpha值寫入原material.color
            }
        }
    }

    public void BeHit()
    {
        HitCallback?.Invoke();
        //StartCoroutine(OnWaitAnimation());
    }

    IEnumerator OnWaitAnimation()
    {
        playerAnimator.SetBool("Ducking", true);

        yield return new WaitForSeconds(1.5f);

        playerAnimator.SetBool("Ducking", false);
    }
}