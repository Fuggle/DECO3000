using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour {

	public AudioClip soundLayer; //soundlayer 
	AudioSource bgm;

	public AudioClip[] layers;
	private Dictionary<string, AudioClip> sortedLayers;

	// Use this for initialization
	void Start () {

		//set up sortedLayers
		sortedLayers = new Dictionary<string, AudioClip> ();
		foreach (AudioClip layer in layers) {
			sortedLayers.Add(layer.name, layer);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//some key binds for testing functions
		if (Input.GetKeyDown ("v")) {
			Debug.Log("pressed v");
			StartCoroutine(raiseVolume(1f));
		};
		if (Input.GetKeyDown ("c")) {
			Debug.Log("pressed c");
			StartCoroutine(lowerVolume(.1f));
		};
		if (Input.GetKeyDown("l")){
			addLayer();
		};
		if (Input.GetKeyDown("j")){
			//playLayer(2);
		};

	}

	/** 
	Adjusts the volume based on size of an object.
	Accepts range between 0.5-10.
	 **/
	public IEnumerator autoVolume(float size){
		if ((size > 10f) || (size < 0.5f)) {
			//had to comment line below because it was causing errors with the IEnumerator
			//return false;
		}
		for (var i = 0; i < 10; i++){
			if (size > bgm.volume) {
				lowerVolume();
			} else {
				raiseVolume();
			}
			yield return new WaitForSeconds(.5f);
		}
	}

	//raise volume
	private void raiseVolume () {
		bgm = this.gameObject.GetComponent<AudioSource>();
		bgm.volume = bgm.volume + 0.1f;
	}

	//raise volume to the level given, gradually
	//level should be between 0 and 1
	private IEnumerator raiseVolume (float level) {
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
	private void lowerVolume () {
		bgm = this.gameObject.GetComponent<AudioSource>();
		bgm.volume = 0.1f;
	}

	//lower volume to the level given, gradually
	//level should be between 0 and 1
	private IEnumerator lowerVolume (float level) {
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

	/// <summary>
	/// Plays the sound specified in the soundName with the desired paramaters.
	/// </summary>
	/// <param name="soundName">Sound name.</param>
	/// <param name="volume">Volume.</param>
	/// <param name="repeat">Repeat.</param>
	public void playLayer(string soundName, float volume, int repeat) {
		GameObject soundSource = new GameObject();
		soundSource.AddComponent<AudioSource>();
		AudioSource sfx = soundSource.GetComponent<AudioSource>();
		sfx.volume = volume;
		print ("sorted layers: " + sortedLayers);
		sfx.PlayOneShot(sortedLayers[soundName], repeat);


	}

	//Add a new sound
	public void addLayer () {
		GameObject soundSource = new GameObject("soundLayerSource");
		soundSource.AddComponent<AudioSource>();
		AudioSource sfx = soundSource.GetComponent<AudioSource>();
		sfx.PlayOneShot(soundLayer, 1);

	}

	//Add specified sound
	/*public void addLayer () {
	
	}*/

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
