using UnityEngine;
using System.Collections;

public class Root : MonoBehaviour {

	public GameObject root;
	bool canSpawn = true;

	public int numberOfSpawns;
	int rootLength = 30;

	// Update is called once per frame
	void Update () {
		createRoots ( this.transform.position);
	}

	public void createRoots(Vector3 location)
	{
		GameObject childRoot =  Instantiate (root, location, Quaternion.identity) as GameObject;

		childRoot.transform.localScale = new Vector3(this.transform.localScale.x - 0.1f, this.transform.localScale.y - 0.1f);
		childRoot.GetComponent<Root>().createRoots (new Vector3 (childRoot.transform.position.x, childRoot.transform.position.y + 0.1f));
	}
}
