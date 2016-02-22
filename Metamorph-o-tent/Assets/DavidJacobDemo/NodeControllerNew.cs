using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class NodeControllerNew : MonoBehaviour {

	public GameObject node;
	public GameObject linePrefab;
	
	public List<GameObject> nodeList;
	public List<Connection> connections;
	public List<GameObject> lines;

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
		connections = new List<Connection> ();
		lines = new List<GameObject> ();
	
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
							drawline(conn);
						}
						
					}
				}
			}
		}
		//print ("number of elements in connections" + connections.Count);
	}

	/// <summary>
	/// Checks the given list of connections for duplicates of the attempted connection
	/// </summary>
	/// <returns><c>true</c>, if connections for duplicates was searched, <c>false</c> otherwise.</returns>
	/// <param name="attemptedConnection">Attempted connection.</param>
	/// <param name="connectionList">Connection list.</param>
	bool searchConnectionsForDuplicates(Connection attemptedConnection, List<Connection> connectionList) {
		
		// check to see if this connection has already been formed
		for( int n = 0; n < connectionList.Count; n++) {
			
			if (checkConnectionsTheSame(attemptedConnection, connectionList[n])) {
				return true;
			}
		}
		
		return false;
	}

	/// <summary>
	/// Checks if two connections are the same
	/// </summary>
	/// <returns><c>true</c>, if connections the same was checked, <c>false</c> otherwise.</returns>
	/// <param name="conn1">Conn1.</param>
	/// <param name="conn2">Conn2.</param>
	bool checkConnectionsTheSame(Connection conn1, Connection conn2)
	{
		return conn1.containsNode(conn2.getFirstNode()) && conn1.containsNode(conn2.getSecondNode());
	}


	/// <summary>
	/// draws a line between the two nodes in a connection
	/// </summary>
	/// <param name="conn">Conn.</param>
	void drawline(Connection conn) {
		
		Vector3[] positions = new Vector3 [2];
		
		GameObject line = Instantiate(linePrefab, new Vector2(0,0), Quaternion.identity) as GameObject;

		Line thisLine = line.GetComponent<Line> ();
		thisLine.setConnection (conn);

		lines.Add (line);

		LineRenderer lineRenderer = line.GetComponent<LineRenderer> ();
		Vector3 pos1 = conn.getFirstNode ().gameObject.transform.position;
		Vector3 pos2 = conn.getSecondNode ().gameObject.transform.position;
		lineRenderer.SetPosition(0, pos1);
		lineRenderer.SetPosition(1, pos2);
	}

	/// <summary>
	/// Removes any connections that contain the given node.
	/// </summary>
	/// <param name="node">Node.</param>
	public void removeConnections(NodeNew node) {

		//for removing connections
		List<int> indexes = new List<int>();

		for (int i = 0; i < connections.Count; i++) {
			if (connections[i].containsNode(node)) {
				indexes.Add(i);
			}
		} 

		for (int i = 0; i < indexes.Count; i++) {
			connections.RemoveAt(indexes[i]);
		}

		//for removing connection lines
		List<int> indexes2 = new List<int> ();

		for (int i = 0; i < lines.Count; i++) {
			if (lines [i].GetComponent<Line> ().getConnection().containsNode (node)) {
				indexes2.Add (i);
			}
		}

		for (int i = 0; i < indexes2.Count; i++) {
			Destroy (lines [indexes2 [i]].gameObject);
			lines.RemoveAt (indexes2 [i]);
		}
	}

	/// <summary>
	/// Removes the node from the node list.
	/// </summary>
	/// <param name="node">Node.</param>
	public void removeNode(NodeNew node) {
		nodeList.Remove (node.gameObject);
	}

	/// <summary>
	/// Makes all nodes that are connected to the triggering node trigger something
	/// </summary>
	/// <param name="node">Node.</param>
	public void makeConnectionsVisible(NodeNew node) {
		List<NodeNew> nodesToGlow = new List<NodeNew> ();
		nodesToGlow.Add (node);

		for (int i = 0; i < connections.Count; i++) {
			if (connections [i].containsNode (node)) {
				if (connections [i].getFirstNode () == node) {
					nodesToGlow.Add (connections [i].getSecondNode ());
				} else {
					nodesToGlow.Add (connections [i].getFirstNode ());
				}
			}
		} 

		for (int i = 0; i < nodeList.Count; i++) {
			for (int n = 0; n < nodesToGlow.Count; n++) {
				if (nodeList [i].GetComponent<NodeNew> () == nodesToGlow [n]) {
					print ("hello");
					nodesToGlow [n].GetComponent<FadeInOut>().visibleOn();
				}
			}
		}
	}
}
