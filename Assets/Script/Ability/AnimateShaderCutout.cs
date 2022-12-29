using UnityEngine;

namespace AmazingAssets.AdvancedDissolve.ExampleScripts
{
    public class AnimateShaderCutout : MonoBehaviour
    {
        Renderer mr;

        [SerializeField]
        Material orginMaterial;//原始材質球

        public Material tempMaterial;//溶解材質球
        [SerializeField] 
        Texture orginTexture;//原始貼圖

        float offset;
        [SerializeField]
        float speed;

        float time = 0;

        [SerializeField]
        float clip;

        bool isStart = false;

        bool inHide = false;

        bool isShow = false;

        private void Start()
        {
            orginMaterial = GetComponent<Renderer>().material;

            orginTexture = orginMaterial.mainTexture;

            //speed = Random.Range(0.1f, 0.2f);
            speed = 0.12f;
        }

        void Update()
        {
            if (isStart) ChangeShader();
        }

        public void StartHide(bool start)
        {
            isStart = start;
            inHide = start;

            InitShaderCutout();
        }

        public void StartShow(bool start)
        {
            isStart = start;
            isShow = start;

            InitShaderCutout();
        }

        //溶解
        void ChangeShader()
        {
            //clip:1f透明 0.01不透明
            if (inHide)
            {
                offset = 0.1f;
                clip = offset + time * speed;

                if (clip >= 1f)
                {
                    time = 0f;
                    isStart = false;
                    inHide = false;

                    AdvancedDissolveProperties.Cutout.Standard.UpdateLocalProperty(mr.material, AdvancedDissolveProperties.Cutout.Standard.Property.Clip, 1f);
                }

                AdvancedDissolveProperties.Cutout.Standard.UpdateLocalProperty(mr.material, AdvancedDissolveProperties.Cutout.Standard.Property.Clip, clip);
            }
            else if (isShow)
            {
                offset = 1.0f;
                clip = offset - time * speed;

                if (clip <= 0f)
                {
                    time = 0f;
                    isStart = false;
                    isShow = false;

                    AdvancedDissolveProperties.Cutout.Standard.UpdateLocalProperty(mr.material, AdvancedDissolveProperties.Cutout.Standard.Property.Clip, 0f);
                }

                AdvancedDissolveProperties.Cutout.Standard.UpdateLocalProperty(mr.material, AdvancedDissolveProperties.Cutout.Standard.Property.Clip, clip);
            }

            time += Time.deltaTime * 3f;
        }

        void InitShaderCutout()
        {
            mr = GetComponent<Renderer>();

            mr.material = tempMaterial;//替換溶解材質球
           if (mr.material.HasProperty("_BaseMap")) mr.material.SetTexture("_BaseMap", orginTexture);//溶解材質球換原本貼圖
        }
    }
}