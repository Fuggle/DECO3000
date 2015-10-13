using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeController : MonoBehaviour {
	public GameObject node;
	public GameObject centralNode;
	public GameObject nodeConnector;
	public List<GameObject> nodeList;

	List<Transform> nodePathList;
	List<GameObject> nodeHitList;

	public float checkRadius = 3f;
	bool moveNode;

	void Start () 
	{
		nodeList = new List<GameObject>();
		nodeHitList = new List<GameObject>();
		nodePathList = new List<Transform>();
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
		float triggeredNodeDistance = Vector3.Distance(centralNode.transform.position, triggeredNode.position);
		foreach (GameObject node in nodeList) {
		
			float distance = Vector3.Distance(node.transform.position, triggeredNode.position);

			if(distance < triggeredNodeDistance){
				if(nodePathList.Count < 1){
					nodePathList.Add(node.transform);
					return;
				} 
					for(int i = 0; i< nodePathList.Count; i++){
						if(distance < Vector3.Distance(nodePathList[i].position, triggeredNode.position)){
							//add to pathList
							nodePathList.Add(node.transform);
						}
					}

			}

		}
		print (nodePathList);

	}


	
}
