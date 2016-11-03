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
		public Transform center;
		public VRConsole[] Vcon;
		public int top, bottom,previous,next;
		public float t;
		[SerializeField]
        private int cursor = 0;
		private float origin = -2f;
		private float timethreshold = 0.5f;
		private float timer = 0;
		private static Vector3 _Reset = new Vector3 (0, 35, 0);
		Holojam.Tools.Phonecontroller phonecontroller;


        // Use this for initialization
        void Start()
        {
			top = 0;
			bottom = 5;
			previous = 0;
			next = 6;
			phonecontroller = GameObject.Find ("Controller").gameObject.GetComponent<Holojam.Tools.Phonecontroller> ();
//            Vcon = GameObject.Find("VRConsole").GetComponent<VRConsole>();
            lines = Textfile.text.Split('\n');
			for (int i = 0; i < 6; i++) {
				Vcon [i].setText (lines [i]);
			}
			SetColor ();
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
				for (int i = 0; i < 6; i++) {
					Vcon [i].gameObject.GetComponent<MeshRenderer> ().enabled = true;
				}
//				timer += Time.deltaTime;
				if (origin == -2f)
					origin = phonecontroller.y;
				dis = phonecontroller.y - origin;
				SetPos ();

			} else {
				origin = -2f;
			}


          }

		void InitText(){
			for (int i = 0; i < 6; i++) {
				Vcon [i].gameObject.GetComponent<MeshRenderer> ().enabled = true;
				Vcon [i].setText (lines [i]);
			}
		}

		void SetPos(){
			for (int i = 0; i < 6; i++) {
				Vcon [i].transform.localPosition += new Vector3 (0, dis * 80, 0);
			}
			origin = phonecontroller.y;
			if (Vcon [top].transform.localPosition.y > 105) {
				Vcon [top].transform.localPosition = Vcon [bottom].transform.localPosition - _Reset;
				SetText (top, next, true);
				bottom = top;
				top = (top + 1) % 6;
			}
			if (Vcon [bottom].transform.localPosition.y < -140) {
				Vcon [bottom].transform.localPosition = Vcon [top].transform.localPosition + _Reset;
				SetText (bottom, previous, false);
				top = bottom;
				bottom = (top + 5) % 6;
			}
			SetColor ();
				
		}

		void SetText(int index, int cursor, bool forword){
			if (forword) {
				if (next + 1 < lines.Length) {
					Vcon [index].setText (lines [++next]);
					if (next >6)
					previous++;
				} else {
					Vcon [index].setText ("");
					if (previous + 1 <= next)
						previous++;
				}
			}else{
				if (previous - 1 >= 0) {
					Vcon [index].setText (lines [--previous]);
					if (previous<lines.Length - 6)
					next--;
				} else {
					Vcon [index].setText ("");
					if (next - 1 >= previous)
						next--;
				}
		}
		}

		void SetColor(){
			for (int i = 0; i < 6; i++) {
				if (Vcon [i].transform.localPosition.y < -20) {
					t = (Vcon [i].transform.localPosition.y + 140f) / 120f;
				}
				if (Vcon [i].transform.localPosition.y < -105)
					t = 0;
				if (Vcon [i].transform.localPosition.y >= -20 && Vcon [i].transform.localPosition.y<=0) {
					t=1;
				}
				if (Vcon [i].transform.localPosition.y >0) {
					t=1 - (Vcon [i].transform.localPosition.y - 10f) / 95f;
				}
				if (Vcon [i].transform.localPosition.y >85)
					t = 0;
				t = 3 * t * t - 2 * t * t * t;
				Vcon [i].setColor(new Vector4 (1, 1, 1, t));
			}
		}
		void SetText(int dir){
//			GameObject.Find("VRConsole").GetComponent<MeshRenderer>().enabled = true;
//
//			if (dir >= 0) {
//				if (cursor + 2 < lines.Length) {
//					Vcon.setText (String.Concat (lines [cursor], '\n', lines [++cursor] , '\n', lines [cursor+1]));
//				} else
//					Vcon.setText (String.Concat (lines [cursor], '\n', lines [cursor + 1]));
//			} else {
//				if (cursor > 1)
//					Vcon.setText (String.Concat (lines [--cursor - 1],'\n',lines [cursor], '\n', lines [cursor+1]));
//				else
//					Vcon.setText (String.Concat (lines [cursor - 1], '\n',lines [cursor]));
//			}
		}
        }
}
