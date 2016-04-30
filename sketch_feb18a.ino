#include <SPI.h>
#include <Ethernet.h>
#include <OneWire.h>
#include <DallasTemperature.h>
#include <LiquidCrystal.h>

// Data wire is plugged into port 2 on the Arduino
#define ONE_WIRE_BUS 2
#define RELE_PIN A0
#define LED_RELE_PIN A1
#define RESOLUTION 12

long sendTimer = 0;
long printTimer = 0;
long releTimer = 0;
const int TIME_OUT = 5000; // 5 seconds
const int PRINT_TIME_OUT = 2000;
const int RELE_TIME_OUT = 2000;

float bsumTemp = 0;
float bcountOfTemps = 0;

float hsumTemp = 0;
float hcountOfTemps = 0;

OneWire oneWire(ONE_WIRE_BUS);
DallasTemperature sensors(&oneWire);

LiquidCrystal lcd(3, 4, 5, 6, 7, 8);

// arrays to hold device address
DeviceAddress boilerThermometer = { 0x28, 0xFF, 0xBE, 0x9D, 0x72, 0x15, 0x02, 0xD6 };
DeviceAddress homeThermometer = { 0x28, 0xFF, 0x2C, 0x5F, 0x06, 0x00, 0x00, 0x02 };

byte mac[] = { 0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED };
IPAddress ip(192, 168, 0, 177);
char server[] = "aohouse.1gb.ru";

EthernetClient client;

void initEthernet()
{
  if (Ethernet.begin(mac) == 0) {
    Ethernet.begin(mac, ip);
  }
  delay(1000); // give the Ethernet shield a second to initialize
}

void initTempSensors()
{
  sensors.begin();
  sensors.setResolution(boilerThermometer, RESOLUTION);
  sensors.setResolution(homeThermometer, RESOLUTION);
}

void initRele()
{
  pinMode(RELE_PIN, OUTPUT);
  pinMode(LED_RELE_PIN, OUTPUT);
}

void sendData(int index, int temp)
{
  if (client.connect(server, 80)) {

    String url = "GET /api/sensor/add/" + String(index) + "/" + String(temp) + " HTTP/1.1";

    Serial.println(url);

    client.println(url);
    client.print("Host: ");
    client.println(server);
    client.println("Connection: close");
    client.println();

    client.flush();
    client.stop();
  } else {
    // if you didn't get a connection to the server:
    //Serial.println("Connection failed!");
  }
}

void setup() {
  initEthernet();
  initTempSensors();
  initRele();

  Serial.begin(115200);
}

void printTemp(float tempBoiler, float tempHome)
{
  if (tempBoiler > -100 && tempHome > -100)
  {
    if ((millis() - printTimer) >= PRINT_TIME_OUT)
    {
      lcd.clear();

      lcd.begin(16, 2);
      lcd.print("Room: ");
      lcd.print(tempHome);

      lcd.setCursor(0, 1);
      lcd.print("Boiler: ");
      lcd.print(tempBoiler);

      printTimer = millis();
    }
  }
}

bool ReleIsTurnOn(int index)
{
  char inChar;
  String currentLine = "";

  if (client.connect(server, 80))
  {
    String url = "GET /api/socket/" + String(index) + " HTTP/1.1";

    client.println(url);
    client.print("Host: ");
    client.println(server);
    client.println("Connection: close");
    client.println();

    client.flush();
  }
  else
  {
    return false;
  }

  // connectLoop controls the hardware fail timeout
  int connectLoop = 0;

  while (client.connected())
  {
    while (client.available())
    {
      inChar = client.read();
      currentLine += inChar;
      if (inChar == '\n') {
        currentLine = "";
      }
      connectLoop = 0;
    }

    connectLoop++;
    if (connectLoop > 10000)
    {
      client.stop();
    }
    delay(1);
  }
  client.stop();

  bool releIsTurnOn = currentLine.indexOf("\"IsTurnOn\":true") != -1;
  return releIsTurnOn;
}

void checkReleState()
{
  if ((millis() - releTimer) >= RELE_TIME_OUT)
  {
    if (ReleIsTurnOn(1))
    {
      digitalWrite(RELE_PIN, HIGH);
      digitalWrite(LED_RELE_PIN, HIGH);
    }
    else
    {
      digitalWrite(RELE_PIN, LOW);
      digitalWrite(LED_RELE_PIN, LOW);
    }
    releTimer = millis();
  }
}

int hlastAvgTemp = 0;
int blastAvgTemp = 0;

void sendTemps()
{
  if ((millis() - sendTimer) >= TIME_OUT)
  {
    int havgTemp = (int)(hsumTemp / hcountOfTemps);
    if (havgTemp != hlastAvgTemp)
    {
      sendData(1, havgTemp);
      hlastAvgTemp = havgTemp;
    }
    hcountOfTemps = 0;
    hsumTemp = 0;

    int bavgTemp = (int)(bsumTemp / bcountOfTemps);
    if (bavgTemp != blastAvgTemp)
    {
      sendData(2, bavgTemp);
      blastAvgTemp = bavgTemp;
    }
    bcountOfTemps = 0;
    bsumTemp = 0;

    sendTimer = millis();
  }
}

void getTemps()
{
  sensors.requestTemperatures();

  float htemp = sensors.getTempC(homeThermometer);
  if (htemp > -100)
  {
    hsumTemp += htemp;
    hcountOfTemps++;
  }

  float btemp = sensors.getTempC(boilerThermometer);
  if (btemp > -100)
  {
    bsumTemp += btemp;
    bcountOfTemps++;
  }
  printTemp(btemp, htemp);
}

void loop() {

  sendTemps();

  getTemps();

  checkReleState();
}
