using UnityEngine;
using System.Collections;

public class traveller : MonoBehaviour {

	public GameObject node;
	NavMeshAgent navAgent;
	// Use this for initialization
	void Start () {
		navAgent = GetComponent<NavMeshAgent> ();
		navAgent.destination = node.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
