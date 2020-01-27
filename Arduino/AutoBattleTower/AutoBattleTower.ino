#include <SwitchControlLibrary.h>

uint8_t top = (uint8_t)Hat::TOP;
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

void setup() {
  // put your setup code here, to run once:
  for (int i = 0; i < 20; i++)
  {
    pushB(50, 200);
  }
}

void loop() {
  // put your main code here, to run repeatedly:
  pushA(50, 1100);
  pushA(50, 16000);
  pushA(50, 1100);
  pushA(50, 1100);
  pushUp(50, 1100);
  pushA(50, 1100);
  pushA(50, 1100);
  pushUp(50, 1100);
  pushA(50, 1100);
  pushA(50, 1100);
  pushA(50, 16000);
  pushA(50, 1100);
  pushA(50, 1100);
  pushB(50, 1100);
  pushB(50, 1100);
  pushUp(50, 1100);
}
