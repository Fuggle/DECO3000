using UnityEngine;
using System.Collections;
using System.IO;
using System;
using Windows.Kinect;

public class SmoothManager : MonoBehaviour {

	//The number set to smooth out the image (bigger is slower and smoother)
	public int delay;
	private int current;

	public GameObject depthManager;
	private DepthManager _depthManager;

	//The depth data after the smoothing process
	private ushort[] result;

	//The set of depth data used to smooth out
	private ushort[,]buffer;

	//Number of pixels in kinect depth capture
	private int formatLength;


	// Use this for initialization
	void Start () {
		//Setting startin variable
		delay = 25;
		current = 0;
		formatLength = 217088;

		//Initializing depth manager
		_depthManager = depthManager.GetComponent<DepthManager> ();

		//Initializing the 2D array
		buffer = new ushort[delay,formatLength];
		result = new ushort[formatLength];

		//Display the resolution
		print ("Current depth format length is:");
		print(formatLength);
	}
	
	// Update is called once per frame
	void Update () {
		//When the depth information is reading, start adding frames to the buffer
		if (_depthManager.IsReading () == true) {
			AddFrame ();
		}

		//When S is pressed write current smoothed out depth data into a file
		if (Input.GetKeyDown(KeyCode.S))
		{
			print("S key is pressed");
			print("A smoothed out snapshot is stored");
			if(formatLength != 0)
			{
				BetterAverage();
				print(result[222]);

				//Path where the file is written. root location is project root
				var path = ".\\snapShot.txt";
				using (FileStream fs = new FileStream(path, FileMode.Truncate, FileAccess.Write))
				{
					using (StreamWriter sw = new StreamWriter(fs))
					{
						foreach (ushort value in result)
						{
							sw.Write(Convert.ToString(value) + ",");
						}
					}
				}

				//Prints out a sample, if this is not zero, you are probably detecting something
			}
		}

		//When A is pressed display the current sample depth
		if (Input.GetKeyDown(KeyCode.A))
		{
			print("A key is pressed");
			print("Sample depth:");
			if(formatLength != 0)
			{
				NewAverage();
				print(result[1]);
			}
		}

	}

	//Add frame into the list of buffer frames ready to be smoothed
	private void AddFrame(){
		if (current < delay) {
			ushort[] data = new ushort[formatLength];
			data = _depthManager.GetData ();
			//Buffer frames are not yet filled, no average is taken
			for (int i = 0; i < formatLength; i++) {
				buffer [current, i] = data [i];
			}
			current ++;
		} else {
			//After the frame has been fill, new pop in is needed pop out is needed, average calculated
			//print ("reset!");
			//print(current);
			current = 0;
		}
	}

	//new average is calculated
	private void BetterAverage(){
		int discarded = 0;
		for (int j = 0; j <formatLength; j++) {
			//Sum is the sum of all the depths in the buffer
			int sum = 0;
			//max is the highest value one
			int max = 0;
			//min is the lowest value one
			int min = -1;
			for (int i = 0; i < delay; i++) {
				//Addes all the values up so the average can be found
				sum = sum + (int)buffer [i, j];

				//Finds the max
				if((int)buffer [i, j] > max){
					max = (int)buffer [i, j];
				}

				//Finds the min
				if(min == -1 || min > (int)buffer [i, j]){
					min = (int)buffer [i, j];
				}
			}

			//An average of 0 will be given if the fluctation on this point is too much (more than 50mm)
			if(max - min < 50){
				result [j] = (ushort)(sum / delay);
			}else{
				discarded ++;
				result [j] = 0;
			}

		}
		//Debug.Log ("discarded amount = " + discarded);
	}

	//new average is calculated
	private void NewAverage(){
		for (int j = 0; j <formatLength; j++) {
			//Sum is the sum of all the depths in the buffer
			int sum = 0;
			for (int i = 0; i < delay; i++) {
				//Addes all the values up so the average can be found
				sum = sum + (int)buffer [i, j];

			}

			result [j] = (ushort)(sum / delay);

			
		}
	}



	public ushort[] GetData ()
	{
		BetterAverage ();
		return result;
	}
}
