using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeController : MonoBehaviour {

	public GameObject node;

	public List<GameObject> nodeList;
	public List<GameObject> nodeHitList;

	public float checkRadius = 10;

	void Start () 
	{
		nodeList = new List<GameObject>();
		nodeHitList = new List<GameObject>();
	}
	
	public void createNode(Vector3 touchLocation)
	{

		Collider[] nodeHitList = Physics.OverlapSphere (touchLocation, checkRadius);
		print ("NODEHIT list count: " + nodeHitList.Length);
		print (nodeHitList [0]);
		if (nodeHitList.Length == 1 ) {
			//creates node and adds to 'nodeList'
			nodeList.Add((GameObject)Instantiate (node, touchLocation , transform.rotation));
			return;
		}

		foreach (Collider nodes in nodeHitList) {
			if(nodes.tag == "Node"){
				print ("make node hover");
				Node nodeScript = nodes.gameObject.GetComponent<Node>();
				nodeScript.nodeHover();
			}
		}




	}
	
}
