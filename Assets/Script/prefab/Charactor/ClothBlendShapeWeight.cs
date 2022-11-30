using UnityEngine;

public class ClothBlendShapeWeight : MonoBehaviour
{
    [SerializeField]
    SkinnedMeshRenderer clothRenderer;

    [SerializeField]
    RagdollControll ragdollControll;

    void Start()
    {
        //ragdollControll = GetComponent<RagdollControll>();
    }

    public void SetValue(float damage)
    {
        for (int i = 1; i < 3 ; i++)
        {
            var floatValue = clothRenderer.GetBlendShapeWeight(i);

            if (floatValue < 200)
            { 
                floatValue += damage;
            }else
            {
                //ragdollControll.ToggleRagdoll(true);
            }

            clothRenderer.SetBlendShapeWeight(i, floatValue);
            Debug.Log($"{clothRenderer.GetBlendShapeWeight(i)}");
        }
    }
}