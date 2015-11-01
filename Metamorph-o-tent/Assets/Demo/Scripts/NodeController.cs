using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class NodeController : MonoBehaviour {
	public GameObject node;
	public GameObject centralNode;

	public List<GameObject> nodeList;

	public List<Transform> nodePathList;
	List<GameObject> nodeHitList;
	public Transform travelNode;
	int pathCounter = 0;

	public float speed = 5;
	public float checkRadius = 3f;
	public bool movingNode;
	bool eventTriggered;
	float currentLerpTime = 0;
	void Start () 
	{
		nodeList = new List<GameObject>();
		nodeHitList = new List<GameObject>();
		nodePathList = new List<Transform>();
		pathCounter = 0;
	}

	void Update(){

		if (movingNode && nodePathList.Count > 0) {	

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

		Collider[] nodeHitList = Physics.OverlapSphere (touchLocation, checkRadius);
	
		if (nodeHitList.Length == 1 ) {
			//creates node and adds to 'nodeList'
			GameObject currentNode = (GameObject)Instantiate (node, touchLocation , transform.rotation);
			currentNode.transform.LookAt(Camera.main.transform.position);
			nodeList.Add(currentNode);
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
		if (!eventTriggered) {
			eventTriggered = true;
			print ("TriggerEvent!!");

			// sond that gets played when the node event is triggered
			Camera.main.GetComponent<SoundManager>().playLayer("Bell1", 0.7f, 1);

			float triggeredNodeDistance = Vector3.Distance (centralNode.transform.position, triggeredNode.position);
			List<Transform> temporaryNodelist = new List<Transform> ();

			List<float> temporaryDistanceList = new List<float> ();
			List<Transform> actualList = new List<Transform> ();
			
			foreach (GameObject aNode in nodeList) {
				float distance = Vector3.Distance (aNode.transform.position, centralNode.transform.position);
				float nodeDistance = Vector3.Distance(aNode.transform.position, triggeredNode.position);
				if (distance < triggeredNodeDistance && nodeDistance < triggeredNodeDistance) {
					temporaryNodelist.Add (aNode.transform);
					temporaryDistanceList.Add (Vector3.Distance (aNode.transform.position, centralNode.transform.position));
				}
			}
			
			Transform[] transArray = temporaryNodelist.ToArray ();
			float[] distArray = temporaryDistanceList.ToArray ();
			
			Array.Sort (distArray, transArray);
		

			for(int i = transArray.Length - 1; i > -1; i--) {
				nodePathList.Add(transArray[i]);
			}
			nodePathList.Insert(0,triggeredNode);
			nodePathList.Add (centralNode.transform);
			print(nodePathList.Count);
			movingNode = true;
			float currentLerpTime = 0f;
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

	
}
