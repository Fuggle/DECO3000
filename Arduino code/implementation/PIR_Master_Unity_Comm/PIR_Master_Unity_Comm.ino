
//amount of time we give the sensor to calibrate(10-60 secs according to the datasheet)
int calibrationTime = 30;

//the time when the sensor outputs a low impulse
//long unsigned int lowIn1;   
//long unsigned int lowIn2;   
long unsigned int lowIn3;   
long unsigned int lowIn4; 
//long unsigned int lowIn5;   
//long unsigned int lowIn6;        

//the amount of milliseconds the sensor has to be low
//before we assume all motion has stopped
long unsigned int pause = 800; 

//boolean lockLow1 = true;
//boolean takeLow1Time;
//boolean lockLow2 = true;
//boolean takeLow2Time;
boolean lockLow3 = true;
boolean takeLow3Time;
boolean lockLow4 = true;
boolean takeLow4Time; 
//boolean lockLow5 = true;
//boolean takeLow5Time; 
//boolean lockLow6 = true;
//boolean takeLow6Time; 

//DO NOT REMOVE old refs to pins... just in case we need to reintroduce extras.
//int pirPin = 12;            //digital pin connected to the PIR's output
//int pirPos = 13;           //connects to the PIR's 5V pin
//int pirPin2 = 10;
//int pirPos2 = 11;
int pirPin3 = 9;           
int pirPos3 = 8;           
int pirPin4 = 6;
int pirPos4 = 7;
//int pirPin5 = 4;
//int pirPos5 = 5;
//int pirPin6 = 2;
//int pirPos6 = 3;

char none = '0';
char screenOne = '1';
char screenTwo = '2';
char both = '3';


void setup(){
  Serial.begin(9600);    //begins serial communication
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
  while (digitalRead(pirPin3) == HIGH || digitalRead(pirPin4) == HIGH) {
    delay(500);
    Serial.print(".");     
  }
  Serial.print("SENSOR ACTIVE");
}

void loop(){

  if(digitalRead(pirPin3) == HIGH || digitalRead(pirPin4) == HIGH){  
    checkSections();
    
    if(lockLow3){ 
      //makes sure we wait for a transition to LOW before further output is made
      lockLow3 = false;
      delay(50);
    }
    if(lockLow4){ 
      //makes sure we wait for a transition to LOW before further output is made
      lockLow4 = false;
      delay(50);
    }
    takeLow3Time = true;
    takeLow4Time = true;

  }

  if(digitalRead(pirPin3) == LOW){      
    if(takeLow3Time){
      lowIn3 = millis();             //save the time of the transition from HIGH to LOW
      takeLow3Time = false;    //make sure this is only done at the start of a LOW phase
    }
   
    //if the sensor is low for more than the given pause,
    //we can assume the motion has stopped
    if(!lockLow3 && millis() - lowIn3 > pause){
      //makes sure this block of code is only executed again after
      //a new motion sequence has been detected
      checkSections();
      lockLow3 = true;
      delay(50);
    } 
  }

  if(digitalRead(pirPin4) == LOW){      
    if(takeLow4Time){
      lowIn4 = millis();       //save the time of the transition from HIGH to LOW
      takeLow4Time = false;    //make sure this is only done at the start of a LOW phase
    }
   
    //if the sensor is low for more than the given pause,
    //we can assume the motion has stopped
    if(!lockLow4 && millis() - lowIn4 > pause){
      //makes sure this block of code is only executed again after
      //a new motion sequence has been detected
      checkSections();
      lockLow4 = true;                       
      delay(50);
    } 
  }
  
  
}

//check section
//a section will be a defined set of PIR sensor settings.
//will check all sensors and return an array of integers that correspond to the sections that should be expanded
void checkSections () {

  if (checkSectionOne() || checkSectionTwo()) {
    //send to unity
    if (checkSectionOne() && checkSectionTwo()) {
      Serial.write(both);
    } else if (checkSectionTwo()) {
      Serial.write(screenTwo);
    } else if (checkSectionOne()) {
      Serial.write(screenOne);
    }
  } else if (!checkSectionOne() && !checkSectionTwo()) {
    Serial.write(none);  
  }
  
}

//checkSections
//check each section's set of possible active states.
boolean checkSectionOne () {
  if(digitalRead(pirPin3) == HIGH){
    return true;
  }
  return false;
}

boolean checkSectionTwo () {
  if (digitalRead(pirPin4) == HIGH) {
    return true;
  } 
  return false;
}

