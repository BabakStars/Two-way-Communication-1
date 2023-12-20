#include <RF24.h>
#include <SPI.h>
#include <Wire.h>
#include <Adafruit_GFX.h> 
#include <Adafruit_SSD1306.h> 

Adafruit_SSD1306 display(-1); 

RF24 radio(7,8);
byte rxAddr[][6] = {"00001","00002"};
char DATA[20];
char text[20]="";

void setup() 
{
  display.begin(SSD1306_SWITCHCAPVCC, 0x3C);
  display.clearDisplay();

  display.setTextSize(1);
  display.setTextColor(WHITE);
  display.setCursor(0,0);
  display.println("Channel:");
  display.display();
  Serial.begin(115200);
  radio.begin();
  //radio.setRetries(15,15);
  radio.openWritingPipe(rxAddr[0]);
  radio.openReadingPipe(1,rxAddr[1]);
  radio.setPALevel(RF24_PA_MIN);
}

void loop() 
{
  delay(5);
  radio.stopListening();

  if(Serial.available())
  {
        String SS = Serial.readStringUntil('\n');
        if(SS.indexOf('^')!=-1)
        {
          
          for(int i=0 ; i<SS.length() ; i++)
          {
            text[i]=SS[i];
            delay(1);
          }
          //Serial.println('\t'+text);
          radio.write(text,sizeof(text));
          delay(10);
          SS="";
          for(int j=0 ; j<SS.length() ; j++)
          {
            text[j]="";
            delay(1);
          }
       }
        if(SS.indexOf('@')!=-1)
        {
          String ch = SS.substring(1,SS.length());
          int chint = ch.toInt();
          radio.setChannel(chint);
          display.clearDisplay();

          display.setTextSize(1);
          display.setTextColor(WHITE);
          display.setCursor(0,0);
          display.println("Channel:");
          display.setTextSize(3);
          display.setTextColor(WHITE);
          display.setCursor(40,10);
          display.println(ch);
          display.display();
        }
        if(SS.indexOf('#')!=-1)
        {
          String Address = SS.substring(1,SS.length());
          
          radio.openWritingPipe(&Address);
          display.clearDisplay();

          display.setTextSize(1);
          display.setTextColor(WHITE);
          display.setCursor(0,0);
          display.println("Channel:");
          display.setTextSize(1);
          display.setTextColor(WHITE);
          display.setCursor(50,0);
          display.println(Address);
          display.display();
        }
  }
  delay(5);
  radio.startListening();
  while(radio.available())
  {
    radio.read(DATA,sizeof(DATA));
    Serial.println(DATA);

    delay(1);
  }
  for(int r=0 ; r<20 ; r++)
    {
      DATA[r]="";
      delay(1);
    }

}
