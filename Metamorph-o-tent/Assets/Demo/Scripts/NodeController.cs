using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeController : MonoBehaviour {

	public GameObject node;

	public List<GameObject> nodeList;



	void Start () 
	{
		nodeList = new List<GameObject>();
	}
	
	public void createNode(Vector3 touchLocation)
	{
		//creates node and adds to 'nodeList'
		nodeList.Add((GameObject)Instantiate (node, touchLocation , transform.rotation));
	}
	
}
