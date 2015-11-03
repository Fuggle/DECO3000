using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {
	
	NodeController nodeController;
	List<GameObject> nodeList;
	float scaleAmount = 1.5f;
	float decayAmount = 0.9990f;
	public float maxScale = 12;
	float minScale = 1f;
	int passNum = 0;

	float timer = 0;
	bool scaleNode;

	public float timeOfScale = 3f;
	//Whether we are currently interpolating Scale or not
	private bool isScaling;
	
	//The start and finish positions for the interpolation
	private Vector3 startScale;
	private Vector3 endScale;
	
	//The Time.time value when we started the interpolation
	private float timeStartedScaling;

//	private ParticlePlayground playgroundController; 

	void Start () 
	{
		nodeController = Camera.main.GetComponent<NodeController> ();
		nodeList = nodeController.nodeList;
		print (transform.localScale.magnitude);

		//playergroundController = this.GetComponent<ParticlePlayground> ();

	}
	

	void Update () 
	{
		slowDecay ();

		if (isScaling) {
			if (transform.localScale.magnitude > maxScale) {
				nodeController.TriggerEvent (this.gameObject.transform);
				return;
			}

			float timeSinceStarted = Time.time - timeStartedScaling;
			float percentageComplete = timeSinceStarted / timeOfScale;

			transform.localScale = Vector3.Lerp (startScale, endScale, percentageComplete);

			if(percentageComplete >= 1.0f){
				isScaling = false;
			}
		}


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
	/// When the node is touched change color.
	/// </summary>
	public void nodeTouched()
	{
		Renderer nodeRenderer = GetComponent<Renderer>();
		nodeRenderer.material.color = Color.blue;
	}

	/// <summary>
	/// On hover of the node increase scale.
	/// </summary>
	public void nodeHover()
	{
		isScaling = true;
		timeStartedScaling = Time.time;
				
		//We set the start position to the current position, and the finish to 10 spaces in the 'forward' direction
		startScale = transform.localScale;
		endScale = new Vector3 (transform.localScale.x * scaleAmount, transform.localScale.y * scaleAmount, 
		                    transform.localScale.z * scaleAmount);

		/*
		scaleNode = true;
		if (transform.localScale.magnitude > maxScale) {
			nodeController.TriggerEvent (this.gameObject.transform);
		} else {
			transform.localScale = new Vector3 
				(transform.localScale.x * scaleAmount, transform.localScale.y * scaleAmount, transform.localScale.z * scaleAmount);
		}*/
	}

	public void nodeHover(float scaleAmount)
	{
		if (transform.localScale.magnitude > maxScale) {
			nodeController.TriggerEvent (this.gameObject.transform);
		} else {
			transform.localScale = new Vector3 
				(transform.localScale.x * scaleAmount, transform.localScale.y * scaleAmount, transform.localScale.z * scaleAmount);
		}
	}

	/// <summary>
	/// Slowly decays the node.
	/// </summary>
	void slowDecay()
	{
		transform.localScale = new Vector3 
			(transform.localScale.x * decayAmount, transform.localScale.y * decayAmount, transform.localScale.z * decayAmount);

		if (transform.localScale.magnitude < minScale) {
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

	public void reinforcePath(){
		passNum ++;
		decayAmount = decayAmount + ((1-decayAmount)/1.25f); 

	}
}
