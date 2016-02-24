using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	float timeLeft = 10.0f;
	bool becomingInvisible = true;
	float scaleFactor = (1f / 10) / 60;
	float currentTransparency = 1f;

	// Use this for initialization
	void Start () {
		spriteRenderer = this.GetComponent<SpriteRenderer> ();
	}

	void Update()
	{
		if (becomingInvisible) {
			print ("becoming invisible");
			timeLeft -= Time.deltaTime;
			currentTransparency -= scaleFactor;
			spriteRenderer.color = new Color(1f,1f,1f,currentTransparency);
			this.transform.localScale = new Vector2(currentTransparency, currentTransparency);
			if (timeLeft < 0) {
				becomingInvisible = false;
				timeLeft = 10.0f;
			}
		} else {
			print ("becoming visible");
			timeLeft -= Time.deltaTime;
			//less transparency
			currentTransparency += scaleFactor;
			spriteRenderer.color = new Color(1f,1f,1f,currentTransparency);
			this.transform.localScale = new Vector2(currentTransparency, currentTransparency);
			if (timeLeft < 0) {
				print("should do something");
				becomingInvisible = true;
				timeLeft = 10.0f;
			}
		}
	}
}
