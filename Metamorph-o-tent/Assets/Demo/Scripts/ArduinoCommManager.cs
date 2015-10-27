using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class ArduinoCommManager : MonoBehaviour {

	public static SerialPort port;
	public static string strFromArduino;
	public static string message;

	void Awake() {
		// Get a list of serial port names.
//		string[] portlist = SerialPort.GetPortNames();
//		
//		Debug.Log("The following serial ports were found:");
//		
//		// Display each port name to the console.
//		foreach(string p in portlist)
//		{
//			Debug.Log(p);
//		}

		port = new SerialPort("COM3", 9600);
	}

	// Use this for initialization
	void Start () {
		//OpenConnection();
		port.Open();
	}
	
	// Update is called once per frame
	void Update () {
		//some key binds for testing functions
		if (Input.GetKeyDown ("v")) {
			Debug.Log("pressed v");
			port.Write("1");
		};
		if (Input.GetKeyDown ("t")) {
			Debug.Log("pressed t");
			port.Write("0");
		};
		//strFromArduino = port.ReadLine();
		//Debug.Log(strFromArduino);

		/*if (Input.GetKeyDown ("c")) {
			Debug.Log("pressed c");
			StartCoroutine(lowerVolume(.1f));
		};
		if (Input.GetKeyDown("l")){
			addLayer();
		};*/
	}

//	public void OpenConnection() {
//		if (port != null) {
//			if (port.IsOpen) {
//				port.Close();
//				message = "Closing port, because it was already open!";
//			} else {
//				port.Open();  // opens the connection
//				port.ReadTimeout = 50;  // sets the timeout value before reporting error
//				message = "Port Opened!";
//			}
//		} else {
//			if (port.IsOpen) {
//				print("Port is already open");
//			} else {
//				print("Port == null");
//			}
//		}
//	}
	
	void OnApplicationQuit() {
		port.Close ();
	}

		
//	SerialPort stream = new SerialPort("COM3", 9600); //Set the port (com4) and the baud rate (9600, is standard on most devices)
//	float[] initState = {0,0,0}; //Need the last rotation to tell how far to spin the camera
//	
//	
//
//	void Start () {
//		stream.Open(); //Open the Serial Stream.
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		string value = stream.ReadLine(); //Read the information
//		string[] vec3;
//		if(vec3[0] != "" && vec3[1] != "" && vec3[2] != "") //Check if all values are recieved
//		{
//			transform.Move(           //Rotate the camera based on the new values
//			               float.Parse(vec3[0])-initState [0],
//			               float.Parse(vec3[1])-initState [1],
//			               float.Parse(vec3[2])-initState [2],
//			               Space.Self
//			               );
//			initState [0] = float.Parse(vec3[0]);  //Set new values to last time values for the next loop
//			initState [1] = float.Parse(vec3[1]);
//			initState [2] = float.Parse(vec3[2]);
//			stream.BaseStream.Flush();
//		}
//	}
//	
//	
//	
//	void OnGUI() {
//		string newString = "Connected: " + transform.move.x + ", " + transform.move.y + ", " + transform.move.z;
//		GUI.Label(new Rect(10,10,300,100), newString); //Display new values
//	}


}
