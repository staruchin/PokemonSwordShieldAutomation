#include <SwitchControlLibrary.h>

enum class ControlType : int
{
  Button = 0,
  DPad,
  Stick,
};

enum class ButtonType : int
{
  A = 0,
  B,
  X,
  Y,
  L,
  R,
  ZL,
  ZR,
  Minus,
  Plus,
  LeftStick,
  RightStick,
  Home,
  Capture,
};

enum class ButtonCommand : int
{
  Press = 0,
  Release,
};

enum  class DPadCommand : int
{
  Up = 0,
  UpLeft,
  Left,
  DownLeft,
  Down,
  DownRight,
  Right,
  UpRight,
  Center,
};

enum class StickType : int
{
  Left = 0,
  Right,
};

void setup()
{
  for (int i = 0; i < 3; ++i)
  {
    SwitchControlLibrary().PressButtonZL();
    SwitchControlLibrary().PressButtonZR();
    delay(500);
    SwitchControlLibrary().ReleaseButtonZL();
    SwitchControlLibrary().ReleaseButtonZR();
    delay(500);
  }

  Serial1.begin(115200);
}

void loop()
{
  if (Serial1.available() <= 0)
  {
    return;
  }

  int control = ReadSerial();
  if (control == (int)ControlType::Button)
  {
    int button = ReadSerial();
    int buttonCommand = ReadSerial();
    DoButtonCommand(button, buttonCommand);
    return;
  }
  else if (control == (int)ControlType::DPad)
  {
    int dPadCommand = ReadSerial();
    DoDPadCommand(dPadCommand);
    return;
  }
  else if (control == (int)ControlType::Stick)
  {
    int stick = ReadSerial();
    int x = ReadSerial();
    int y = ReadSerial();
    DoStickCommand(stick, x, y);
    return;
  }
}

int ReadSerial()
{
  while (Serial1.available() <= 0) {}
  return Serial1.read();
}

void DoButtonCommand(int button, int command)
{
  switch (command)
  {
    case (int)ButtonCommand::Press:
      PressButton(button);
      return;
    case (int)ButtonCommand::Release:
      ReleaseButton(button);
      return;
    default:
      break;
  }
}

void DoDPadCommand(int command)
{
  MoveDPad(command);
}

void DoStickCommand(int stick, int x, int y)
{
  MoveStick(stick, x, y);
}

void PressButton(int button)
{
  switch (button)
  {
    case (int)ButtonType::A:
      SwitchControlLibrary().PressButtonA();
      return;
    case (int)ButtonType::B:
      SwitchControlLibrary().PressButtonB();
      return;
    case (int)ButtonType::X:
      SwitchControlLibrary().PressButtonX();
      return;
    case (int)ButtonType::Y:
      SwitchControlLibrary().PressButtonY();
      return;
    case (int)ButtonType::L:
      SwitchControlLibrary().PressButtonL();
      return;
    case (int)ButtonType::R:
      SwitchControlLibrary().PressButtonR();
      return;
    case (int)ButtonType::ZL:
      SwitchControlLibrary().PressButtonZL();
      return;
    case (int)ButtonType::ZR:
      SwitchControlLibrary().PressButtonZR();
      return;
    case (int)ButtonType::Minus:
      SwitchControlLibrary().PressButtonMinus();
      return;
    case (int)ButtonType::Plus:
      SwitchControlLibrary().PressButtonPlus();
      return;
    case (int)ButtonType::LeftStick:
      SwitchControlLibrary().PressButtonLClick();
      return;
    case (int)ButtonType::RightStick:
      SwitchControlLibrary().PressButtonRClick();
      return;
    case (int)ButtonType::Home:
      SwitchControlLibrary().PressButtonHome();
      return;
    case (int)ButtonType::Capture:
      SwitchControlLibrary().PressButtonCapture();
      return;
    default:
      break;
  }
}

void ReleaseButton(int button)
{
  switch (button)
  {
    case (int)ButtonType::A:
      SwitchControlLibrary().ReleaseButtonA();
      return;
    case (int)ButtonType::B:
      SwitchControlLibrary().ReleaseButtonB();
      return;
    case (int)ButtonType::X:
      SwitchControlLibrary().ReleaseButtonX();
      return;
    case (int)ButtonType::Y:
      SwitchControlLibrary().ReleaseButtonY();
      return;
    case (int)ButtonType::L:
      SwitchControlLibrary().ReleaseButtonL();
      return;
    case (int)ButtonType::R:
      SwitchControlLibrary().ReleaseButtonR();
      return;
    case (int)ButtonType::ZL:
      SwitchControlLibrary().ReleaseButtonZL();
      return;
    case (int)ButtonType::ZR:
      SwitchControlLibrary().ReleaseButtonZR();
      return;
    case (int)ButtonType::Minus:
      SwitchControlLibrary().ReleaseButtonMinus();
      return;
    case (int)ButtonType::Plus:
      SwitchControlLibrary().ReleaseButtonPlus();
      return;
    case (int)ButtonType::LeftStick:
      SwitchControlLibrary().ReleaseButtonLClick();
      return;
    case (int)ButtonType::RightStick:
      SwitchControlLibrary().ReleaseButtonRClick();
      return;
    case (int)ButtonType::Home:
      SwitchControlLibrary().ReleaseButtonHome();
      return;
    case (int)ButtonType::Capture:
      SwitchControlLibrary().ReleaseButtonCapture();
      return;
    default:
      break;
  }
}

void MoveDPad(int command)
{
  uint8_t hat = (uint8_t)ToHat(command);
  SwitchControlLibrary().MoveHat(hat);
}

Hat ToHat(int command)
{
  switch (command)
  {
    case (int)DPadCommand::Up:
      return Hat::TOP;
    case (int)DPadCommand::UpLeft:
      return Hat::TOP_LEFT;
    case (int)DPadCommand::Left:
      return Hat::LEFT;
    case (int)DPadCommand::DownLeft:
      return Hat::BOTTOM_LEFT;
    case (int)DPadCommand::Down:
      return Hat::BOTTOM;
    case (int)DPadCommand::DownRight:
      return Hat::BOTTOM_RIGHT;
    case (int)DPadCommand::Right:
      return Hat::RIGHT;
    case (int)DPadCommand::UpRight:
      return Hat::TOP_RIGHT;
    case (int)DPadCommand::Center:
      return Hat::CENTER;
    default:
      break;
  }
  return Hat::CENTER;
}

void MoveStick(int stick, int x, int y)
{
  switch (stick)
  {
    case (int)StickType::Left:
      SwitchControlLibrary().MoveLeftStick(x, y);
      return;
    case (int)StickType::Right:
      SwitchControlLibrary().MoveRightStick(x, y);
      return;
    default:
      break;
  }
}
