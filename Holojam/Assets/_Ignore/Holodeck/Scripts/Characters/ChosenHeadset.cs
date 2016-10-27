using UnityEngine;
using System.Collections;


public class ChosenHeadset : MonoBehaviour {

	public Holojam.Network.Motive.Tag whichHeadset;
	public int headsetInt;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	public void setHeadset(int which){
		headsetInt = which;
		if (which == 1)
			whichHeadset = Holojam.Network.Motive.Tag.HEADSET1;
		else if (which == 2)
			whichHeadset = Holojam.Network.Motive.Tag.HEADSET2;
		else if (which == 3)
			whichHeadset = Holojam.Network.Motive.Tag.HEADSET3;
		else if (which == 4)
			whichHeadset = Holojam.Network.Motive.Tag.HEADSET4;
		else
			Debug.Log ("no headset");
	}

}
