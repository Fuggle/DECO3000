/* Sweep
 by BARRAGAN <http://barraganstudio.com> 
 This example code is in the public domain.

 modified 8 Nov 2013
 by Scott Fitzgerald
 http://arduino.cc/en/Tutorial/Sweep
*/ 

#include <Servo.h> 
 
Servo myservo1, myservo2, myservo3;  // create servo object to control a servo                 // twelve servo objects can be created on most boards
 
int pos1 = 20;    // variable to store the servo position 
 
void setup() 
{ 
  Serial.begin(9600);
  myservo1.attach(9);
  myservo2.attach(8);
  myservo3.attach(7);  // attaches the servo on pin 9 to the servo object 
} 
 
void loop() 
{ 
  for(pos1 = 20; pos1 <= 170; pos1 += 1) // goes from 0 degrees to 180 degrees 
  {                                  // in steps of 1 degree 
    myservo1.write(pos1);  
    myservo2.write(pos1);
    myservo3.write(pos1);    // tell servo to go to position in variable 'pos' 
    delay(5);                       // waits 15ms for the servo to reach the position 
  } 
   delay (3000);
   Serial.print(myservo1.read());
   Serial.print(myservo2.read());
  for(pos1 = 170; pos1>=20; pos1-=1)     // goes from 180 degrees to 0 degrees 
  {                                
    myservo1.write(pos1);              // tell servo to go to position in variable 'pos' 
    myservo2.write(pos1);
    myservo3.write(pos1);
    delay(5);                       // waits 15ms for the servo to reach the position 
  } 
   Serial.print(myservo1.read());
   Serial.print(myservo2.read());
} 

