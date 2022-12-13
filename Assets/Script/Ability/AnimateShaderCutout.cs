using UnityEngine;

using AmazingAssets.AdvancedDissolve;


namespace AmazingAssets.AdvancedDissolve.ExampleScripts
{
    public class AnimateShaderCutout : MonoBehaviour
    {
        [SerializeField]
        Material material;

        public Material tempMaterial;

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
            material = GetComponent<Renderer>().material;

            AdvancedDissolveProperties.Cutout.Standard.UpdateLocalProperty(material, AdvancedDissolveProperties.Cutout.Standard.Property.Clip, 0f);

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
        }

        public void StartShow(bool start)
        {
            isStart = start;
            isShow = start;
        }

        //溶解
        void ChangeShader()
        {
            var mr = GetComponent<SkinnedMeshRenderer>();
            mr.material = tempMaterial;
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
                    //timeOut = true;
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
                    //timeOut = true;
                }

                AdvancedDissolveProperties.Cutout.Standard.UpdateLocalProperty(mr.material, AdvancedDissolveProperties.Cutout.Standard.Property.Clip, clip);
            }

            time += Time.deltaTime * 3f;
        }
    }
}