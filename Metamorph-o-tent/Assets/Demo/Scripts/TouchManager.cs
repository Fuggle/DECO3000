using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {

	public GameObject smoothManager;
	private SmoothManager _smoothManager;


	//Setting for starting and refresh speed
	public int startDelay;
	public int detechRate;

	// Use this for initialization
	void Start () {
		_smoothManager = smoothManager.GetComponent<SmoothManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
