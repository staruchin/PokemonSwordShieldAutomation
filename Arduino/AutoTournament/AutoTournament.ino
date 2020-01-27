#include <SwitchControlLibrary.h>

void setup() {
  // put your setup code here, to run once:

}

int first = 1;
void loop() {
  // put your main code here, to run repeatedly:

  if (first != 0)
  {
    SwitchControlLibrary().MoveLeftStick(192, 0);
    first = 0;
  }
  
  for (int i = 0; i < 10; i++)
  {
    SwitchControlLibrary().PressButtonA();
    delay(50);
    SwitchControlLibrary().ReleaseButtonA();
    delay(450);
  }
  SwitchControlLibrary().PressButtonB();
  delay(50);
  SwitchControlLibrary().ReleaseButtonB();
  delay(450);
}
