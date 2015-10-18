using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {

	LineRenderer lineRenderer;
	NodeController nodeController;
	List<GameObject> nodeList;
	float scaleAmount = 1.01f;
	float decayAmount = 0.9990f;
	public float maxScale = 12;
	float minScale = 1f;
	int passNum = 0;

	void Start () 
	{
		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.enabled = false;
		nodeController = Camera.main.GetComponent<NodeController> ();
		nodeList = nodeController.nodeList;
		print (transform.localScale.magnitude);
	}
	

	void Update () 
	{
		slowDecay ();
	}

	/*
	public void lineConnection()
	{
		print ("NodeCount: " + nodeList.Count);
		if (nodeList.Count > 1 ) {
			for (int i = 0; i<nodeList.Count-1; i++) {
				lineRenderer.enabled = true;
				lineRenderer.SetPosition (0, nodeList [i].transform.position);
				lineRenderer.SetPosition (1, nodeList [i + 1].transform.position);
			}
		}
	}*/

	/// <summary>
	/// When the node is touched change color.
	/// </summary>
	public void nodeTouched()
	{
		Renderer nodeRenderer = GetComponent<Renderer>();
		nodeRenderer.material.color = Color.blue;
	}

	/// <summary>
	/// On hover of the node increase scale.
	/// </summary>
	public void nodeHover()
	{
		if (transform.localScale.magnitude > maxScale) {
			nodeController.TriggerEvent (this.gameObject.transform);
		} else {
			transform.localScale = new Vector3 
				(transform.localScale.x * scaleAmount, transform.localScale.y * scaleAmount, transform.localScale.z * scaleAmount);
		}
	}

	public void nodeHover(float scaleAmount)
	{
		if (transform.localScale.magnitude > maxScale) {
			nodeController.TriggerEvent (this.gameObject.transform);
		} else {
			transform.localScale = new Vector3 
				(transform.localScale.x * scaleAmount, transform.localScale.y * scaleAmount, transform.localScale.z * scaleAmount);
		}
	}

	/// <summary>
	/// Slowly decays the node.
	/// </summary>
	void slowDecay()
	{
		transform.localScale = new Vector3 
			(transform.localScale.x * decayAmount, transform.localScale.y * decayAmount, transform.localScale.z * decayAmount);

		if (transform.localScale.magnitude < minScale) {
			destroyNode();
		}
	}

	/// <summary>
	/// Destroys the node.
	/// </summary>
	void destroyNode()
	{
		Destroy (this.gameObject);
	}

	public void reinforcePath(){
		passNum ++;
		decayAmount = decayAmount + ((1-decayAmount)/1.25f); 

	}
}
