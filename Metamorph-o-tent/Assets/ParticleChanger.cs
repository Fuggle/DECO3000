using UnityEngine;
using System.Collections;
using ParticlePlayground;

public class ParticleChanger : MonoBehaviour {

	public int NumParticles = 200;
	// Use this for initialization
	void Start () {
		PlaygroundC.SetParticleCount (GetComponent<PlaygroundParticlesC> (), NumParticles);
	}
	
	// Update is called once per frame
	void Update () {
		//PlaygroundC.SetParticleCount (GetComponent<PlaygroundParticlesC> (), NumParticles);	
	}
}
