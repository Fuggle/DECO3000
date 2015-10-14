#include <Wire.h>

//amount of time we give the sensor to calibrate(10-60 secs according to the datasheet)
int calibrationTime = 30;

//the time when the sensor outputs a low impulse
long unsigned int lowIn;        

//the amount of milliseconds the sensor has to be low
//before we assume all motion has stopped
long unsigned int pause = 800; 

boolean lockLow = true;
boolean takeLowTime; 

int pirPin = 12;            //digital pin connected to the PIR's output
int pirPos = 13;           //connects to the PIR's 5V pin
int pirPin2 = 10;
int pirPos2 = 11;

void setup(){
  Wire.begin();
  Serial.begin(9600);    //begins serial communication
  pinMode(pirPin, INPUT);
  pinMode(pirPos, OUTPUT);
  digitalWrite(pirPos, HIGH);
  pinMode(pirPin2, INPUT);
  pinMode(pirPos2, OUTPUT);
  digitalWrite(pirPos2, HIGH);

  //give the sensors time to calibrate
  Serial.println("calibrating sensor ");
  for(int i = 0; i < calibrationTime; i++){
    Serial.print(calibrationTime - i);
    Serial.print("-");
    delay(1000);
  }
  Serial.println();
  Serial.println("done");
 
  //while making this Instructable, I had some issues with the PIR's output
  //going HIGH immediately after calibrating
  //this waits until the PIR's output is low before ending setup
  while (digitalRead(pirPin) == HIGH || digitalRead(pirPin2) == HIGH) {
    delay(500);
    Serial.print(".");     
  }
  Serial.print("SENSOR ACTIVE");
}

void loop(){

  if(digitalRead(pirPin) == HIGH || digitalRead(pirPin2) == HIGH){  
    Wire.beginTransmission(checkSensors()); //device #2
    Wire.write(1);
    Wire.endTransmission();
    /*Wire.beginTransmission(7); //device #2
    Wire.write(1);
    Wire.endTransmission();
    Wire.beginTransmission(6); //device #2
    Wire.write(1);
    Wire.endTransmission();*/
    //analogWrite(slavePinIn, 255); //sends start servo message to slave arduino
    Serial.println("motion");
    if(lockLow){ 
      //makes sure we wait for a transition to LOW before further output is made
      lockLow = false;           
      Serial.println("---");
      Serial.print("motion detected at ");
      Serial.print(millis()/1000);
      Serial.println(" sec");
      delay(50);
    }        
    takeLowTime = true;
  }

  if(digitalRead(pirPin) == LOW && digitalRead(pirPin2) == LOW){      

    if(takeLowTime){
      lowIn = millis();             //save the time of the transition from HIGH to LOW
      takeLowTime = false;    //make sure this is only done at the start of a LOW phase
    }
   
    //if the sensor is low for more than the given pause,
    //we can assume the motion has stopped
    if(!lockLow && millis() - lowIn > pause){
      //makes sure this block of code is only executed again after
      //a new motion sequence has been detected
      Wire.beginTransmission(8); //device #8
      Wire.write(0);
      Wire.endTransmission();
      Wire.beginTransmission(7); //device #7
      Wire.write(0);
      Wire.endTransmission();
      Wire.beginTransmission(6); //device #6
      Wire.write(0);
      Wire.endTransmission();
      //analogWrite(slavePinIn, 0); //sends stop servo message to arduino board
      lockLow = true;                       
      Serial.print("motion ended at "); //output
      Serial.print((millis() - pause)/1000);
      Serial.println(" sec");
      delay(50);
    }
  }
}

//check section
//a section will be a defined set of PIR sensor settings.
//will check all sensors and return an array of integers that correspond to the sections that should be expanded
int checkSensors () {
  //int sections[] = {};
  if(digitalRead(pirPin) == HIGH && digitalRead(pirPin2) == LOW){
    //sections.add();
    return 8;
  } else if (digitalRead(pirPin) == LOW && digitalRead(pirPin2) == HIGH) {
    return 7;
  } else if (digitalRead(pirPin) == HIGH && digitalRead(pirPin2) == HIGH) {
    return 6;
  }
  
}

