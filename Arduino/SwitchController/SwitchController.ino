#include <SwitchControlLibrary.h>

enum class Control : uint8_t
{
  Button = 0,
  DPad,
  Stick,
};

enum class ButtonType : uint8_t
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

enum class ButtonCommand : uint8_t
{
  Press = 0,
  Release,
};

enum class DPadType : uint8_t
{
  Left = 0,
};

enum  class DPadCommand : uint8_t
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

enum class StickType : uint8_t
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

  // Serial1.begin(115200);
  Serial.begin(115200);
}

void loop()
{
  if (!Serial.available())
  {
    return;
  }

  switch ((Control)ReadSerial())
  {
    case Control::Button:
      DoButtonCommand((ButtonType)ReadSerial(), (ButtonCommand)ReadSerial());
      return;
    case Control::DPad:
      DoDPadCommand((DPadType)ReadSerial(), (DPadCommand)ReadSerial());
      return;
    case Control::Stick:
      DoStickCommand((StickType)ReadSerial(), ReadSerial(), ReadSerial());
      return;
    default:
      break;
  }
}

uint8_t ReadSerial()
{
  while (!Serial.available()) {}
  return (uint8_t)Serial.read();
}

void DoButtonCommand(ButtonType button, ButtonCommand command)
{
  if (command == ButtonCommand::Press)
  {
    PressButton(button);
    return;
  }
  if (command == ButtonCommand::Release)
  {
    ReleaseButton(button);
    return;
  }
}

void DoDPadCommand(DPadType dPad, DPadCommand command)
{
  if (dPad == DPadType::Left)
  {
    MoveDPad(command);
    return;
  }
}

void DoStickCommand(StickType stick, uint8_t x, uint8_t y)
{
  MoveStick(stick, x, y);
}

void PressButton(ButtonType button)
{
  switch (button)
  {
    case ButtonType::A:
      SwitchControlLibrary().PressButtonA();
      return;
    case ButtonType::B:
      SwitchControlLibrary().PressButtonB();
      return;
    case ButtonType::X:
      SwitchControlLibrary().PressButtonX();
      return;
    case ButtonType::Y:
      SwitchControlLibrary().PressButtonY();
      return;
    case ButtonType::L:
      SwitchControlLibrary().PressButtonL();
      return;
    case ButtonType::R:
      SwitchControlLibrary().PressButtonR();
      return;
    case ButtonType::ZL:
      SwitchControlLibrary().PressButtonZL();
      return;
    case ButtonType::ZR:
      SwitchControlLibrary().PressButtonZR();
      return;
    case ButtonType::Minus:
      SwitchControlLibrary().PressButtonMinus();
      return;
    case ButtonType::Plus:
      SwitchControlLibrary().PressButtonPlus();
      return;
    case ButtonType::LeftStick:
      SwitchControlLibrary().PressButtonLClick();
      return;
    case ButtonType::RightStick:
      SwitchControlLibrary().PressButtonRClick();
      return;
    case ButtonType::Home:
      SwitchControlLibrary().PressButtonHome();
      return;
    case ButtonType::Capture:
      SwitchControlLibrary().PressButtonCapture();
      return;
    default:
      break;
  }
}

void ReleaseButton(ButtonType button)
{
  switch (button)
  {
    case ButtonType::A:
      SwitchControlLibrary().ReleaseButtonA();
      return;
    case ButtonType::B:
      SwitchControlLibrary().ReleaseButtonB();
      return;
    case ButtonType::X:
      SwitchControlLibrary().ReleaseButtonX();
      return;
    case ButtonType::Y:
      SwitchControlLibrary().ReleaseButtonY();
      return;
    case ButtonType::L:
      SwitchControlLibrary().ReleaseButtonL();
      return;
    case ButtonType::R:
      SwitchControlLibrary().ReleaseButtonR();
      return;
    case ButtonType::ZL:
      SwitchControlLibrary().ReleaseButtonZL();
      return;
    case ButtonType::ZR:
      SwitchControlLibrary().ReleaseButtonZR();
      return;
    case ButtonType::Minus:
      SwitchControlLibrary().ReleaseButtonMinus();
      return;
    case ButtonType::Plus:
      SwitchControlLibrary().ReleaseButtonPlus();
      return;
    case ButtonType::LeftStick:
      SwitchControlLibrary().ReleaseButtonLClick();
      return;
    case ButtonType::RightStick:
      SwitchControlLibrary().ReleaseButtonRClick();
      return;
    case ButtonType::Home:
      SwitchControlLibrary().ReleaseButtonHome();
      return;
    case ButtonType::Capture:
      SwitchControlLibrary().ReleaseButtonCapture();
      return;
    default:
      break;
  }
}

void MoveDPad(DPadCommand command)
{
  SwitchControlLibrary().MoveHat(DPadCommandToHat(command));
}

uint8_t DPadCommandToHat(DPadCommand command)
{
  switch (command)
  {
    case DPadCommand::Up:
      return (uint8_t)Hat::TOP;
    case DPadCommand::UpLeft:
      return (uint8_t)Hat::TOP_LEFT;
    case DPadCommand::Left:
      return (uint8_t)Hat::LEFT;
    case DPadCommand::DownLeft:
      return (uint8_t)Hat::BOTTOM_LEFT;
    case DPadCommand::Down:
      return (uint8_t)Hat::BOTTOM;
    case DPadCommand::DownRight:
      return (uint8_t)Hat::BOTTOM_RIGHT;
    case DPadCommand::Right:
      return (uint8_t)Hat::RIGHT;
    case DPadCommand::UpRight:
      return (uint8_t)Hat::TOP_RIGHT;
    case DPadCommand::Center:
      return (uint8_t)Hat::CENTER;
    default:
      break;
  }
  return (uint8_t)Hat::CENTER;
}

void MoveStick(StickType stick, uint8_t x, uint8_t y)
{
  switch (stick)
  {
    case StickType::Left:
      SwitchControlLibrary().MoveLeftStick(x, y);
      return;
    case StickType::Right:
      SwitchControlLibrary().MoveRightStick(x, y);
      return;
    default:
      break;
  }
}
