using UnityEngine;
using System.Collections;

public class ChosenHeadsetAssign : MonoBehaviour
{

    Holojam.Tools.ActorManager manager;
    ChosenHeadset headset;
	Holojam.Tools.Phonecontroller phonecontroller;
	string[] controllertags = { "controller0",
								"controller1",
								"controller2",
								"controller3"};
    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("ActorManager").gameObject.GetComponent<Holojam.Tools.ActorManager>();
		phonecontroller = GameObject.Find ("Controller").gameObject.GetComponent<Holojam.Tools.Phonecontroller> ();
        if (GameObject.Find("ChosenHeadset"))
        {
            headset = GameObject.Find("ChosenHeadset").gameObject.GetComponent<ChosenHeadset>();
			phonecontroller.label = controllertags [headset.headsetInt - 1];
            manager.buildTag = headset.whichHeadset;
            manager.reindex = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
