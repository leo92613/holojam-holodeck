using UnityEngine;
using System.Collections;

public class ChosenHeadsetAssign : MonoBehaviour
{

    Holojam.Tools.ActorManager manager;
    ChosenHeadset headset;
    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find("ActorManager").gameObject.GetComponent<Holojam.Tools.ActorManager>();
        if (GameObject.Find("ChosenHeadset"))
        {
            headset = GameObject.Find("ChosenHeadset").gameObject.GetComponent<ChosenHeadset>();
            manager.buildTag = headset.whichHeadset;
            manager.reindex = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
