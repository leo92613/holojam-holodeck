using UnityEngine;
using System;
using System.Collections;
using System.Text.RegularExpressions;


namespace Holojam.Tools
{
    [ExecuteInEditMode]
    public class scriptcontroller : MonoBehaviour
    {
        public TextAsset Textfile;
        public string[] lines;
        [SerializeField]
        private VRConsole Vcon;
        private int cursor = 0;

        // Use this for initialization
        void Start()
        {
            Vcon = GameObject.Find("VRConsole").GetComponent<VRConsole>();
            lines = Textfile.text.Split('\n');
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Mouse0))
            //if (Input.GetKeyDown("space"))
            {
                GameObject.Find("VRConsole").GetComponent<MeshRenderer>().enabled = true;
                if (cursor + 1 < lines.Length)
                    Vcon.setText(String.Concat(lines[cursor], '\n', lines[++cursor]));
                else
                    Vcon.setText(lines[cursor]);
            }
            }
        }
}
