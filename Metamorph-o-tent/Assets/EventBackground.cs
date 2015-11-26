using UnityEngine;
using System.Collections;

public class EventBackground : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	float timeLeft = .5f;
	bool becomingvisible = true;
	float scaleFactor = (1f / .5f) / 60;
	float currentTransparency = 0;

	bool eventTriggered = false;
	
	// Use this for initialization
	void Start () {
		spriteRenderer = this.GetComponent<SpriteRenderer> ();
		spriteRenderer.color = new Color (1f, 1f, 1f, 0f);
	}
	
	void Update()
	{
		if (eventTriggered) {
			if (becomingvisible) {
				timeLeft -= Time.deltaTime;
				currentTransparency += scaleFactor;
				spriteRenderer.color = new Color (1f, 1f, 1f, currentTransparency);
				//this.transform.localScale = new Vector2 (currentTransparency, currentTransparency);
				if (timeLeft < 0) {
					becomingInvisible = false;
					timeLeft = .5f;
				}
			} else {
				timeLeft -= Time.deltaTime;
				//less transparency
				currentTransparency -= scaleFactor;
				spriteRenderer.color = new Color (1f, 1f, 1f, currentTransparency);
				//this.transform.localScale = new Vector2 (currentTransparency, currentTransparency);
				if (timeLeft < 0) {
					print ("should do something");
					becomingInvisible = true;
					timeLeft = .5f;
					setTrigger(false);
				}
			}
		}
	}

	public void setTrigger(bool trig ) {
		eventTriggered = trig;
	}
}
