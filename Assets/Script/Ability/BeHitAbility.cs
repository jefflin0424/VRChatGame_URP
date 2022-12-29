using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeHitAbility : MonoBehaviour
{
    int damage;

    [SerializeField]
    Material partMaterial;

    [SerializeField]
    Color _emissionColorValue;

    public UnityEvent onHitEvent = new UnityEvent();

    public Action<int> HitValueCallback;
    void Start()
    {
        _emissionColorValue = new Color(255, 0, 0, 1f);

        onHitEvent.AddListener(Hit);

        partMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {

    }

    public void Hit()
    {
        //Debug.Log($"BeHitAbility Call:PlayerName{transform.parent.name}");

        damage = HitDamageValue();

        HitValueCallback.Invoke(damage);

        HitFlash();
    }

    int HitDamageValue()
    {
        var value = 0;
        value = 15;

        return value;
    }

    public void HitFlash()
    {
        StartCoroutine(hitFlash());
    }

    IEnumerator hitFlash()
    {
        partMaterial.SetVector("_EmissionColor", _emissionColorValue * 1f);

        yield return new WaitForSeconds(0.1f);

        partMaterial.SetVector("_EmissionColor", _emissionColorValue * 0f);
    }
}