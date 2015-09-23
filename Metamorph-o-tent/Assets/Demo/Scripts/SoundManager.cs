using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public AudioClip soundLayer; //soundlayer 
	AudioSource bgm;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//some key binds for testing functions
		/*if (Input.GetKeyDown ("v")) {
			Debug.Log("pressed v");
			StartCoroutine(raiseVolume(1f));
		};
		if (Input.GetKeyDown ("c")) {
			Debug.Log("pressed c");
			StartCoroutine(lowerVolume(.1f));
		};
		if (Input.GetKeyDown("l")){
			addLayer();
		};*/

	}

	//raise volume
	public void raiseVolume () {
		bgm = this.gameObject.GetComponent<AudioSource>();
		bgm.volume = 1;
	}

	//raise volume to the level given, gradually
	//level should be between 0 and 1
	public IEnumerator raiseVolume (float level) {
		bgm = this.gameObject.GetComponent<AudioSource> ();
		Debug.Log ("raising volume...");
		//make sure level given is between 0 and 1;
		if (level > 1) {
			level = 1;
		} else if (level < 0) {
			level = 0;
		}
	
		//i starts at current volume.
		for (var i = bgm.volume * 10; i < (level * 10); i++){
			bgm.volume = (i/10) + 0.1f;
			yield return new WaitForSeconds(.5f);
		}
		Debug.Log (bgm.volume);
	}

	//lower volume
	public void lowerVolume () {
		bgm = this.gameObject.GetComponent<AudioSource>();
		bgm.volume = 0.1f;
	}

	//lower volume to the level given, gradually
	//level should be between 0 and 1
	public IEnumerator lowerVolume (float level) {
		bgm = this.gameObject.GetComponent<AudioSource> ();
		Debug.Log ("lowering volume...");
		//make sure level given is between 0 and 1;
		if (level > 1) {
			level = 1;
		} else if (level < 0) {
			level = 0;
		}

		//i starts at current volume.
		for (var i = bgm.volume * 10; i > (level * 10); i--){
			bgm.volume = (i/10) * 0.1f;
			yield return new WaitForSeconds(.5f);
		}

		Debug.Log (bgm.volume);
	}

	//Add a new sound
	public void addLayer () {
		GameObject soundSource = new GameObject("soundLayerSource");
		soundSource.AddComponent<AudioSource>();
		AudioSource sfx = soundSource.GetComponent<AudioSource>();
		sfx.PlayOneShot(soundLayer, 1);

	}

	public IEnumerator pitchUp (float level) {
		bgm = this.gameObject.GetComponent<AudioSource> ();
		float startingPitch = bgm.pitch;
		for (int i = 0; i < level; i++) {
			bgm.pitch += i * 0.1f;
			yield return new WaitForSeconds(.5f);
		}
	}

	public IEnumerator pitchDown (float level) {
		bgm = this.gameObject.GetComponent<AudioSource> ();
		float startingPitch = bgm.pitch;
		for (int i = 10; i > level; i--) {
			bgm.pitch -= i * 0.1f;
			yield return new WaitForSeconds(.5f);
		}
	}

}
