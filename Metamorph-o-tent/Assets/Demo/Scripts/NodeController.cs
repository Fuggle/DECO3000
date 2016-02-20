using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class NodeController : MonoBehaviour {
	public GameObject node;
	public GameObject centralNode;
	public GameObject cell;

	public List<GameObject> nodeList;

	public List<Transform> nodePathList;
	public List<GameObject> nodeHitList;
	public Transform travelNode;
	int pathCounter = 0;

	public float speed = 5;
	public float checkRadius = 2f;
	public bool movingNode;
	public bool eventTriggered;
	float currentLerpTime = 0;

	private float timeDelay = 4f;

	public List<Connection> connections;

	void Start () 
	{
		nodeList = new List<GameObject>();
		nodeHitList = new List<GameObject>();
		nodePathList = new List<Transform>();
		pathCounter = 0;
		Camera.main.GetComponent<ArduinoCommManager>().TurnOff();
		
	}

	void Update(){
//		if (timeDelay > 0f && timeDelay < 4f) {
//			timeDelay-=Time.deltaTime;
//		} else if (timeDelay <= 0f){
//			ArduinoCommManager commManager = Camera.main.GetComponent<ArduinoCommManager> ();
//			commManager.TurnOff ();
//			timeDelay = 4f;
//		}

		if (movingNode && nodePathList.Count > 1) {	

				//increment timer once per frame
				currentLerpTime += Time.deltaTime;
				if (currentLerpTime > speed) {
					currentLerpTime = speed;
				}

				float perc = currentLerpTime / speed;

				Vector3 destination = nodePathList[pathCounter].position;

				travelNode.position = Vector3.Lerp (travelNode.position, destination, perc);
				print ("moving node");

				if(Vector3.Distance(travelNode.position, destination) < 1){
					if(pathCounter == nodePathList.Count - 1){
						
						// play a sound when you reach the central node
						Camera.main.GetComponent<SoundManager>().playLayer("OneShot-Wire-C3", 0.5f, 1);

						print ("made it to the central node!!!!");
						movingNode = false;
						eventTriggered = false;
						pathCounter = 0;
						nodePathList.Clear();
						return; 
					} else {
						pathCounter ++;
						currentLerpTime = 0;

					}
				}

		} 
	}

	public void createNode(Vector3 touchLocation)
	{
		print ("Touch Location: " + touchLocation);
		Collider[] nodeHitList = Physics.OverlapSphere (touchLocation, checkRadius);

		print ("NodeHitList size: " + nodeHitList.Length);
		if (nodeHitList.Length == 0) {
			print ("Make node");
			//creates node and adds to 'nodeList'
			GameObject currentNode = (GameObject)Instantiate (node, touchLocation , transform.rotation);
			GameObject theCell = (GameObject)Instantiate (cell, touchLocation , Quaternion.identity);
			theCell.transform.parent = currentNode.transform;
			ArduinoCommManager commManager = Camera.main.GetComponent<ArduinoCommManager>();
			commManager.triggerLight(4.5f);

			currentNode.transform.LookAt(Camera.main.transform.position);
			nodeList.Add(currentNode);
			return;
		}

		foreach (Collider nodes in nodeHitList) {
			if(nodes.tag == "Node"){
				print ("Make node bigger");
				Node nodeScript = nodes.gameObject.GetComponent<Node>();
				nodeScript.nodeHover();
			}
		}

	}


	public void TriggerEvent(Transform triggeredNode)
	{
		if (!eventTriggered) {

			if(Camera.main.transform.FindChild("EventBackground")) {
				print("background found");
				Camera.main.transform.FindChild("EventBackground").gameObject.GetComponent<EventBackground>().setTrigger(true);
			}
		
		} 
		
	}
	
	List<Transform> createTransList(List<Transform> transList, List<float> distanceList)
	{
		List<Transform> returnThis = new List<Transform> ();
		float num = 0.0f;
		
		for (int i = 0; i < transList.Count; i++) {
			for (int y = 0; y < distanceList.Count; y++) {
				if(distanceList[y] > num) {
					num = distanceList[y];
				}
			}
			returnThis.Add(transList[distanceList.IndexOf(num)]);
			transList.RemoveAt(distanceList.IndexOf(num));
			distanceList.RemoveAt(distanceList.IndexOf(num));
		}
		
		return returnThis;
		
	}

	//create a connection between two nodes and store in the connections list
	public void createConnection ( NodeNew triggeringNode)
	{
		// when the node gets big enough it triggers the event and is ready to make connections
		
		if (triggeringNode.connectReady ()) {
			
			if (nodeList.Count > 0) {
				
				// attempt to connect to all suitable nodes
				for (int i = 0; i < nodeList.Count; i++) {
					
					if (nodeList [i].GetComponent<NodeNew>().connectReady() && nodeList [i].GetComponent<NodeNew>() != triggeringNode) {
						
						Connection conn = new Connection (triggeringNode, nodeList [i].GetComponent<NodeNew>());
						
						if (!searchConnectionsForDuplicates (conn, connections)) {
							connections.Add(conn);
							
							// draw line from one node to another
							// drawline(conn);
						}
					}
				}
			}
		}
		print ("number of elements in connections" + connections.Count);
	}

	//searches the given list of connections for a duplicate of the attempted connection
	bool searchConnectionsForDuplicates(Connection attemptedConnection, List<Connection> connectionList) {
		
		// check to see if this connection has already been formed
		for( int n = 0; n < connectionList.Count; n++) {
			
			if (checkConnectionsTheSame(attemptedConnection, connectionList[n])) {
				return true;
			}
		}
		
		return false;
	}

	//compares two connections to see if they are the same
	bool checkConnectionsTheSame(Connection conn1, Connection conn2)
	{
		return conn1.containsNode(conn2.getFirstNode()) && conn1.containsNode(conn2.getSecondNode());
	}

	//draws a line between two nodes
	/*
	void drawline(Connection conn) {
		
		Vector3[] positions = new Vector3 [2];
		
		GameObject line = Instantiate(lineConnection, new Vector2(0,0), Quaternion.identity) as GameObject;
		LineRenderer lineRenderer = line.GetComponent<LineRenderer> ();
		positions [0] = conn.getFirstNode ().gameObject.transform.position;
		positions [1] = conn.getSecondNode ().gameObject.transform.position;
		lineRenderer.SetPositions (positions);
	} */

	
}
