using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ParticlePlayground; 

/* The purpose of this script is to fade a node in or out. This script must be attached to the node prefab.
 * If you have any questions feel free to ask David Chaseling */

public class FadeInOut : MonoBehaviour {
	public float scale = 0.5f;
	public PlaygroundParticlesC visualParticles;
	public bool visable = true;
	public float fadeSpeed = 5;

	bool fadedIn = true;
	float currentLerpTime = 0;

	void Update()
	{

		// slowly fades node's particle emission settings in
		if (!fadedIn && visable) 
		{	
			//increment timer once per frame
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime > fadeSpeed) {
				currentLerpTime = fadeSpeed;
			}
			
			float perc = currentLerpTime / fadeSpeed;
			visualParticles.emissionRate = Mathf.Lerp(0,1, perc); 

			//if fading is complete..
			if(visualParticles.emissionRate == 1)
			{
				fadedIn = true;
			} else 
			{
				fadedIn = false;
			}
			
		} 
	}

	/// <summary>
	/// Sets node to be visible.
	/// </summary>
	public void visibleOn()
	{
		if (!visable) {
			print ("visible on");
			visable = true;
			visualParticles.emit = true;
			fadedIn = false;
			currentLerpTime = 0;
		}
	}

	/// <summary>
	/// Sets node to be not visible.
	/// </summary>
	public void visibleOff()
	{

		if (visable) {
			visable = false;
			visualParticles.emit = false;
			print ("visible off");
			visualParticles.emissionRate = 0;

		}
	}
}
