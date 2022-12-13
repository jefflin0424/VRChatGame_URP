using AmazingAssets.AdvancedDissolve.ExampleScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderManager : MonoBehaviour
{
    public SkinnedMeshRenderer mr;

    [SerializeField]
    Material material;

    [SerializeField]
    AnimateShaderCutout animateShaderCutout;

    void Start()
    {
        mr = GameObject.Find("JK 1 Variant").GetComponentInChildren<SkinnedMeshRenderer>();

        Shader tempShader = Shader.Find("Amazing Assets/Advanced Dissolve/Universal Render Pipeline/Lit");

        animateShaderCutout = mr.gameObject.GetComponent<AnimateShaderCutout>() ?? mr.gameObject.AddComponent<AnimateShaderCutout>();
        animateShaderCutout.tempMaterial = material;

        animateShaderCutout.transform.parent.GetComponent<CharacterControl>().animateShaderCutout = animateShaderCutout;
    }
}