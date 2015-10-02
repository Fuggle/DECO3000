using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {

	LineRenderer lineRenderer;
	NodeController nodeController;
	List<GameObject> nodeList;
	float scaleAmount = 1.01f;
	float maxScale = 5;
	// Use this for initialization
	void Start () {
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.enabled = false;
		nodeController = Camera.main.GetComponent<NodeController> ();
		nodeList = nodeController.nodeList;
		print (transform.localScale.magnitude);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void lineConnection(){
		print ("NodeCount: " + nodeList.Count);
		if (nodeList.Count > 1 ) {
			for (int i = 0; i<nodeList.Count-1; i++) {
				lineRenderer.enabled = true;
				lineRenderer.SetPosition (0, nodeList [i].transform.position);
				lineRenderer.SetPosition (1, nodeList [i + 1].transform.position);
			}
		}
	}

	public void nodeTouched(){
		Renderer nodeRenderer = GetComponent<Renderer>();
		nodeRenderer.material.color = Color.blue;
		lineConnection ();
	}

	public void nodeHover(){
		print ("Node touched");
		transform.localScale = new Vector3 
			(transform.localScale.x * scaleAmount, transform.localScale.y * scaleAmount, transform.localScale.z * scaleAmount);

		if (transform.localScale.magnitude > maxScale) {
			lineConnection();
		}
	}
}
