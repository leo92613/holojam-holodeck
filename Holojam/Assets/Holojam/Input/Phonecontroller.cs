//Synchronizable.cs
//Created by Aaron C Gaudette on 11.07.16

using UnityEngine;
using Holojam.Network;

namespace Holojam.Tools
{
	public class Phonecontroller : Synchronizable
    {
        public int index;
		public Vector3 lastpos;
		public float x,y,z;
		void Start(){
			useMasterPC = false;
			sending = false;
		}
		public float angle2 = 0;
        //Override this in derived classes
		protected override void Sync()
		{
			if (view.IsTracked) {
				transform.position = synchronizedVector3;
				transform.rotation = synchronizedQuaternion;
				lastpos = transform.position;
			} else {
				transform.position = lastpos;
				transform.rotation = Quaternion.identity;
			}
			Quaternion q = transform.rotation;
			float now = Time.time;
			Vector3 myLeft = q * Vector3.up;
			//angle2 = Vector3.Dot (myLeft, Vector3.forward);
			angle2 = Vector3.Dot (myLeft, Vector3.up);
			x = lastpos.x;
			y = lastpos.y;
			z = lastpos.z;
		}

		void LateUpdate () {
			//lastz = transform.position.z;
			//print (lastz);
		}
			


    }
}