using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public float spinSpeed = 5;
	bool isScalingUp = true;
	int scaleBuffer = 0;
	int limit = 100;
	float scaleAmount = 0.002f;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.right * spinSpeed);

		if (isScalingUp && scaleBuffer < limit) {
			this.transform.localScale = new Vector3(transform.localScale.x + scaleAmount, transform.localScale.y + scaleAmount, transform.localScale.z + scaleAmount);
			scaleBuffer++;
			if (scaleBuffer == limit) {
				isScalingUp = false;
			}
		}

		if (!isScalingUp && scaleBuffer > -limit) {
			this.transform.localScale = new Vector3(transform.localScale.x - scaleAmount, transform.localScale.y - scaleAmount, transform.localScale.z - scaleAmount);
			scaleBuffer--;
			if (scaleBuffer == -limit) {
				isScalingUp = true;
			}
		}
	}
}
