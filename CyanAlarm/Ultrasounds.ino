#include <String.h>
#define TRIG_PIN 8
#define ECHO_PIN0 9
#define ECHO_PIN1 10
#define ECHO_PIN2 11
#define LIGHT0 6
#define LIGHT1 7

static bool pin0_Active = true; //porta principale
static bool pin1_Active = true; //finestra
static bool pin2_Active = false; //veranda

void setup() {
  Serial.begin(9600);
  pinMode(TRIG_PIN, OUTPUT);
  pinMode(ECHO_PIN0, INPUT);
  pinMode(ECHO_PIN1, INPUT);
  pinMode(ECHO_PIN2, INPUT);
  digitalWrite(TRIG_PIN, LOW);
  pinMode(LIGHT0, OUTPUT);
  pinMode(LIGHT1, OUTPUT);
  digitalWrite(LIGHT0, HIGH);
  digitalWrite(LIGHT1, HIGH);
}

int DELAY = 100;
unsigned long time0;
unsigned long time1;
unsigned long time2;
float distance0;
float distance1;
float distance2;
void loop() {

      if(pin0_Active){
        digitalWrite(TRIG_PIN, HIGH);
        delayMicroseconds(10);
        digitalWrite(TRIG_PIN, LOW);
  
        time0 = pulseIn(ECHO_PIN0, HIGH);
        distance0 = 0.03438 * time0 /2;
        Serial.println("Pin0: " + String(distance0));
        delay(DELAY);
      }

      if(pin1_Active){
        digitalWrite(TRIG_PIN, HIGH);
        delayMicroseconds(10);
        digitalWrite(TRIG_PIN, LOW);
  
        time1 = pulseIn(ECHO_PIN1, HIGH);
        distance1 = 0.03438 * time1 /2;
        Serial.println("Pin1: " + String(distance1));
        delay(DELAY);
      }
  
      if(pin2_Active){
        digitalWrite(TRIG_PIN, HIGH);
        delayMicroseconds(10);
        digitalWrite(TRIG_PIN, LOW);
  
        time2 = pulseIn(ECHO_PIN2, HIGH);
        distance2 = 0.03438 * time2 /2;
        Serial.println("Pin2: " + String(distance2));
        delay(DELAY);
      }

      
      for(int i=0; i<Serial.available(); i++){
          
          char ch = Serial.read();
          if(i==0){
            if(ch == 'N') {digitalWrite(LIGHT0, HIGH);  }
            else {digitalWrite(LIGHT0, LOW);  }
          }
          else if(i==1){
            if(ch == 'N') {digitalWrite(LIGHT1, HIGH);  }
            else {digitalWrite(LIGHT1, LOW);  }
          }
      }
      while(Serial.available() > 0) {
        char t = Serial.read();
      }
      
}
