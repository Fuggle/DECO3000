using UnityEngine;
using System.Collections;
using System.IO.Ports;

public class ArduinoCommManager : MonoBehaviour {

	public static SerialPort port = new SerialPort("/dev/cu.usbmodem1411", 9600, Parity.None, 8, StopBits.One);
	public static string strFromArduino;
	public static string message;

	// Use this for initialization
	void Start () {
		OpenConnection();
	}
	
	// Update is called once per frame
	void Update () {
		//some key binds for testing functions
		if (Input.GetKeyDown ("v")) {
			Debug.Log("pressed v");
			port.Write("1");
		};
		strFromArduino = port.ReadLine();
		Debug.Log (strFromArduino);
		/*if (Input.GetKeyDown ("c")) {
			Debug.Log("pressed c");
			StartCoroutine(lowerVolume(.1f));
		};
		if (Input.GetKeyDown("l")){
			addLayer();
		};*/
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

}
