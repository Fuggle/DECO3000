using UnityEngine;
using System.Collections;
using System.IO;
using System;
using Windows.Kinect;

public class SmoothManager : MonoBehaviour {

	//The number set to smooth out the image (bigger is slower and smoother)
	public int delay = 250;
	private int current = 0;

	public GameObject depthManager;
	private DepthManager _depthManager;

	private ushort[] result;

	private ushort[,]buffer;

	private int formatLength = 0;

	// Use this for initialization
	void Start () {
		//Initializing depth manager
		_depthManager = depthManager.GetComponent<DepthManager> ();

		//Initialize format, might be zero to start with, this means the sensor has not started up
		formatLength = _depthManager.GetDataLength ();

		// ... Create 2D array of strings.
		string[,] array = new string[,]
		{
			{"cat", "dog", "monkey"},
			{"bird", "fish", "lolcat"},
		};
		// ... Print out values.
		print(array[0, 0]);
		print(array[0, 1]);
		print(array[1, 0]);
		print(array[1, 1]);
		print (array [1, 2]);

		//Initializing the 2D array
		buffer = new ushort[delay,formatLength];

		//Display the resolution
		print ("format length is:");
		print(formatLength);
	}
	
	// Update is called once per frame
	void Update () {
		if (formatLength == 0) {
			formatLength = _depthManager.GetDataLength ();
			return;
		}

		if (_depthManager.IsReading () == true) {

			AddFrame ();
		}

		//When space is pressed write current data into a file
		if (Input.GetKeyDown("space"))
		{
			print(formatLength);
			print("space key is pressed");
			print ("THANK GOD");
			if(formatLength == 0)
			{
				print ("THANK GOD");
				print(GetData()[222]);
				print ("format length is:");
				print(formatLength);
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
			current = 0;
		}
	}

	//new average is calculated
	private void NewAverage(){
		for (int y = 0; y <formatLength; y++) {
			int sum = 0;
			for (int i = 0; i < delay; i++) {
				sum = sum + buffer [i, y];
			}
			result [y] = (ushort)(sum / delay);
			if (y ==2 ){
				print ("it's working");
			}
		}
	}



	public ushort[] GetData ()
	{
		if (formatLength != 0) {
			NewAverage ();
		}

		return result;
	}
}
