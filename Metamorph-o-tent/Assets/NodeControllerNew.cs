using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class NodeControllerNew : MonoBehaviour {

	public GameObject node;
	
	public List<GameObject> nodeList;

	public List<GameObject> nodeHitList;
	
	public float checkRadius = 2f;
	public bool movingNode;
	public bool eventTriggered;
	float currentLerpTime = 0;
	
	private float timeDelay = 4f;
	
	void Start () 
	{
		nodeList = new List<GameObject>();
		nodeHitList = new List<GameObject>();
	
		//Camera.main.GetComponent<ArduinoCommManager>().TurnOff();
		
	}

	/// <summary>
	/// Creates a node if there is no other nodes within a radius of touchLocation, else increases the scale of nearby nodes.
	/// </summary>
	public void createNode(Vector3 touchLocation)
	{
		Collider[] nodeHitList = Physics.OverlapSphere (touchLocation, checkRadius);

		if (nodeHitList.Length == 1) 
		{

			//creates node and adds to 'nodeList'
			GameObject currentNode = (GameObject)Instantiate (node, touchLocation , transform.rotation);

			ArduinoCommManager commManager = Camera.main.GetComponent<ArduinoCommManager>();
			//commManager.triggerLight(4.5f);
			
			currentNode.transform.LookAt(Camera.main.transform.position);
			nodeList.Add(currentNode);
			return;
		}
		
		foreach (Collider nodes in nodeHitList) 
		{
			if(nodes.tag == "Node")
			{
				NodeNew nodeScript = nodes.gameObject.GetComponent<NodeNew>();
				nodeScript.nodeTouch();
			}
		}
		
	}

	
}
