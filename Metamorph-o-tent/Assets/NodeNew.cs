using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ParticlePlayground; 

public class NodeNew : MonoBehaviour {

	public PlaygroundParticlesC visualParticles;
	public float scale = 5, min = 3, max = 10;

	public float growthFactor = 1.5f, timeOfScale = 2f, decayAmount = 0.9990f;
	public float sizePercentage;
	
	float currentScale;
	Vector3 newScale;

	int reinforceNumber = 0;

	//The Time.time value when we started the interpolation
	float timeStartedScaling;

	//Whether we are currently interpolating Scale or not
	bool isScaling;
	
	//The start and finish positions for the interpolation
	Vector3 startScale;
	Vector3 endScale;
	
	void Update () {
		currentScale = transform.localScale.x;
		slowDecay ();
		clampScale ();

		if (Input.GetKeyDown(KeyCode.R)) {
			reinforceNode();
		}
		visualParticles.scale = currentScale;



		if (isScaling) {
			if (transform.localScale.x > (max - 0.1f)) {
				print ("Trigger event");
				return;
			}
			print ("is scaling");
			/*
			float timeSinceStarted = 0;
			float timeSinceStarted += Time.deltaTime;
			float percentageComplete = timeSinceStarted / timeOfScale;
			
			transform.localScale = Vector3.Lerp (startScale, endScale, percentageComplete);
			
			if(percentageComplete >= 1.0f){
				isScaling = false;
				timeSinceStarted = 0;
			}*/
		}
		
		currentScale = transform.localScale.x;
		
		sizePercentage = (1 - (max - currentScale) / (max - min));
	}


	public Vector3 LerpScale(Vector3 start, Vector3 finish, float percentage)
	{		
		//Makes sure percentage is between 0 and 1.
		percentage = Mathf.Clamp01(percentage);
		
		//The Vector3 scale between 'start' and 'finish'.
		Vector3 startToFinish = finish - start;
		
		//The remainder of scaling left.
		return start + startToFinish * percentage;
	}

	/// <summary>
	/// Slowly decreases the size of the node.
	/// </summary>
	void slowDecay()
	{
		transform.localScale = new Vector3 
			(transform.localScale.x * decayAmount, transform.localScale.y * decayAmount, transform.localScale.z * decayAmount);
		
		if (transform.localScale.magnitude < min) {
			destroyNode();
		}
	}

	/// <summary>
	/// Destroys the node.
	/// </summary>
	void destroyNode()
	{
		Destroy (this.gameObject);
	}


	/// <summary>
	/// Decreases the decay Rate of the node.
	/// </summary>
	public void reinforceNode()
	{
		reinforceNumber += 1;
	
	}

	/// <summary>
	/// Clamps the scale between the max and min node scale.
	/// </summary>
	private void clampScale()
	{
		Vector3 tempScale = transform.localScale;
		tempScale.x = Mathf.Clamp (tempScale.x, min, max);
		tempScale.y = Mathf.Clamp (tempScale.y, min, max);
		tempScale.z = Mathf.Clamp (tempScale.z, min, max);
		transform.localScale = tempScale;
	}

	/// <summary>
	/// Increases the size of the node.
	/// </summary>
	public void nodeTouch()
	{
		isScaling = true;
		startScale = transform.localScale;
		endScale = new Vector3 
			(transform.localScale.x * growthFactor, transform.localScale.y * growthFactor, transform.localScale.z * growthFactor);
		/*
		if (transform.localScale.x > max-growthFactor) {
			print ("node has reached max");
			transform.localScale = new Vector3(max,max,max);
			//nodeController.TriggerEvent (this.gameObject.transform);
		} else {
			print ("increase size of node");

			Vector3 newScale = new Vector3 
				(transform.localScale.x * growthFactor, transform.localScale.y * growthFactor, transform.localScale.z * growthFactor);
			LerpScale(transform.localScale, newScale, 0);
		}*/
	}
}
