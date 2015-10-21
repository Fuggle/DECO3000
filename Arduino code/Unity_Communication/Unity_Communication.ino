int incomingByte = 0;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);

  Serial.write(2);
}

void loop() {
  // put your main code here, to run repeatedly:
  //Receiving value
  if (Serial.available() > 0) {
     // read the incoming byte:
     incomingByte = Serial.read();

     // say what you got:
     Serial.print("I received: ");
     Serial.println(incomingByte, DEC);
  }

}
