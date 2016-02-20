using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/* The main purpose of this script is to detect if a person is in front of a touchable 
 * surface and tell that surface what to do in that event. If you have any questions 
 * feel free to ask David Chaseling */

public class DetectPerson : MonoBehaviour {
	

	public GameObject surface01;
	public GameObject surface02;

	void Update () 
	{
		//REPLACE THIS WITH TRIGGERS FROM ARDUINO 
		//Surface 1
		if (Input.GetKeyDown(KeyCode.Alpha1)){
			hasEntered(surface01);
		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			hasExited (surface01);
		}

		//Surface 2
		if (Input.GetKeyDown(KeyCode.Alpha2)){
			hasEntered(surface02);
		}
		if (Input.GetKeyDown (KeyCode.W)) {
			hasExited (surface02);
		}



	}

	/// <summary>
	/// Tells every node in surface to be visible
	/// </summary>
	void hasEntered(GameObject surface)
	{
		foreach (Transform child in surface.transform)
		{
			print (child.name);
			child.GetComponent<FadeInOut>().visibleOn();
		}
	}

	/// <summary>
	/// Tells every node in surface to not be visible
	/// </summary>
	void hasExited(GameObject surface)
	{
		foreach (Transform child in surface.transform)
		{
			print (child.name);
			child.GetComponent<FadeInOut>().visibleOff();
		}
	}

}
