using UnityEngine;
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
        private string bufferstring;

        // Use this for initialization
        void Start()
        {
            bufferstring = null;
            Vcon = GameObject.Find("VRConsole").GetComponent<VRConsole>();
            lines = Textfile.text.Split('\n');
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {

                Vcon.setText(bufferstring);
            }
            }
        }
}
