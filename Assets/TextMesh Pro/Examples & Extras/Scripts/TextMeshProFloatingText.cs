using System.Collections;
using UnityEngine;
using TMPro_ExtensionMethods = TMPro.TMPro_ExtensionMethods;

namespace TextMesh_Pro.Scripts
{
    
    public class TextMeshProFloatingText : MonoBehaviour
    {
        public Font TheFont;

        private GameObject m_floatingText;
        private TMPro.TextMeshPro m_textMeshPro;
        private TextMesh m_textMesh;

        private Transform m_transform;
        private Transform m_floatingText_Transform;
        private Transform m_cameraTransform;

        Vector3 lastPOS = Vector3.zero;
        Quaternion lastRotation = Quaternion.identity;

        public int SpawnType;

        //private int m_frame = 0;

        void Awake()
        {
            this.m_transform = this.transform;
            this.m_floatingText = new GameObject(this.name + " floating text");

            // Reference to Transform is lost when TMP component is added since it replaces it by a RectTransform.
            //m_floatingText_Transform = m_floatingText.transform;
            //m_floatingText_Transform.position = m_transform.position + new Vector3(0, 15f, 0);

            this.m_cameraTransform = Camera.main.transform;
        }

        void Start()
        {
            if (this.SpawnType == 0)
            {
                // TextMesh Pro Implementation
                this.m_textMeshPro = this.m_floatingText.AddComponent<TMPro.TextMeshPro>();
                this.m_textMeshPro.rectTransform.sizeDelta = new Vector2(3, 3);
                
                this.m_floatingText_Transform = this.m_floatingText.transform;
                this.m_floatingText_Transform.position = this.m_transform.position + new Vector3(0, 15f, 0);

                //m_textMeshPro.fontAsset = Resources.Load("Fonts & Materials/JOKERMAN SDF", typeof(TextMeshProFont)) as TextMeshProFont; // User should only provide a string to the resource.
                //m_textMeshPro.fontSharedMaterial = Resources.Load("Fonts & Materials/LiberationSans SDF", typeof(Material)) as Material;

                this.m_textMeshPro.alignment = TMPro.TextAlignmentOptions.Center;
                this.m_textMeshPro.color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
                this.m_textMeshPro.fontSize = 24;
                //m_textMeshPro.enableExtraPadding = true;
                //m_textMeshPro.enableShadows = false;
                this.m_textMeshPro.enableKerning = false;
                this.m_textMeshPro.text = string.Empty;

                this.StartCoroutine(this.DisplayTextMeshProFloatingText());
            }
            else if (this.SpawnType == 1)
            {
                //Debug.Log("Spawning TextMesh Objects.");

                this.m_floatingText_Transform = this.m_floatingText.transform;
                this.m_floatingText_Transform.position = this.m_transform.position + new Vector3(0, 15f, 0);

                this.m_textMesh = this.m_floatingText.AddComponent<TextMesh>();
                this.m_textMesh.font = Resources.Load<Font>("Fonts/ARIAL");
                this.m_textMesh.GetComponent<Renderer>().sharedMaterial = this.m_textMesh.font.material;
                this.m_textMesh.color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
                this.m_textMesh.anchor = TextAnchor.LowerCenter;
                this.m_textMesh.fontSize = 24;

                this.StartCoroutine(this.DisplayTextMeshFloatingText());
            }
            else if (this.SpawnType == 2)
            {

            }

        }


        //void Update()
        //{
        //    if (SpawnType == 0)
        //    {
        //        m_textMeshPro.SetText("{0}", m_frame);
        //    }
        //    else
        //    {
        //        m_textMesh.text = m_frame.ToString();
        //    }
        //    m_frame = (m_frame + 1) % 1000;

        //}


