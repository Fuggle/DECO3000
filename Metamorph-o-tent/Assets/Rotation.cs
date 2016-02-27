using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {

	public float speed = 30;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up * (Time.deltaTime*speed/2));
		transform.Rotate(Vector3.forward * (Time.deltaTime*speed));
	}
}
