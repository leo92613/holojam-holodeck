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
		public int top, bottom, previous, next;
		public float t;
		public int mode = 0;
		public float masterDis;
		public Transform telePos;
		public float masterAlpha;
		public GameObject targetPos;
		[SerializeField]
		private int curmode = 0;
		private float origin = -2f;
		private float timethreshold = 0.5f;
		private float timer = 0;
		private static Vector3 _Reset = new Vector3 (0, 35, 0);
		Holojam.Tools.Phonecontroller phonecontroller;


		// Use this for initialization
		void Start ()
		{
			top = 0;
			bottom = 5;
			previous = 0;
			next = 5;
			phonecontroller = GameObject.Find ("Controller").gameObject.GetComponent<Holojam.Tools.Phonecontroller> ();
			lines = Textfile.text.Split ('\n');
			for (int i = 0; i < 6; i++) {
				Vcon [i].gameObject.GetComponent<MeshRenderer> ().enabled = true;
				Vcon [i].setText (lines [i]);
				SetColor (i);
			}
			SetAlpha (0);
		}

		void Update ()
		{
			//telePos.rotation = GameObject.Find ("OriginTele").transform.rotation;
		}
		// Update is called once per frame
		void LateUpdate ()
		{
			masterAlpha = GameObject.Find ("MasterManager").transform.position.z;
			if (mode == 3) {
				for (int i = 0; i < 6; i++) {
					Vcon [i].gameObject.GetComponent<MeshRenderer> ().enabled = true;
				}
				masterDis = GameObject.Find ("MasterManager").GetComponent<MasterManager> ().masterDis;
				SetPos (masterDis);
			}
			if (mode == 5) {
				telePos.position = GameObject.Find ("OriginTele").transform.position;
				telePos.parent = GameObject.Find ("VRCamera").transform;
			}
			if (mode == 4) {
				if (telePos.parent.gameObject == targetPos)
					telePos.parent = null;
				else
					telePos.parent = targetPos.transform;
			}

			if (mode < 3 && mode > -1)
				curmode = mode;
			if (phonecontroller.z > 0.1) {
				for (int i = 0; i < 6; i++) {
					Vcon [i].gameObject.GetComponent<MeshRenderer> ().enabled = true;
				}
				if (origin == -2f)
					origin = phonecontroller.y;
				dis = phonecontroller.y - origin;
				if (!((previous == 0 && dis < 0) || (next == lines.Length - 1 && dis >= 0))) {
					if (curmode == 1) {
						float _dis;
						if (phonecontroller.y == 0)
							_dis = 0;
						else
							_dis = -phonecontroller.y / Mathf.Abs (phonecontroller.y) * 0.05f;
						SetPos (_dis);
					}
					if (curmode == 0)
						SetPos ();	
				}
				if (curmode == 2)
					SetPrompterPos (dis);
			} else {
				origin = -2f;
			}
		}

		void InitText ()
		{
			for (int i = 0; i < 6; i++) {
				Vcon [i].gameObject.GetComponent<MeshRenderer> ().enabled = true;
				Vcon [i].setText (lines [i]);
			}
		}

		void SetPrompterPos (float _dis)
		{
			telePos.localPosition += new Vector3 (0, _dis * 0.1f, 0);
		}

		void SetPos ()
		{
			SetPos (dis);
		}

		void SetPos (float _dis)
		{

			for (int i = 0; i < 6; i++) {
				Vcon [i].transform.localPosition += new Vector3 (0, _dis * 80, 0);
			}				
			origin = phonecontroller.y;
			if (Vcon [top].transform.localPosition.y > 105) {
				Vcon [top].transform.localPosition = Vcon [bottom].transform.localPosition - _Reset;
				SetText (top, true);
				bottom = top;
				top = (top + 1) % 6;
			}
			if (Vcon [bottom].transform.localPosition.y < -140) {
				Vcon [bottom].transform.localPosition = Vcon [top].transform.localPosition + _Reset;
				SetText (bottom, false);
				top = bottom;
				bottom = (top + 5) % 6;
			}
			SetAlpha (masterAlpha);
				
		}

		void SetText (int index, bool forward)
		{
			if (forward) {
				if (next + 1 < lines.Length) {
					Vcon [index].setText (lines [++next]);
					SetColor (index);
					if (next > 6)
						previous++;
				} else {
					//if (!(previous == 0||next == lines.Length - 1))
					Vcon [index].setText ("");
					if (previous + 1 <= next)
						previous++;
				}
			} else {
				if (previous - 1 >= 0) {
					Vcon [index].setText (lines [previous--]);
					SetColor (index);
					if (previous < lines.Length - 6)
						next--;
				} else {
					Vcon [index].setText ("");
					if (next - 1 >= previous)
						next--;
				}
			}

		}

		void InitColor ()
		{
		}

		void SetAlpha ()
		{
			SetAlpha (0);
		}

		void SetAlpha (float alpha)
		{
			for (int i = 0; i < 6; i++) {
				if (Vcon [i].transform.localPosition.y < -20) {
					t = (Vcon [i].transform.localPosition.y + 140f) / 120f;
				}
				if (Vcon [i].transform.localPosition.y < -105)
					t = 0;
				if (Vcon [i].transform.localPosition.y >= -20 && Vcon [i].transform.localPosition.y <= 0) {
					t = 1;
				}
				if (Vcon [i].transform.localPosition.y > 0) {
					t = 1 - (Vcon [i].transform.localPosition.y - 10f) / 95f;
				}
				if (Vcon [i].transform.localPosition.y > 85)
					t = 0;
				t = 3 * t * t - 2 * t * t * t + alpha;
				Vcon [i].setAlpha (t);
			}
		}

		void SetColor (int ind)
		{
			string tmp = Vcon [ind].getText ();
			int i = (int)Char.GetNumericValue (tmp [0]);
			tmp = tmp.Remove (0, 1);
			Vcon [ind].setText (tmp);
			switch (i) {
			case 1:
				Vcon [ind].setColor (new Vector4 (0.5f, 0.7f, 1f, 1f));
				break;
			case 2:
				Vcon [ind].setColor (new Vector4 (1f, 0.6f, 0.6f, 1f));
				break;
			case 3:
				Vcon [ind].setColor (Color.red);
				break;
			default:
				Vcon [ind].setColor (Color.white);
				break;	
			}
		}
	}
}

