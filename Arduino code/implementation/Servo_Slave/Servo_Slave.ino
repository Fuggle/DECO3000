//each of these represents one moving section of the installation

#include <Servo.h>
#include <Wire.h>

Servo servo1, servo2, servo3, servo4; //servo objects

int deviceNo = 7; // CHANGE THIS PER DEVICE (count down from 8)
int pos1 = 10; //track servo position globally.
int pos2 = 170;
bool isExpanding = false;
boolean isCollapsing = false;
boolean fullyExpanded = false;
boolean fullyCollapsed = false;

//int masterPinIn = 4; //input to master
//int masterPinOut = 5; //output from master
int sensorState = 0;
int ledPin = 10;
void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  Wire.begin(deviceNo); // receive device # message
  Wire.onReceive(receiveEvent);
  servo1.attach(9);
  servo2.attach(8);
//  servo3.attach(7);
//  servo4.attach(6);
  pinMode(ledPin, OUTPUT);
}

void loop() {
  // put your main code here, to run repeatedly:
  /*sensorState = analogRead(masterPinOut);
  Serial.println(sensorState);
  if (sensorState == 255) {
    digitalWrite(ledPin, HIGH);
  } else {
    digitalWrite(ledPin, LOW);
  }*/
}

void receiveEvent(int howMany) {
  int x = Wire.read();
  if (x > 0) {
    digitalWrite(ledPin, HIGH);
    expand();
  } else {
    digitalWrite(ledPin, LOW);
    collapse();
  }
}

//expand function
void expand() {
//check if can expand
  if (!isExpanding && !isCollapsing && !fullyExpanded) {
    for(pos1 = 10; pos1 <= 170; pos1 += 1) { 
      isExpanding = true;
      fullyCollapsed = false;
      //pos2 = 180 - pos1;
      servo1.write(pos1);  
      servo2.write(pos1);
//      servo3.write(pos1);
//      servo4.write(pos2);
      delay(80); 
    } 

    if (pos1 >= 170) {
      fullyExpanded = true;
      isExpanding = false;
    }
  } 
}

//retract function
void collapse() {
//check if can retract  
  if (!isExpanding && !isCollapsing && !fullyCollapsed) {
    for(pos1 = 170; pos1>=10; pos1-=1) {
      fullyExpanded = false;
      isCollapsing = true;        
      //pos2 = 180 - pos1;    
      servo1.write(pos1);
      servo2.write(pos1);
//      servo3.write(pos1);
//      servo4.write(pos2);
      delay(80); 
    } 
    
    if (pos1 <= 10) {
      fullyCollapsed = true;
      isCollapsing = false;
    }
  } 
}
