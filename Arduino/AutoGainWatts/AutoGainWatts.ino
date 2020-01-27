#include <SwitchControlLibrary.h>

uint8_t top = (uint8_t)Hat::TOP;
uint8_t right = (uint8_t)Hat::RIGHT;
uint8_t bottom = (uint8_t)Hat::BOTTOM;
uint8_t left = (uint8_t)Hat::LEFT;
uint8_t center = (uint8_t)Hat::CENTER;

void pushA(int delay1, int delay2)
{
  SwitchControlLibrary().PressButtonA();
  delay(delay1);
  SwitchControlLibrary().ReleaseButtonA();
  delay(delay2);
}

void pushB(int delay1, int delay2)
{
  SwitchControlLibrary().PressButtonB();
  delay(delay1);
  SwitchControlLibrary().ReleaseButtonB();
  delay(delay2);
}

void pushUp(int delay1, int delay2)
{
  SwitchControlLibrary().MoveHat(top);
  delay(delay1);
  SwitchControlLibrary().MoveHat(center);
  delay(delay2);
}

void pushRight(int delay1, int delay2)
{
  SwitchControlLibrary().MoveHat(right);
  delay(delay1);
  SwitchControlLibrary().MoveHat(center);
  delay(delay2);
}

void pushDown(int delay1, int delay2)
{
  SwitchControlLibrary().MoveHat(bottom);
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

void pushHome(int delay1, int delay2)
{
  SwitchControlLibrary().PressButtonHome();
  delay(delay1);
  SwitchControlLibrary().ReleaseButtonHome();
  delay(delay2);
}


void setup() {
  // put your setup code here, to run once:
  for (int i = 0; i < 20; i++)
  {
    pushB(50, 200);
  }
}

void loop() {
  // put your main code here, to run repeatedly:
  pushA(50, 250);
  pushA(50, 250);
  pushA(50, 250); // for safe
  pushA(50, 750); // for safe
  pushA(50, 2500);
  pushHome(50, 900);
  pushDown(50, 50);
  pushRight(50, 50);
  pushRight(50, 50);
  pushRight(50, 50);
  pushRight(50, 50);
  pushA(50, 50);
  pushDown(2400, 50);
  pushA(50, 50);
  pushDown(50, 50);
  pushDown(50, 50);
  pushDown(50, 50);
  pushDown(50, 50);
  pushA(50, 200);
  pushDown(50, 50);
  pushDown(50, 50);
  pushA(50, 150);
  pushRight(50, 50);
  pushRight(50, 50);
  pushUp(50, 50);
  pushA(50, 50);
  pushA(50, 50);
  pushA(50, 50);
  pushA(50, 50);
  pushHome(50, 1000);
  pushA(50, 500);
  pushB(50, 1000);
  pushA(50, 4000);
}
