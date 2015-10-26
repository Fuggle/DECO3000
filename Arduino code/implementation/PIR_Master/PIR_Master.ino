#include <Wire.h>

//amount of time we give the sensor to calibrate(10-60 secs according to the datasheet)
int calibrationTime = 30;

//the time when the sensor outputs a low impulse
long unsigned int lowIn1;   
long unsigned int lowIn2;   
long unsigned int lowIn3;   
long unsigned int lowIn4;        

//the amount of milliseconds the sensor has to be low
//before we assume all motion has stopped
long unsigned int pause = 800; 

boolean lockLow1 = true;
boolean takeLow1Time;
boolean lockLow2 = true;
boolean takeLow2Time;
boolean lockLow3 = true;
boolean takeLow3Time;
boolean lockLow4 = true;
boolean takeLow4Time; 


int pirPin = 12;            //digital pin connected to the PIR's output
int pirPos = 13;           //connects to the PIR's 5V pin
int pirPin2 = 10;
int pirPos2 = 11;
int pirPin3 = 8;            //digital pin connected to the PIR's output
int pirPos3 = 9;           //connects to the PIR's 5V pin
int pirPin4 = 7;
int pirPos4 = 6;

int sections[] = {};

void setup(){
  Wire.begin();
  Serial.begin(9600);    //begins serial communication
  pinMode(pirPin, INPUT);
  pinMode(pirPos, OUTPUT);
  digitalWrite(pirPos, HIGH);
  pinMode(pirPin2, INPUT);
  pinMode(pirPos2, OUTPUT);
  digitalWrite(pirPos2, HIGH);
  pinMode(pirPin3, INPUT);
  pinMode(pirPos3, OUTPUT);
  digitalWrite(pirPos3, HIGH);
  pinMode(pirPin4, INPUT);
  pinMode(pirPos4, OUTPUT);
  digitalWrite(pirPos4, HIGH);

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
  while (digitalRead(pirPin) == HIGH || digitalRead(pirPin2) == HIGH || digitalRead(pirPin3) == HIGH || digitalRead(pirPin4) == HIGH) {
    delay(500);
    Serial.print(".");     
  }
  Serial.print("SENSOR ACTIVE");
}