        public IEnumerator DisplayTextMeshProFloatingText()
        {
            float CountDuration = 2.0f; // How long is the countdown alive.    
            float starting_Count = Random.Range(5f, 20f); // At what number is the counter starting at.
            float current_Count = starting_Count;

            Vector3 start_pos = this.m_floatingText_Transform.position;
            Color32 start_color = this.m_textMeshPro.color;
            float alpha = 255;
            int int_counter = 0;


            float fadeDuration = 3 / starting_Count * CountDuration;

            while (current_Count > 0)
            {
                current_Count -= (Time.deltaTime / CountDuration) * starting_Count;

                if (current_Count <= 3)
                {
                    //Debug.Log("Fading Counter ... " + current_Count.ToString("f2"));
                    alpha = Mathf.Clamp(alpha - (Time.deltaTime / fadeDuration) * 255, 0, 255);
                }

                int_counter = (int)current_Count;
                this.m_textMeshPro.text = int_counter.ToString();
                //m_textMeshPro.SetText("{0}", (int)current_Count);

                this.m_textMeshPro.color = new Color32(start_color.r, start_color.g, start_color.b, (byte)alpha);

                // Move the floating text upward each update
                this.m_floatingText_Transform.position += new Vector3(0, starting_Count * Time.deltaTime, 0);

                // Align floating text perpendicular to Camera.
                if (!TMPro_ExtensionMethods.Compare(this.lastPOS, this.m_cameraTransform.position, 1000) || !TMPro_ExtensionMethods.Compare(this.lastRotation, this.m_cameraTransform.rotation, 1000))
                {
                    this.lastPOS = this.m_cameraTransform.position;
                    this.lastRotation = this.m_cameraTransform.rotation;
                    this.m_floatingText_Transform.rotation = this.lastRotation;
                    Vector3 dir = this.m_transform.position - this.lastPOS;
                    this.m_transform.forward = new Vector3(dir.x, 0, dir.z);
                }

                yield return new WaitForEndOfFrame();
            }

            //Debug.Log("Done Counting down.");

            yield return new WaitForSeconds(Random.Range(0.1f, 1.0f));

            this.m_floatingText_Transform.position = start_pos;

            this.StartCoroutine(this.DisplayTextMeshProFloatingText());
        }


        public IEnumerator DisplayTextMeshFloatingText()
        {
            float CountDuration = 2.0f; // How long is the countdown alive.    
            float starting_Count = Random.Range(5f, 20f); // At what number is the counter starting at.
            float current_Count = starting_Count;

            Vector3 start_pos = this.m_floatingText_Transform.position;
            Color32 start_color = this.m_textMesh.color;
            float alpha = 255;
            int int_counter = 0;

            float fadeDuration = 3 / starting_Count * CountDuration;

            while (current_Count > 0)
            {
                current_Count -= (Time.deltaTime / CountDuration) * starting_Count;

                if (current_Count <= 3)
                {
                    //Debug.Log("Fading Counter ... " + current_Count.ToString("f2"));
                    alpha = Mathf.Clamp(alpha - (Time.deltaTime / fadeDuration) * 255, 0, 255);
                }

                int_counter = (int)current_Count;
                this.m_textMesh.text = int_counter.ToString();
                //Debug.Log("Current Count:" + current_Count.ToString("f2"));

                this.m_textMesh.color = new Color32(start_color.r, start_color.g, start_color.b, (byte)alpha);

                // Move the floating text upward each update
                this.m_floatingText_Transform.position += new Vector3(0, starting_Count * Time.deltaTime, 0);

                // Align floating text perpendicular to Camera.
                if (!TMPro_ExtensionMethods.Compare(this.lastPOS, this.m_cameraTransform.position, 1000) || !TMPro_ExtensionMethods.Compare(this.lastRotation, this.m_cameraTransform.rotation, 1000))
                {
                    this.lastPOS = this.m_cameraTransform.position;
                    this.lastRotation = this.m_cameraTransform.rotation;
                    this.m_floatingText_Transform.rotation = this.lastRotation;
                    Vector3 dir = this.m_transform.position - this.lastPOS;
                    this.m_transform.forward = new Vector3(dir.x, 0, dir.z);
                }



                yield return new WaitForEndOfFrame();
            }

            //Debug.Log("Done Counting down.");

            yield return new WaitForSeconds(Random.Range(0.1f, 1.0f));

            this.m_floatingText_Transform.position = start_pos;

            this.StartCoroutine(this.DisplayTextMeshFloatingText());
        }
    }
}