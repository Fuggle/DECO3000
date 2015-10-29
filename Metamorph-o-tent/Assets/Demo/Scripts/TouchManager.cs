using UnityEngine;
using System.Collections;

public class TouchManager : MonoBehaviour {
	//Linking to the smooth manager to get processed depth information
	public GameObject smoothManager;
	private SmoothManager _smoothManager;

	//Single frame received from the smooth manager
	private ushort[] frame;

	//The list of frames used to determine if a touch is detected and where
	private ushort[,] buffer;

	//Setting for starting and refresh speed
	public int startDelay;
	public int bufferSize;
	private int formatLength;

	// Use this for initialization
	void Start () {
		//Setting startin variable
		startDelay = 200;
		bufferSize = 50;
		formatLength = 217088;

		//Setting up the smoothManager reference
		_smoothManager = smoothManager.GetComponent<SmoothManager> ();

		//Setting up the size of the frame and buffer according to buffersize and formatlength variables
		frame = new ushort[formatLength];
		buffer = new ushort[bufferSize, formatLength];

	}
	
	// Update is called once per frame
	void Update () {
		frame = _smoothManager.GetData ();

	}


}
