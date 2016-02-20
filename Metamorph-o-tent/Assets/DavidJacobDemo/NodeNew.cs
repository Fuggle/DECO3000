using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ParticlePlayground; 

public class NodeNew : MonoBehaviour {

	public PlaygroundParticlesC visualParticles;
	public float currentScale = 5, min = 3, connectionSize = 8, max = 10;

	public float growthFactor = 1.5f, timeOfScale = 3f, decayAmount = 0.9990f;
	public float sizePercentage;
	bool canConnect;
	NodeControllerNew nodeController;
	float timer = 0;
	float connectionTimer = 0, connectionCoolDown = 3;

	int reinforceNumber = 0;


	//Whether we are currently interpolating Scale or not
	bool isScaling, connectionIsReady;
	
	//The start and finish positions for the interpolation
	Vector3 startScale, endScale, newScale;


	void Start()
	{
		nodeController = Camera.main.GetComponent<NodeControllerNew> ();
	}

	void Update () 
	{

		if (currentScale >= connectionSize && isScaling || connectionIsReady) 
		{
			canConnect = true;
			print ("attempting to make a connection");
			visualParticles.initialLocalVelocityMax = new Vector3(20f,0.1f,0.1f);
			nodeController.makeConnectionsVisible (this.GetComponent<NodeNew>());
			nodeController.createConnection (this.gameObject.GetComponent<NodeNew> ());

		} else 
		{
			canConnect = false;
			visualParticles.initialLocalVelocityMax = new Vector3(0.1f,0.1f,0.1f);
		}

		currentScale = transform.localScale.x;

		slowDecay ();
		clampScale ();

		if (Input.GetKeyDown(KeyCode.R)) {
			reinforceNode();
		}
		visualParticles.scale = currentScale;



		if (isScaling) {

			timer += Time.deltaTime;
			float perc = timer / timeOfScale;
			
			transform.localScale = Vector3.Lerp (startScale, endScale, perc);
			
			if(perc >= 1.0f){
				isScaling = false;
				timer = 0;
				connectionIsReady = true;
			}
		}

		if (connectionIsReady) {
			connectionTimer += Time.deltaTime;
			if(connectionTimer >= connectionCoolDown){
				connectionIsReady = false;
				connectionTimer = 0;
			}
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
		
		if (transform.localScale.x < min + 0.5f) {
			destroyNode();
		}
	}

	/// <summary>
	/// Destroys the node.
	/// </summary>
	void destroyNode()
	{
		nodeController.removeConnections (this.GetComponent<NodeNew>());
		nodeController.removeNode (this.GetComponent<NodeNew> ());
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
		if (endScale.x >= max) {
			endScale = new Vector3 (max, max, max);
		}

	}

	/// <summary>
	/// Returns whether this node is ready to connect to others
	/// </summary>
	/// <returns><c>true</c>, if ready was connected, <c>false</c> otherwise.</returns>
	public bool connectReady() {
		return canConnect;
	}
}
