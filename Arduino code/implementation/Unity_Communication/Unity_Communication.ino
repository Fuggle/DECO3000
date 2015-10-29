int ledPin = 10;

void setup() {
  pinMode(ledPin, OUTPUT);
  
  // put your setup code here, to run once:
  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  //Receiving value
  if (Serial.available() > 0) {
     // read the incoming byte:
      char incomingByte = Serial.read();

     char on = '1';
     char off = '0';
     if (incomingByte == on) {
      digitalWrite(ledPin, HIGH);
      Serial.print("LED ON");
     } else if (incomingByte == off) {
      digitalWrite(ledPin, LOW);
      Serial.print("LED OFF");
     }
  }
  Serial.flush();
  delay(50);
}
