using UnityEngine;
using System.Collections;

public class AddPathfinding : MonoBehaviour {

	public float connectionRadius = 5;
	//OffMeshLink offMeshLink;

	void Start () 
	{
		//offMeshLink = gameObject.AddComponent<OffMeshLink> ();
		AddPaths ();
	}
	

	void AddPaths()
	{
		Collider[] connectionHitList = Physics.OverlapSphere (transform.position, connectionRadius);
		foreach (Collider col in connectionHitList) {
			if(col.tag == "Node" && col.gameObject != this.gameObject){
				print ("connection made!");
				OffMeshLink offMeshLink = gameObject.AddComponent<OffMeshLink> ();
				offMeshLink.startTransform = transform;
				offMeshLink.endTransform = col.transform;
			}

		}
	}
}
