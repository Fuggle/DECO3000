using UnityEngine;
using System.Collections;
using System.IO;
using System;
using Windows.Kinect;

public class DepthManager : MonoBehaviour {

    private KinectSensor sensor;
    private DepthFrameReader reader;
    private ushort[] data;
	private bool state;
	private int length = 0;


	// Use this for initialization
	void Start () {
        //Grab the default sensor at the start of the script
        sensor = KinectSensor.GetDefault();

        //If the sensor exists then 
        if (sensor != null)
        {
            reader = sensor.DepthFrameSource.OpenReader();
            data = new ushort[sensor.DepthFrameSource.FrameDescription.LengthInPixels];
			length = (int)sensor.DepthFrameSource.FrameDescription.LengthInPixels;
        }

		//Initial state is false, to show that data steam has not started yet
		state = false;
	}
	
	// Update is called once per frame
	void Update () {

        //Checks to see if there is a reader, if so grab depth information and 
        //put it into the list of ushort called data
	    if (reader != null)
        {
            var frame = reader.AcquireLatestFrame();
            if (frame != null)
            {
                frame.CopyFrameDataToArray(data);
                frame.Dispose();
                frame = null;
            } else {
				state = true;
			}
        }

        //When space is pressed write current data into a file
        if (Input.GetKeyDown(KeyCode.S))
        {
            print("S key is pressed");
            if(data != null)
            {
                //Path where the file is written. root location is project root
                var path = ".\\snapShot.txt";
                using (FileStream fs = new FileStream(path, FileMode.Truncate, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        foreach (ushort value in data)
                        {
                            sw.Write(Convert.ToString(value) + ",");
                        }
                    }
                }

                //Prints out a sample, if this is not zero, you are probably detecting something
                print(data[222]);
            }
        }
	}

    void OnApplicationQuit()
    {
        //If the reader is initialized then dump it on exit
        if (reader != null)
        {
            reader.Dispose();
            reader = null;
        }

        //If the sensor is initialized then dump it on exit
        if (sensor != null)
        {
            if (sensor.IsOpen)
            {
                sensor.Close();
            }
            sensor = null;
        }
    }

    public ushort[] GetData()
    {
        //returns the depth information as a list of ushort
        return data;
    }

	public bool IsReading(){
		return state;
	}

	public int GetDataLength(){
		print (length);
		return length;
	}
}
