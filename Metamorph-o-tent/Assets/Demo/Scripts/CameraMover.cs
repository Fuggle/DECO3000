using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {

	public float speed = 5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Y)) {
			float tZ = Time.deltaTime * speed;
			transform.Translate(0,0,tZ);
		}
		if (Input.GetKeyDown(KeyCode.G)) {
			float tZ = Time.deltaTime * speed;
			transform.Translate(-tZ,0,0);
		}
		if (Input.GetKeyDown(KeyCode.J)) {
			float tZ = Time.deltaTime * speed;
			transform.Translate(tZ,0,0);
		}
		if (Input.GetKeyDown(KeyCode.H)) {
			float tZ = Time.deltaTime * speed;
			transform.Translate(0,0,-tZ);
		}

		if (Input.GetKeyDown(KeyCode.T)) {
			float tZ = Time.deltaTime * speed;
			transform.Translate(0,tZ,0);
		}
		if (Input.GetKeyDown(KeyCode.U)) {
			float tZ = Time.deltaTime * speed;
			transform.Translate(0,-tZ,0);
		}
		

	}
}
