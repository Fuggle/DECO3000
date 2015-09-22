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
	
	}

	//raise volume
	public void raiseVolume () {
		bgm = this.gameObject.GetComponent<AudioSource>();
		bgm.volume = 1;
	}

	//lower volume
	public void lowerVolume () {
		bgm = this.gameObject.GetComponent<AudioSource>();
		bgm.volume = 0;
		Debug.Log(bgm.isPlaying);
	}

	//Add a new sound
	public void addLayer () {
		GameObject soundSource = new GameObject("soundLayerSource");
		soundSource.AddComponent<AudioSource>();
		AudioSource sfx = soundSource.GetComponent<AudioSource>();
		sfx.PlayOneShot(soundLayer, 1);

	}

	void removeLayer () {
	}

}
