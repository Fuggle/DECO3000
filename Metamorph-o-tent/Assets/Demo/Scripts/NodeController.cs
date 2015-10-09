using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeController : MonoBehaviour {

	public GameObject node;
	public GameObject centralNode;
	public GameObject nodeConnector;
	public List<GameObject> nodeList;
	List<GameObject> nodeHitList;

	public float checkRadius = 1f;
	bool moveNode;

	void Start () 
	{
		nodeList = new List<GameObject>();
		nodeHitList = new List<GameObject>();
	}

	void Update(){
		if (moveNode) {


		}
	}

	public void createNode(Vector3 touchLocation)
	{

		Collider[] nodeHitList = Physics.OverlapSphere (touchLocation, checkRadius);
		//print ("NODEHIT list count: " + nodeHitList.Length);
		//print (nodeHitList [0]);
		if (nodeHitList.Length == 1 ) {
			//creates node and adds to 'nodeList'
			nodeList.Add((GameObject)Instantiate (node, touchLocation , transform.rotation));
			return;
		}

		foreach (Collider nodes in nodeHitList) {
			if(nodes.tag == "Node"){
				Node nodeScript = nodes.gameObject.GetComponent<Node>();
				nodeScript.nodeHover();
			}
		}

	}

	public void TriggerEvent(Transform triggeredNode)
	{
		print ("TriggerEvent!!");
		moveNode = true;
		/*
		foreach (Transform node in nodeList) {
			Transform previousNode;
			previousNode = triggeredNode;
			float distance = Vector3.Distance(previousNode, node.position);


			List<Transform> nodePath = new List<Transform>();
			if(node != previousNode){
				nodePath.Add(node);
				previou
			}
		}*/
	}
	
}