void loop(){

  if(digitalRead(pirPin) == HIGH || digitalRead(pirPin2) == HIGH || digitalRead(pirPin3) == HIGH || digitalRead(pirPin4) == HIGH){  
    Wire.beginTransmission(checkSensors()); //device
    Wire.write(1);
    Wire.endTransmission();
    /*Wire.beginTransmission(7); //device #2
    Wire.write(1);
    Wire.endTransmission();
    Wire.beginTransmission(6); //device #2
    Wire.write(1);
    Wire.endTransmission();*/
    //analogWrite(slavePinIn, 255); //sends start servo message to slave arduino
    
    if(lockLow1){ 
      //makes sure we wait for a transition to LOW before further output is made
      lockLow1 = false;           
      Serial.println("---");
      Serial.print("sensor 1 motion detected at ");
      Serial.print(millis()/1000);
      Serial.println(" sec");
      delay(50);
    }
    if(lockLow2){ 
      //makes sure we wait for a transition to LOW before further output is made
      lockLow2 = false;           
      Serial.println("---");
      Serial.print("sensor 2 motion detected at ");
      Serial.print(millis()/1000);
      Serial.println(" sec");
      delay(50);
    }
    if(lockLow3){ 
      //makes sure we wait for a transition to LOW before further output is made
      lockLow3 = false;           
      Serial.println("---");
      Serial.print("sensor 3 motion detected at ");
      Serial.print(millis()/1000);
      Serial.println(" sec");
      delay(50);
    }
    if(lockLow4){ 
      //makes sure we wait for a transition to LOW before further output is made
      lockLow4 = false;           
      Serial.println("---");
      Serial.print("sensor 4 motion detected at ");
      Serial.print(millis()/1000);
      Serial.println(" sec");
      delay(50);
    }        
    takeLow1Time = true;
    takeLow2Time = true;
    takeLow3Time = true;
    takeLow4Time = true;
  }

  if(digitalRead(pirPin) == LOW || digitalRead(pirPin2) == LOW || digitalRead(pirPin3) == LOW || digitalRead(pirPin4) == LOW){      

    if(takeLow1Time){
      lowIn1 = millis();             //save the time of the transition from HIGH to LOW
      takeLow1Time = false;    //make sure this is only done at the start of a LOW phase
    }
    if(takeLow2Time){
      lowIn2 = millis();             //save the time of the transition from HIGH to LOW
      takeLow2Time = false;    //make sure this is only done at the start of a LOW phase
    }
    if(takeLow3Time){
      lowIn3 = millis();             //save the time of the transition from HIGH to LOW
      takeLow3Time = false;    //make sure this is only done at the start of a LOW phase
    }
    if(takeLow4Time){
      lowIn4 = millis();             //save the time of the transition from HIGH to LOW
      takeLow4Time = false;    //make sure this is only done at the start of a LOW phase
    }
   
    //if the sensor is low for more than the given pause,
    //we can assume the motion has stopped
    if(!lockLow1 && millis() - lowIn1 > pause){
      //makes sure this block of code is only executed again after
      //a new motion sequence has been detected
      Wire.beginTransmission(checkLowSensors()); //device #8
      Wire.write(0);
      Wire.endTransmission();
      lockLow1 = true;                       
      Serial.print("sensor 1 motion ended at "); //output
      Serial.print((millis() - pause)/1000);
      Serial.println(" sec");
      delay(50);
    }
    if(!lockLow2 && millis() - lowIn2 > pause){
      //makes sure this block of code is only executed again after
      //a new motion sequence has been detected
      Wire.beginTransmission(checkLowSensors()); //device #8
      Wire.write(0);
      Wire.endTransmission();
      lockLow2 = true;                       
      Serial.print("sensor 2 motion ended at "); //output
      Serial.print((millis() - pause)/1000);
      Serial.println(" sec");
      delay(50);
    }
    if(!lockLow3 && millis() - lowIn3 > pause){
      //makes sure this block of code is only executed again after
      //a new motion sequence has been detected
      Wire.beginTransmission(checkLowSensors()); //device #8
      Wire.write(0);
      Wire.endTransmission();
      lockLow3 = true;                       
      Serial.print("sensor 3 motion ended at "); //output
      Serial.print((millis() - pause)/1000);
      Serial.println(" sec");
      delay(50);
    }
    if(!lockLow4 && millis() - lowIn4 > pause){
      //makes sure this block of code is only executed again after
      //a new motion sequence has been detected
      Wire.beginTransmission(checkLowSensors()); //device #8
      Wire.write(0);
      Wire.endTransmission();
      lockLow4 = true;                       
      Serial.print("sensor 4 motion ended at "); //output
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
  if(digitalRead(pirPin) == HIGH && digitalRead(pirPin2) == HIGH){
    //sections[sizeof(sections)] = 6;
    return 6;
  } else if (digitalRead(pirPin2) == HIGH) {
    //sections[sizeof(sections)] = 7;
    return 7;
  } else if (digitalRead(pirPin) == HIGH) {
    //sections[sizeof(sections)] = 8;
    return 8;
  } else if (digitalRead(pirPin3) == HIGH) {
    return 5;  
  } else if (digitalRead(pirPin4) == HIGH) {
    return 4;
  }
}

//checks which sensors are low and returns which boards should start collapsing
int checkLowSensors () {
  if(digitalRead(pirPin) == LOW && digitalRead(pirPin2) == LOW){
    //sections.add();
    return 6;
  } else if (digitalRead(pirPin2) == LOW) {
    return 7;
  } else if (digitalRead(pirPin) == LOW) {
    return 8;
  } else if (digitalRead(pirPin3) == LOW) {
    return 5;  
  } else if (digitalRead(pirPin4) == LOW) {
    return 4;
  }
}


