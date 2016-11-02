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
		public float dis;
		public int dir;
        [SerializeField]
        private VRConsole Vcon;
        private int cursor = 0;
		private float origin = -2f;
		private float timethreshold = 0.5f;
		private float timer = 0;
		Holojam.Tools.Phonecontroller phonecontroller;

        // Use this for initialization
        void Start()
        {
			phonecontroller = GameObject.Find ("Controller").gameObject.GetComponent<Holojam.Tools.Phonecontroller> ();
            Vcon = GameObject.Find("VRConsole").GetComponent<VRConsole>();
            lines = Textfile.text.Split('\n');
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Mouse0))
            //if (Input.GetKeyDown("space"))
            {
				cursor = 0;
				SetText (0);
            }

			if (phonecontroller.z > 0.1) {
				timer += Time.deltaTime;
				if (origin == -2f)
					origin = phonecontroller.y;
				dis = Mathf.Abs (phonecontroller.y - origin);
				if (dis < 0.1f)
					dis = 0.1f;
				timethreshold = 0.5f / dis;
				if (phonecontroller.y - origin < 0)
					dir = 1;
				else
					dir = -1;
				if (timer > timethreshold) {
					SetText (dir);
					timer = 0f;
				}
			} else {
				origin = -2f;
			}


          }


		void SetText(int dir){
			GameObject.Find("VRConsole").GetComponent<MeshRenderer>().enabled = true;

			if (dir >= 0) {
				if (cursor + 1 < lines.Length) {
					Vcon.setText (String.Concat (lines [cursor], '\n', lines [++cursor]));
				} else
					Vcon.setText (lines [cursor]);
			} else {
				if (cursor > 0)
					Vcon.setText (String.Concat (lines [--cursor], '\n', lines [cursor+1]));
				else
					Vcon.setText (lines [cursor]);
			}
		}
        }
}
