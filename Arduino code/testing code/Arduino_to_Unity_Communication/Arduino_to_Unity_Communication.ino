int ledPin = 6;
boolean fading = false; //is it already fading?
int fadespeed = 20; //higher == slower
boolean mode = false;
long time;

void setup() {
  pinMode(ledPin, OUTPUT);
  
  // put your setup code here, to run once:
  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  //Receiving value
  char on = '1';
  char off = '0';
  
//  if (Serial.available() > 0) {
//     // read the incoming byte:
//     //char incomingByte = Serial.read();
//
//     char on = '1';
//     char off = '0';
////     if (!fading) {
////       if (incomingByte == on && !mode) {
////        fadeIn();
////        Serial.print("LED ON");/
////        mode == true;
////       } else if (incomingByte == off) {
////        fadeOut();
////        Serial.print("LED OFF");
////        mode = false;
////       }
////     }
//     
//  }

  if (mode) {
    Serial.write(on);  
    mode = false;
  } else {
    Serial.write(off);
    mode = true;
  }

  // wait 5 seconds
  delay(5000);
  Serial.flush();
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
