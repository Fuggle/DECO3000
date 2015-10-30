using UnityEngine;
using System.Collections;

public class TravellerController : MonoBehaviour {

	public float giveScaleFactor = 1.005f;


	void OnTriggerStay(Collider col){
		if (col.tag == "Node") {
			print ("passing through node");
			col.gameObject.GetComponent<Node> ().nodeHover(giveScaleFactor);
		}
	}

	void OnTriggerExit(Collider col){
		if (col.tag == "Node") {
			col.gameObject.GetComponent<Node>().reinforcePath();
			//Camera.main.GetComponent<SoundManager>().playLayer(4);
		}
	}
}
