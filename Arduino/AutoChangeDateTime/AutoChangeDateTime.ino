#include <SwitchControlLibrary.h>

uint8_t top = (uint8_t)Hat::TOP;
uint8_t left = (uint8_t)Hat::LEFT;
uint8_t center = (uint8_t)Hat::CENTER;

void pushA(int delay1, int delay2)
{
  SwitchControlLibrary().PressButtonA();
  delay(delay1);
  SwitchControlLibrary().ReleaseButtonA();
  delay(delay2);
}

void pushUp(int delay1, int delay2)
{
  SwitchControlLibrary().MoveHat(top);
  delay(delay1);
  SwitchControlLibrary().MoveHat(center);
  delay(delay2);
}

void pushLeft(int delay1, int delay2)
{
  SwitchControlLibrary().MoveHat(left);
  delay(delay1);
  SwitchControlLibrary().MoveHat(center);
  delay(delay2);
}

void pushL(int delay1, int delay2)
{
  SwitchControlLibrary().PressButtonL();
  delay(delay1);
  SwitchControlLibrary().ReleaseButtonL();
  delay(delay2);
}

void setup() {
  // put your setup code here, to run once:
  for (int i = 0; i < 20; i++)
  {
    pushL(50, 200);
  }
}

const int Delay1 = 50;
const int Delay2 = 75;
const int LoopLimit = 1000;
int count = 0;

void loop() {
  // put your main code here, to run repeatedly:

  if (count < LoopLimit)
  {
    count++;
    pushA(Delay1, 125);
    pushLeft(Delay1, 75);
    pushLeft(Delay1, 75);
    pushLeft(Delay1, 75);
    pushUp(Delay1, 75);
    pushA(Delay1, 75);
    pushA(Delay1, 75);
    pushA(Delay1, 75);
    pushA(Delay1, 125);
  }
  else
  {
    delay(30000);
  }
}
