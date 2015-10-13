using UnityEngine;
using System.Collections;

public class NodeTouch : MonoBehaviour {

	NodeController nodeController;

	void Start () {
		nodeController = GetComponent<NodeController> ();
	}
	
	//test for placing nodes (will be replaced by En's kinect touch code)
	void Update () {

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			//checks to see if ray is hitting existing node
			if (Input.GetMouseButton (0)) {
			
				if(hit.collider.gameObject.tag == "Node"){
					print ("hit node directly");
					GameObject node = hit.collider.gameObject;
					Node nodeScript = node.GetComponent<Node>();
					nodeScript.nodeTouched();
					nodeScript.nodeHover();
				} else{
					nodeController.createNode(hit.point);
				}
				
			} 
			/*
			else{


				if(hit.collider.gameObject.tag == "Node"){
					GameObject node = hit.collider.gameObject;
					Node nodeScript = node.GetComponent<Node>();
					nodeScript.nodeHover();
				}
			}*/
		}
	}
}
