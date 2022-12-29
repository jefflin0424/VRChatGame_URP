using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollider : MonoBehaviour
{
    [SerializeField]
    Transform parentTran;

    [SerializeField]
    Animator playerAnimator;

    void Start()
    {
        playerAnimator = parentTran.GetComponent<Animator>();
    }

    public void BeHit()
    {
        Invoke("StartShowAnimation", 1f);
    }

    void StartShowAnimation()
    {
        StartCoroutine(OnWaitAnimation());
    }

    IEnumerator OnWaitAnimation()
    {
        playerAnimator.SetBool("inTouchHead", true);

        yield return new WaitForSeconds(0.3f);

        playerAnimator.SetBool("inTouchHead", false);
    }
}