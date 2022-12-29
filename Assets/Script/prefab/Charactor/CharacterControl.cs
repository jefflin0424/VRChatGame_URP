using AmazingAssets.AdvancedDissolve.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterControl : MonoBehaviour
{
    public BeHitAbility beHitAbility;

    public AnimateShaderCutout animateShaderCutoutCloth;

    public AnimateShaderCutout animateShaderCutoutSkirt;

    [SerializeField] int clothCurrentHP;

    [SerializeField] int clothMaxHP = 100;

    [SerializeField] int skirtCurrentHP;

    [SerializeField] int skirtMaxHP = 100;

    public UnityEvent<int> onHitValueEvent = new UnityEvent<int>();

    void Start()
    {
        clothCurrentHP = clothMaxHP;
        skirtCurrentHP = skirtMaxHP;

        onHitValueEvent.AddListener(TakeDamage);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (animateShaderCutoutCloth != null) animateShaderCutoutCloth.StartHide(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (animateShaderCutoutSkirt != null) animateShaderCutoutSkirt.StartHide(true);
        }
    }

    void TakeDamage(int damage)
    {
        if (clothCurrentHP == 0) return;

        clothCurrentHP -= damage;

        if (clothCurrentHP < 0)
        {
            clothCurrentHP = 0;

            animateShaderCutoutCloth.StartHide(true);
        }
    }
}