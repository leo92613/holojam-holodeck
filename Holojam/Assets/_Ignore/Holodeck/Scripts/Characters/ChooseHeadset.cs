using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEngine.EventSystems;

public class ChooseHeadset : MonoBehaviour {

	public GameObject textBox;
	public GameObject reticleText;
	public int rows;
	public int amount;
	public float width,height;
	int chosen;

	float tapCounter = .5f;
	int taps;

	void Start(){

		float offset = ((width*.5f)*rows)+width;

		int j = 0;
		int k = 0;
		for (int i = 0; i < amount; i++) {
			if (j > rows) {
				j = 0;
				k++;
			}
			j++;
			GameObject go = Instantiate (textBox, new Vector3 ((width * j)-offset, (height * -k)+(offset*.5f), textBox.transform.position.z), Quaternion.identity) as GameObject;
			go.transform.SetParent (textBox.transform.parent);
			go.GetComponent<Text> ().text = (i+1).ToString();

		}
	}

	void Update(){
		
//		Vector3 fwd = transform.TransformDirection (Vector3.forward);
		Vector3 fwd = transform.forward;
//		Debug.Log (fw	d);
		Debug.DrawRay(transform.position, fwd*10000, Color.green);
		RaycastHit hit;
		if (Physics.Raycast (transform.position, fwd*10000, out hit, 1000)) {
			reticleText.GetComponent<Text> ().text = hit.transform.gameObject.GetComponent<Text> ().text;
			chosen = int.Parse( hit.transform.gameObject.GetComponent<Text> ().text);
			cachedDebugMessage (chosen.ToString());
		}
		tapCounter -= Time.deltaTime;
		if (tapCounter <= 0) {
			tapCounter = .5f;
			if (taps > 0)
				taps--;
		}
        if (Input.GetMouseButtonDown (0) && tapCounter>0) {
       // if (Input.GetKeyDown("space")) { 
			taps++;
			tapCounter = .5f;
			if (taps == 2) {
				GameObject.Find ("ChosenHeadset").GetComponent<ChosenHeadset> ().setHeadset (chosen);
				SceneManager.LoadScene (1);
			}
		}
	}
	private string message="";
	void cachedDebugMessage(string _message) {
		if (_message!=message) {
			Debug.Log("headset chosen "  + _message);
			message = _message;
		}
	}
}
//
//	public GraphicRaycaster raycaster; 
//	// Use this for initialization
//	void Start () {
//		
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		//Code to be place in a MonoBehaviour with a GraphicRaycaster component
//		GraphicRaycaster gr = this.GetComponent<GraphicRaycaster>();
//		//Create the PointerEventData with null for the EventSystem
//		PointerEventData ped = new PointerEventData(null);
//		//Set required parameters, in this case, mouse position
//		ped.position = Input.mousePosition;
//		//Create list to receive all results
//		List<RaycastResult> results = new List<RaycastResult>();
//		//Raycast it
//		raycaster.Raycast (ped, results);
//		Debug.Log (results.Count);
//		for (int i = 0; i < results.Count; i++) {
//			
//			Debug.Log (results [i].gameObject.GetComponent<Text> ().text);
//		}
//	}
//}
