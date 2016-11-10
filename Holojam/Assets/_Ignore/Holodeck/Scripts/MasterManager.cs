//MasterManager.cs
//Created by Wenbo Lan on 09.11.16
using System.Collections;
using UnityEngine;
using Holojam.Network;

namespace Holojam.Tools
{
	public class MasterManager : Synchronizable
	{
		[SerializeField]
		private int clientInt;
		public float masterDis;
		public int masterMode = -1;
		public bool masterControll = false;
		public float masterAlpha = 0;
		private scriptcontroller teleprompter;

		// Use this for initialization
		void Awake ()
		{
			if (!sending) {
				clientInt = GameObject.Find ("Controller").GetComponent<Phonecontroller> ().index;
			}
			teleprompter = GameObject.Find ("Teleprompter").GetComponent<scriptcontroller> ();
		}

		void Client ()
		{
			//transform.position = synchronizedVector3;
			teleprompter.mode = synchronizedInt;
			masterDis = transform.position.y;
		}
	
		// Update is called once per frame
		void Server ()
		{
			synchronizedVector3=transform.position;
			if (masterControll) {
				synchronizedInt = masterMode;
				masterControll = false;
			} else
				synchronizedInt = -1;
		}

		//For Debug and Dev
		protected override void Sync(){

			if (sending) {
				Server ();
				//Client ();
			}
			else 
			{	
				clientInt = GameObject.Find ("Controller").GetComponent<Phonecontroller> ().index;
				transform.position = synchronizedVector3;
				if (clientInt == (int)transform.position.x) 
				Client ();
			}
		}
	}
}