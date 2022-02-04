#include <SoftwareSerial.h>  //Abrir librería serial <softwareSerial.h>
SoftwareSerial mySerial(7, 6); //Puertos de recepción RX 7 y transmisión TX 6
int gatillo = 2;
int electro = 8;
int val;
void setup() {
   Serial.begin(9600);
   mySerial.begin(9600);
   pinMode(gatillo, INPUT);
   pinMode(electro, OUTPUT);
   digitalWrite(electro,LOW);
}
void loop() {
   val=digitalRead(gatillo);
  if(val==HIGH){
    digitalWrite(electro,HIGH);
    delay(200);
    digitalWrite(electro,LOW);
    delay(200);
    mySerial.println(val);
    //Serial.println(val);
  }
}
