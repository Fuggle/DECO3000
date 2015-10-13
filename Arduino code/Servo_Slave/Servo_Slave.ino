//each of these represents one moving section of the installation

#include <Servo.h>
#include <Wire.h>

Servo servo1, servo2, servo3; //servo objects

int pos = 10; //track servo position globally.
bool isExpanding = false;
boolean isCollapsing = false;
boolean fullyExpanded = false;
boolean fullyCollapsed = false;

//int masterPinIn = 4; //input to master
//int masterPinOut = 5; //output from master
int sensorState = 0;
int ledPin = 4;
void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  Wire.begin(2); // receive device #2 messages
  Wire.onReceive(receiveEvent);
  servo1.attach(9);
  servo2.attach(8);
  servo3.attach(7);
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
  for(pos; pos <= 170; pos += 1) {
    isExpanding = true;
    fullyCollapsed = false;
    servo1.write(pos);  
    servo2.write(pos);
    servo3.write(pos);
    delay(200);
  }
  if (pos >= 170) {
    fullyExpanded = true;
    isExpanding = false;
  }
} 

}

//retract function
void collapse() {
//check if can retract  
if (!isExpanding && !isCollapsing && !fullyCollapsed) {
  
  for(pos; pos>=10; pos-=1) {
    fullyExpanded = false;
    isCollapsing = true;
    servo1.write(pos);  
    servo2.write(pos);
    servo3.write(pos);
    delay(200);
  }
  if (pos <= 10) {
    fullyCollapsed = true;
    isCollapsing = false;
  }
} 
}
