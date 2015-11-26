using UnityEngine;
using System.Collections;

public class EventBackground : MonoBehaviour {

	SpriteRenderer spriteRenderer;
	float timeLeft = .8f;
	bool becomingvisible = true;
	float scaleFactor = (1f / .8f) / 60;
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
					becomingvisible = false;
					timeLeft = .8f;
				}
			} else {
				timeLeft -= Time.deltaTime;
				//less transparency
				currentTransparency -= scaleFactor;
				spriteRenderer.color = new Color (1f, 1f, 1f, currentTransparency);
				//this.transform.localScale = new Vector2 (currentTransparency, currentTransparency);
				if (timeLeft < 0) {
					print ("should do something");
					becomingvisible = true;
					timeLeft = .8f;
					setTrigger(false);
					spriteRenderer.color = new Color (1f, 1f, 1f, 0f);
				}
			}
		}
	}

	public void setTrigger(bool trig ) {
		eventTriggered = trig;
	}
}
