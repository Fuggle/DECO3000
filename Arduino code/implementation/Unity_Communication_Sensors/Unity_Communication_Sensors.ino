int ledPin = 6;
boolean fading = false; //is it already fading?
int fadespeed = 20; //higher == slower
int calibrationTime = 30; //pir sensor calibration time

//the time when the sensor outputs a low impulse
long unsigned int lowIn;        

//the amount of milliseconds the sensor has to be low
//before we assume all motion has stopped
long unsigned int pause = 5000; 

boolean lockLow = true;
boolean takeLowTime; 

int pirPin = 12;            //digital pin connected to the PIR's output
int pirPos = 13;           //connects to the PIR's 5V pin

void setup() {
  Serial.begin(9600);
  pinMode(ledPin, OUTPUT);
  pinMode(pirPin, INPUT);
  pinMode(pirPos, OUTPUT);
  digitalWrite(pirPos, HIGH);

  //give the sensor time to calibrate
  Serial.println("calibrating sensor ");
  for(int i = 0; i < calibrationTime; i++){
    Serial.print(calibrationTime - i);
    Serial.print("-");
    delay(1000);
  }
  Serial.println();
  Serial.println("done");

  while (digitalRead(pirPin) == HIGH) {
    delay(500);
    Serial.print(".");     
  }
  Serial.print("SENSOR ACTIVE");
}

void loop() {
  // put your main code here, to run repeatedly:
  if(digitalRead(pirPin) == HIGH){
      Serial.print("SENSOR UP");
    
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

  if(digitalRead(pirPin) == LOW){      

    if(takeLowTime){
      lowIn = millis();             //save the time of the transition from HIGH to LOW
      takeLowTime = false;    //make sure this is only done at the start of a LOW phase
    }
   
    //if the sensor is low for more than the given pause,
    //we can assume the motion has stopped
    if(!lockLow && millis() - lowIn > pause){
      //makes sure this block of code is only executed again after
      //a new motion sequence has been detected
      lockLow = true;                       
      Serial.print("motion ended at "); //output
      Serial.print((millis() - pause)/1000);
      Serial.println(" sec");
      delay(50);
    }
  }
  
  
  //Receiving value //TODO: alter for sending value instead
  /*if (Serial.available() > 0) {
     // read the incoming byte:
      char incomingByte = Serial.read();

     char on = '1';
     char off = '0';
     if (!fading) {
       if (incomingByte == on) {
        fadeIn();
        Serial.print("LED ON");
       } else if (incomingByte == off) {
        fadeOut();
        Serial.print("LED OFF");
       }
     }
  }
  Serial.flush();*/
  //delay(50);
}

void fadeIn(){
  for (int fadeValue = 0 ; fadeValue <= 255; fadeValue += 5) {
    fading = true;
    // sets the value (range from 0 to 255):
    analogWrite(ledPin, fadeValue);
    // wait for 30 milliseconds to see the dimming effect
    delay(fadespeed);
  }
  fading = false;
}

void fadeOut(){
  for (int fadeValue = 255 ; fadeValue >= 0; fadeValue -= 5) {
    fading = true;
    // sets the value (range from 0 to 255):
    analogWrite(ledPin, fadeValue);
    // wait for 30 milliseconds to see the dimming effect
    delay(fadespeed);
  }
  fading = false;
}
