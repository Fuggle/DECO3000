using UnityEngine;
using System.Collections;
using System.IO.Ports;

/** This is for receiving of sensor states from arduino in to unity **/
public class ArduinoSensorManager : MonoBehaviour {
	
	public static SerialPort port;
	public static string strFromArduino;
	public static string message;

	private static bool screenOne;
	private static bool screenTwo;
	
	void Awake() {
		// Get a list of serial port names.
		string[] portlist = SerialPort.GetPortNames();
		
		Debug.Log("The following serial ports were found:");
		
		// Display each port name to the console.
		foreach(string p in portlist)
		{
			Debug.Log(p);
		}
		
		port = new SerialPort("COM3", 9600);
	}
	
	// Use this for initialization
	void Start () {
		OpenConnection();
		//port.Open();
	}
	
	// Update is called once per frame
	void Update () {
		//some key binds for testing functions
//		if (Input.GetKeyDown ("v")) {
//			checkState();
//			
//		};

		checkState (); //run constantly ... might need to run less constantly? *shrug*
	}
	
	public void OpenConnection() {
		if (port != null) {
			if (port.IsOpen) {
				port.Close();
				message = "Closing port, because it was already open!";
			} else {
				port.Open();  // opens the connection
				port.ReadTimeout = 50;  // sets the timeout value before reporting error
				message = "Port Opened!";
			}
		} else {
			if (port.IsOpen) {
				print("Port is already open");
			} else {
				print("Port == null");
			}
		}
	}
	
	void OnApplicationQuit() {
		port.Close ();
	}
	
	private void checkState () {
		if (!port.IsOpen) {
			port.Open ();
		}
		
		char incomingChar = (char)port.ReadChar();
		//Debug.Log(incomingChar);

		//set states based on incoming state from arduino
		//0 - none
		//1 - screen 1 only
		//2 - screen 2 only
		//3 - both
		if (incomingChar == '0') {
			Debug.Log("none");
			screenOne = false;
			screenTwo = false;
		} else if (incomingChar == '3') {
			Debug.Log("both!");
			screenOne = true;
			screenTwo = true;
		} else if (incomingChar == '1') {
			Debug.Log("trigger 1");
			screenOne = true;
			screenTwo = false;
		} else if (incomingChar == '2') {
			Debug.Log("trigger 2");
			screenOne = false;
			screenTwo = true;
		}
		 
		port.Close ();
	}

	/**
	Returns the state of screen one.
	True if activity is detected.
	False if no activity.
	 **/
	public bool checkScreenOne () {
		bool result = screenOne;
		return result;
	}


	/**
	Returns the state of screen one.
	True if activity is detected.
	False if no activity.
	 **/
	public bool checkScreenTwo () {
		bool result = screenTwo;
		return result;
	}


}
