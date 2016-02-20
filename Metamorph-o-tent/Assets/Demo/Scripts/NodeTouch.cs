using UnityEngine;
using System.Collections;

public class NodeTouch : MonoBehaviour {

	NodeControllerNew nodeController;

	void Start () {
		nodeController = GetComponent<NodeControllerNew> ();
	}
	
	//test for placing nodes (will be replaced by En's kinect touch code)
	void Update () {

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			//checks to see if ray is hitting existing node
			if (Input.GetMouseButtonDown (0)) {
			
				if(hit.collider.gameObject.tag == "Node"){
					GameObject node = hit.collider.gameObject;
					NodeNew nodeScript = node.GetComponent<NodeNew>();
					nodeScript.nodeTouch();
				} else{
					nodeController.createNode(hit.point);
				}
				
			} 
		}
	}
}
