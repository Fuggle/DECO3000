/*
Adafruit Arduino - Lesson 14. Sweep
*/

#include <Servo.h> 

int servoPin = 9;
int servoPin2 = 10;
int servoPin3 = 11;
int servoPin4 = 6;
 
Servo servo;  
Servo servo2;
Servo servo3;
Servo servo4;

int angle = 0;   // servo position in degrees
int angleChange = 1;

unsigned long interval = 20;
unsigned long startTime = 0;
 
void setup() 
{ 
  servo.attach(servoPin);
  servo2.attach(servoPin2);
  servo3.attach(servoPin3);
  servo4.attach(servoPin4);
} 
 
 
void loop() 
{ 
    if (angle >= 180)
      angleChange = -1;
    else if (angle <= 0)
      angleChange = 1;

    angle += angleChange;
    servo.write(angle);
    delay(interval);
    servo2.write(angle);
    delay(interval);
    servo3.write(angle);
    delay(interval);
    servo4.write(angle);
    delay(interval);

} 