using AmazingAssets.AdvancedDissolve.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderManager : MonoBehaviour
{
    [SerializeField]
    List<Transform> playerList;

    [SerializeField]
    string[] partNames;

    [SerializeField]
    List<BreastCollider> breastColliderList;

    public Renderer mr;

    [SerializeField]
    Material material;//要替換的溶解material

    [SerializeField]
    AnimateShaderCutout animateShaderCutout;

    void Awake()
    {
        partNames = new string[] { "Cloth", "Skirt" };

        SetPlayerShaderCutout();
    }

    List<BreastCollider> GetPlayerBreastCollider(Transform player)
    {
        List<BreastCollider> currentBreastColliderList = new List<BreastCollider>();
        foreach (var item in player.GetComponentsInChildren<BreastCollider>())
        {
            currentBreastColliderList.Add(item);
        }

        return currentBreastColliderList;
    }

    void SetPlayerShaderCutout()
    {
        if (playerList.Count == 0) return;

        if (playerList.Count > 0)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                breastColliderList = GetPlayerBreastCollider(playerList[i]);

                foreach (var part in playerList[i].GetComponentsInChildren<Transform>())
                {
                    foreach (var partName in partNames)
                    {
                        if (part.name == partName)
                        {
                            //Debug.Log($"{partName}");
                            //mr = GameObject.Find("JK 1 Variant").GetComponentInChildren<SkinnedMeshRenderer>();
                            ShaderCutoutSetting(part, partName);

                            break;
                        }
                    }
                }
            }
        }
    }

    void ShaderCutoutSetting(Transform part, string partName)
    {
        mr = part.GetComponent<Renderer>();

        animateShaderCutout = mr.gameObject.GetComponent<AnimateShaderCutout>() ?? mr.gameObject.AddComponent<AnimateShaderCutout>();

        animateShaderCutout.tempMaterial = material;//幫部位增加溶解shader腳本寫入溶解材質球

        var beHitAbility = mr.gameObject.AddComponent<BeHitAbility>();

        if (partName == "Cloth")
        {
            var tempCharacterControl = animateShaderCutout.transform.parent.GetComponent<CharacterControl>();
            tempCharacterControl.animateShaderCutoutCloth = animateShaderCutout;
            tempCharacterControl.beHitAbility = beHitAbility;

            beHitAbility.HitValueCallback = x => tempCharacterControl.onHitValueEvent.Invoke(x);//初始化時幫action<int>註冊

            for (int i = 0; i < breastColliderList.Count; i++)
            {
                var breastCollider = breastColliderList[i].GetComponent<BreastCollider>();

                breastCollider.HitCallback = beHitAbility.onHitEvent.Invoke;//初始化時幫action註冊
            }
        }
        else if (partName == "Skirt")
        {
            var tempCharacterControl = animateShaderCutout.transform.parent.GetComponent<CharacterControl>();
            tempCharacterControl.animateShaderCutoutSkirt = animateShaderCutout;
        }

        breastColliderList.Clear();
    }
}