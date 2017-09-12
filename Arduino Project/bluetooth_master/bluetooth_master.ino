/*
 * How to configure and pair two HC-05 Bluetooth Modules
 * by Dejan Nedelkovski, www.HowToMechatronics.com
 * 
 *                 == MASTER CODE ==
 */

#include <SoftwareSerial.h>
#include <SerialCommand.h>

SerialCommand sCmd;
SoftwareSerial BTserial(2, 3); 

int forwardPin = 7;
int backPin = 8;
int rightPin = 9;
int leftPin = 10;

String moveForward = "";
bool canMoveForward = true;
bool canMoveBackward= true;
bool cannotMove = false;

char currentMovement = 's';

char c = ' ';

void setup() {
  while (!Serial);
  sCmd.addCommand("x", xHandler);
  sCmd.addCommand("y", yHandler);
  sCmd.addCommand("k", kHandler);
  sCmd.addCommand("p", pHandler);
  sCmd.addCommand("w", stopHandler);
   sCmd.addCommand("t", restartHandler);

  pinMode(forwardPin, INPUT);
  pinMode(backPin, INPUT);
  pinMode(rightPin, INPUT);
  pinMode(leftPin, INPUT);
  
  Serial.begin(9600); // Default communication rate of the Bluetooth module
  Serial.println("Arduino with HC-05 is ready");
  
  BTserial.begin(38400);  
  Serial.println("BTserial started at 38400");
}

void loop() {
    if (Serial.available() > 0) {
      sCmd.readSerial();
    }
    if (!cannotMove) {
      if (digitalRead(forwardPin)) {
        if (canMoveForward) {
          if (currentMovement != 'f') {
            currentMovement = 'f';
            Serial.println("f");
          }
          Serial.println("f");
          BTserial.write('f');
        } else {
          if (currentMovement != 's') {
            currentMovement = 's';
            Serial.println("s");
          }
          BTserial.write('s');
      }     
  } else if (digitalRead(backPin)) {
    if (canMoveBackward) {
          if (currentMovement != 'b') {
            currentMovement = 'b';
            Serial.println("b");
          }
          Serial.println("b");
          BTserial.write('b');
        } else {
          if (currentMovement != 's') {
            currentMovement = 's';
            Serial.println("s");
          }
          BTserial.write('s');
      }     
  } else if (digitalRead(leftPin)) {
    Serial.println("l");
    BTserial.write('l');
  } else if (digitalRead(rightPin)) {
    Serial.println("r");
    BTserial.write('r');
  } else {
    Serial.println("s");
    BTserial.write('s');
  }    
} else {
  BTserial.write('d');
}

    
  delay(50);   
}

void xHandler () {
  canMoveForward = false;
}

void yHandler () {
  canMoveForward = true;
}

void pHandler () {
  canMoveBackward = false;
}

void kHandler () {
  canMoveBackward = true;
}

void stopHandler () {
  cannotMove = true;
}

void restartHandler () {
  cannotMove = false;
}
