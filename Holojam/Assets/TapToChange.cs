using UnityEngine;
using System.Collections;

namespace Holojam.Tools
{
	public class TapToChange : MonoBehaviour
	{
		public Transform originpos;
		public Transform VRConsole;
		public Transform cameratransform;
		public Vector3 hitpos;
		public bool activated;
		public Viewer viewer;
		// Use this for initialization
		void Start ()
		{
			activated = false;
			viewer = this.GetComponent<Viewer> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (Input.GetMouseButtonDown (0)) {
				if (activated) {
					activated = false;
					VRConsole.parent = originpos.parent;
					VRConsole.localPosition = originpos.localPosition;
					VRConsole.localRotation = originpos.localRotation;
					VRConsole.localScale = originpos.localScale;
				} else {
					activated = true;
				}
			}
			if (activated) {
				Vector3 fwd = viewer.actor.look;
				//		Debug.Log (fw	d);
				//Debug.DrawRay (cameratransform.position, fwd * 10000, Color.green);
				RaycastHit hit;
				if (Physics.Raycast (transform.position, fwd, out hit, 100000)) {
					//Debug.Log (hit.transform.gameObject.name);
					if (hit.transform.gameObject.tag == "Player") {
					
						VRConsole.parent = hit.transform;
						VRConsole.localPosition = new Vector3 (-0.5f, 0f, 0f);
						VRConsole.localRotation = Quaternion.identity;
						VRConsole.localScale = new Vector3 (0.001765348f, 0.002229395f, 0.002229395f) * 2f;
					}
				}
			}

		}
	}
}
