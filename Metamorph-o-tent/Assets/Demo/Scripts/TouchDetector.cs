using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class TouchDetector : MonoBehaviour {
	
	public GameObject depthManager;
	public GameObject nodeController;
	
	private DepthManager _depthManager;
	private NodeController _nodeController;
	
	private ushort[] _depthData;
	public int finalFrame = 8;
	private int currentFrame = 0;
	private ushort[][] orderedDepthData = new ushort[424][];
	private ushort[][] smoothDepthData = new ushort[424][];

	private ushort[][] snapShot;
	private bool snapShotTaken = false;

	public ushort heightThreshold = (ushort)7000;

	private ushort average;

	private int numZeros = 0;
	
	// Use this for initialization
	void Start () 
	{
		_depthManager = depthManager.GetComponent<DepthManager> ();
		_nodeController = nodeController.GetComponent<NodeController> ();
		_depthData = _depthManager.GetData ();

		// create an array of rows for the depth data
		orderedDepthData = createOrderedDepthData (_depthData);
		heightThreshold = (ushort)7000;

	}

	// 526 by 412

	// Update is called once per frame
	void Update () 
	{

		//place buffer
		if (currentFrame == finalFrame && snapShotTaken == true) {
			_depthData = _depthManager.GetData ();
			orderedDepthData = createOrderedDepthData (_depthData);
			smoothDepthData = createSmoothDepthArray(orderedDepthData);
			currentFrame = 0;

			for( int y = 0; y < smoothDepthData.Length; y++) {
				for (int x = 0; x < smoothDepthData[y].Length; x++) {
					if(smoothDepthData[y][x] > (ushort)((ushort)snapShot[y][x] + (ushort)heightThreshold)) {
						_nodeController.createNode(generateNodePosition(x, y));
					}
				}
			}
			//print("stupidass " + (ushort)((ushort)7000 + (ushort)2000));

		} else {
			currentFrame++;
		}
		//print ("curr" + currentFrame + "snaptaken: " + snapShotTaken);


		
		// Used for generating level snapshot
		if (Input.GetKeyDown("s"))
		{
			_depthData = _depthManager.GetData ();
			print ("S has been pressed");
			orderedDepthData = createOrderedDepthData (_depthData);
			snapShot = createSmoothDepthArray(orderedDepthData);
			snapShotTaken = true;
			currentFrame = 0;
		}

		if (Input.GetKeyDown("m"))
		{
				print("m key is pressed");

				if (_depthData != null) {
					print ("depth data exists");
				}
				//Path where the file is written. root location is project root
				var path = ".\\testSmooth.txt";
				using (FileStream fs = new FileStream(path, FileMode.Truncate, FileAccess.Write))
				{
					using (StreamWriter sw = new StreamWriter(fs))
					{
						foreach (ushort[] array in snapShot)
						{
							foreach (ushort value in array) {
								sw.Write(Convert.ToString(value) + ",");
							}
						}
					}
				}

		}
	}
	
	ushort[][] createOrderedDepthData(ushort[] _depthData)
	{
		int index = 0;
		for (int rowNumber = 0; rowNumber < 424; rowNumber++) {
			ushort[] row = new ushort[512];
			
			for (int colNumber = 0; colNumber < 512; colNumber++) {
				row[colNumber] = _depthData[index];
				index++;
			}
			
			orderedDepthData[rowNumber] = row;
		}
		return orderedDepthData;
	}

	ushort[][] createSmoothDepthArray(ushort[][] orderedDepth)
	{
		ushort[][] smoothDepthData = new ushort[424][];

		// create an array of rows for the depth data
		for (int i = 0; i < orderedDepth.Length; i++) {
					smoothDepthData[i] = orderedDepth[i];
			
		}

		//print ("a random number is: " + orderedDepthData [3] [25]);

		// proper depth smoothing per pixel
		/*
		
		for (int y = 2; y < orderedDepthData.Length - 2; y++) {
			for (int x = 2; x < orderedDepthData[y].Length - 2; x++) {

				if (orderedDepthData[y][x] == 0) {

					numZeros++;
					// do the smoothing
					List<ushort> outerPixels = new List<ushort>();
				
					for( int iy = -2; iy < 3; iy++) {

						for (int ix = -2; ix < 3; ix++) {

							if (iy != 0 || ix != 0) {

								int searchy = y + iy;
								int searchx = x + ix;

								outerPixels.Add(orderedDepthData[searchy][searchx]);
							}

						}
					}

					ushort sum = 0;
					foreach(ushort pixel in outerPixels) {
						sum += pixel;
					}
					average = (ushort)(sum / 24);
					smoothDepthData[y][x] = average;
					//smoothDepthData[y][x] = (ushort)666;


				} else {
					//keep the pixel the same
					ushort current = orderedDepthData[y][x];
					smoothDepthData[y][x] = current;
				}

			}
		}
		*/ 

		// dodgy global averaging of zeros
		int total = 0;
		int length = 0;
		for (int y = 0; y < orderedDepth.Length; y++) {
			for (int x = 0; x < orderedDepth[y].Length; x++) {
				
				total += (int) orderedDepth[y][x];
				length ++;
			}
		}
		
		ushort totalAverage = (ushort)(total / length);
		//print ("the total average " + totalAverage);
		
		for (int y = 0; y < orderedDepth.Length; y++) {
			for (int x = 0; x < orderedDepth[y].Length; x++) {
				
				if(orderedDepth[y][x] == 0) {
					smoothDepthData[y][x] = totalAverage;
				}
			}
		} 
		//print ("smooth depth: " + orderedDepthData[45][45] + " " + smoothDepthData[45][45]);
		//print ("smooth depth length: " + smoothDepthData[3][3]);
		//print ("this is the average" + average);
		//print("number of zeros " + numZeros);
		numZeros = 0;
		return smoothDepthData;

	}

	Vector3 generateNodePosition(int y, int x) {
		double yPos = (426 - y) * 0.01;
		double xPos = x * 0.01;
		return new Vector3 ((float)xPos, (float)yPos, -10f);
	}


	
}