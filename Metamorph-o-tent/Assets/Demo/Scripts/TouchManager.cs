using UnityEngine;
using System;
using System.Collections;
using System.Text;
using System.IO; 
using System.Linq;
using System.Collections.Generic;

public class TouchManager : MonoBehaviour {
	//Linking to the smooth manager to get processed depth information
	public GameObject smoothManager;
	private SmoothManager _smoothManager;

	//Single frame received from the smooth manager
	private ushort[] frame;

	//Single frame pulled from the recorded smoothed snapshot
	private ushort[] baseFrame;

	//The list of frames used to determine if a touch is detected and where
	private ushort[,] buffer;

	//Setting for starting and refresh speed
	public int startDelay;
	public int touchDelay;
	public int bufferSize;
	private int formatLength;

	//Key value - 
	public int touchSensitivity;
	public int touchRange;

	//The external call to spawn the nodes
	private NodeController _nodeController;
	

	// Use this for initialization
	void Start () {

		//Setting startin variable
		startDelay = 600;
		touchDelay = 50;
		bufferSize = 50;
		formatLength = 217088;
		touchSensitivity = 200;
		touchRange = 50;

		//Setting up the smoothManager reference
		_smoothManager = smoothManager.GetComponent<SmoothManager> ();

		_nodeController = Camera.main.GetComponent<NodeController> ();

		//Setting up the size of the frame and buffer according to buffersize and formatlength variables
		frame = new ushort[formatLength];
		baseFrame = new ushort[formatLength];
		buffer = new ushort[bufferSize, formatLength];

		//Path where the file is written. root location is project root
		Load(".\\snapShot.txt");
	}
	
	// Update is called once per frame
	void Update () {
		if (startDelay <= 0) {
			frame = _smoothManager.GetData ();
			startDelay = touchDelay;
			DetectTouch();
		} else {
			startDelay--;
		}

	}

	private void DetectTouch(){
		int x = 0;
		int y = 0;
		List<int> touchLocation = new List<int> ();
		List<int> touchStrength = new List<int> ();

		//Finds all points where the current depth is much closer than the snapshot depth
		for (int i = 0; i <formatLength; i++) {
			x = i/512;
			y = i%512;
			if((int)baseFrame[i] - (int)(frame[i]) > 50 && baseFrame[i] != 0 && frame[i] != 0){
				//Debug.Log("base frame is:" + baseFrame[i]);
				//Debug.Log("current frame is:" + frame[i]);
				//Debug.Log("at index:" + i);
				touchLocation.Add(i);
				touchStrength.Add(baseFrame[i] - (frame[i]));
			}
		}
		int[] locations = touchLocation.ToArray ();
		int[] strength = touchStrength.ToArray ();

		Array.Sort (strength, locations);
		Debug.Log ("number of locations triggered = " + locations.Length);
		Debug.Log ("number of strenght recorded = " + strength.Length);
		Array.Reverse (strength);
		Array.Reverse (locations);

		//Finds a cluster of elevated points that denotes touch

		int supportingPoints = 0;
		for (int i = 0; i < strength.Length; i++) {
			supportingPoints = 0;
			for (int j = 0; j < locations.Length; j++){
				int xDistance = (locations[j]-locations[i])/512;
				int yDistance = (locations[j]-locations[i])%512;
				if( touchRange > xDistance && xDistance> -touchRange &&
				   touchRange > yDistance && yDistance > -touchRange){
					supportingPoints ++;
				}
			}

			if(supportingPoints > touchSensitivity){
				TriggerTouch(locations[i]/512, locations[i]%512);
				i = strength.Length;
			}

		}

	}

	private void TriggerTouch(int x, int y){
		Debug.Log("x is: " + x);
		Debug.Log("y is: " + y);
		Vector3 v3 = new Vector3(x/20, y/20, 12);
		_nodeController.createNode (v3);
	}
	
	private bool Load(string fileName)
	{
		// Handle any problems that might arise when reading the text
		try
		{
			string line = "";
			// Create a new StreamReader, tell it which file to read and what encoding the file
			// was saved as
			StreamReader theReader = new StreamReader(fileName, Encoding.Default);
			// Immediately clean up the reader after this block of code is done.
			// You generally use the "using" statement for potentially memory-intensive objects
			// instead of relying on garbage collection.
			// (Do not confuse this with the using directive for namespace at the 
			// beginning of a class!)
			using (theReader)
			{
				// While there's lines left in the text file, do this:
				while (line != null)
				{
					line = theReader.ReadLine();
					
					if (line != null)
					{
						// Do whatever you need to do with the text line, it's a string now
						// In this example, I split it into arguments based on comma
						// deliniators, then send that array to DoStuff()
						string[] entries = line.Split(',');
						if (entries.Length > 0){
							Insert(entries);
						}
					}
				}
				// Done reading, close the reader and return true to broadcast success    
				theReader.Close();
				return true;
			}
		}
		// If anything broke in the try block, we throw an exception with information
		// on what didn't work
		catch (Exception e)
		{
			//Debug.Log("{0}\n", e.Message);
			return false;
		}
	}

	private void Insert(string[] value){
		Debug.Log (value[23]);
		Debug.Log (value.Length);
		for (int i =0; i<(value.Length-1); i++) {
			baseFrame[i] = (ushort)Int32.Parse(value[i]);
		}
		Debug.Log (baseFrame[217086]);
		Debug.Log ("reading done!");
	}

}
