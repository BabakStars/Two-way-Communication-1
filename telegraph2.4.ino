#include <RF24.h>
#include <SPI.h>

RF24 radio(7,8);
byte rxAddr[][6] = {"00001","00002"};
char DATA[20];
char text[20]="";

void setup() 
{
  Serial.begin(115200);
  radio.begin();
  //radio.setRetries(15,15);
  radio.openWritingPipe(rxAddr[1]);
  radio.openReadingPipe(1,rxAddr[0]);
  radio.setPALevel(RF24_PA_MIN);

}

void loop() 
{
  delay(5);
  radio.startListening();
  if (radio.available())
  {
    while(radio.available())
    {
      radio.read(DATA,sizeof(DATA));
      delay(500);
      Serial.println(DATA);
  
      delay(1);
    }
    for(int r=0 ; r<20 ; r++)
    {
      DATA[r]="";
      delay(1);
    }
  }
  delay(5);
  radio.stopListening();

  if(Serial.available())
  {
    String SS = Serial.readStringUntil('\n');
    for(int i=0 ; i<SS.length() ; i++)
    {
      text[i]=SS[i];
      delay(1);
    }
    radio.write(text,sizeof(text));
    delay(10);
    SS = "";
    for(int j=0 ; j<SS.length() ; j++)
    {
      text[j]="";
      delay(1);
    }
  }
  
}
