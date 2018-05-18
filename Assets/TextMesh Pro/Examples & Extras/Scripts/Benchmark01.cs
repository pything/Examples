using System.Collections;
using UnityEngine;

namespace TextMesh_Pro.Scripts
{
    
    public class Benchmark01 : MonoBehaviour
    {

        public int BenchmarkType = 0;

        public TMPro.TMP_FontAsset TMProFont;
        public Font TextMeshFont;

        private TMPro.TextMeshPro m_textMeshPro;
        private TMPro.TextContainer m_textContainer;
        private TextMesh m_textMesh;

        private const string label01 = "The <#0050FF>count is: </color>{0}";
        private const string label02 = "The <color=#0050FF>count is: </color>";

        //private string m_string;
        //private int m_frame;

        private Material m_material01;
        private Material m_material02;



        IEnumerator Start()
        {



            if (this.BenchmarkType == 0) // TextMesh Pro Component
            {
                this.m_textMeshPro = this.gameObject.AddComponent<TMPro.TextMeshPro>();
                this.m_textMeshPro.autoSizeTextContainer = true;

                //m_textMeshPro.anchorDampening = true;

                if (this.TMProFont != null) {
                    this.m_textMeshPro.font = this.TMProFont;
                }

                //m_textMeshPro.font = Resources.Load("Fonts & Materials/Anton SDF", typeof(TextMeshProFont)) as TextMeshProFont; // Make sure the Anton SDF exists before calling this...
                //m_textMeshPro.fontSharedMaterial = Resources.Load("Fonts & Materials/Anton SDF", typeof(Material)) as Material; // Same as above make sure this material exists.

                this.m_textMeshPro.fontSize = 48;
                this.m_textMeshPro.alignment = TMPro.TextAlignmentOptions.Center;
                //m_textMeshPro.anchor = AnchorPositions.Center;
                this.m_textMeshPro.extraPadding = true;
                //m_textMeshPro.outlineWidth = 0.25f;
                //m_textMeshPro.fontSharedMaterial.SetFloat("_OutlineWidth", 0.2f);
                //m_textMeshPro.fontSharedMaterial.EnableKeyword("UNDERLAY_ON");
                //m_textMeshPro.lineJustification = LineJustificationTypes.Center;
                this.m_textMeshPro.enableWordWrapping = false;    
                //m_textMeshPro.lineLength = 60;          
                //m_textMeshPro.characterSpacing = 0.2f;
                //m_textMeshPro.fontColor = new Color32(255, 255, 255, 255);

                this.m_material01 = this.m_textMeshPro.font.material;
                this.m_material02 = Resources.Load<Material>("Fonts & Materials/LiberationSans SDF - Drop Shadow"); // Make sure the LiberationSans SDF exists before calling this...  


            }
            else if (this.BenchmarkType == 1) // TextMesh
            {
                this.m_textMesh = this.gameObject.AddComponent<TextMesh>();

                if (this.TextMeshFont != null)
                {
                    this.m_textMesh.font = this.TextMeshFont;
                    this.m_textMesh.GetComponent<Renderer>().sharedMaterial = this.m_textMesh.font.material;
                }
                else
                {
                    this.m_textMesh.font = Resources.Load("Fonts/ARIAL", typeof(Font)) as Font;
                    this.m_textMesh.GetComponent<Renderer>().sharedMaterial = this.m_textMesh.font.material;
                }

                this.m_textMesh.fontSize = 48;
                this.m_textMesh.anchor = TextAnchor.MiddleCenter;

                //m_textMesh.color = new Color32(255, 255, 0, 255);
            }



            for (int i = 0; i <= 1000000; i++)
            {
                if (this.BenchmarkType == 0)
                {
                    this.m_textMeshPro.SetText(label01, i % 1000);
                    if (i % 1000 == 999) {
                        this.m_textMeshPro.fontSharedMaterial = this.m_textMeshPro.fontSharedMaterial == this.m_material01 ? this.m_textMeshPro.fontSharedMaterial = this.m_material02 : this.m_textMeshPro.fontSharedMaterial = this.m_material01;
                    }
                }
                else if (this.BenchmarkType == 1) {
                    this.m_textMesh.text = label02 + (i % 1000).ToString();
                }

                yield return null;
            }


            yield return null;
        }


        /*
        void Update()
        {
            if (BenchmarkType == 0)
            {
                m_textMeshPro.text = (m_frame % 1000).ToString();
            }
            else if (BenchmarkType == 1)
            {
                m_textMesh.text = (m_frame % 1000).ToString();
            }

            m_frame += 1;
        }
        */
    }
}