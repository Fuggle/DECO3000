using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public float spinSpeed = 5;
	bool isScalingUp = true;
	int scaleBuffer = 0;
	int limit = 100;
	float scaleAmount = 0.002f;

	float curretnLerpTime;
	float lerpTime;


	// Update is called once per frame
	void Start () {
		float random = Random.value * 360;
		transform.Rotate (random, random, random);
	//	transform.rotation = new Vector3 (transform.rotation.x * random, transform.rotation.y * random, transform.rotation.z * random);
	
	}
}
