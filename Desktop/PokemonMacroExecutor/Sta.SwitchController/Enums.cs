using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.SwitchControllerLib
{
    public enum ControlType : byte
    {
        Button = 0,
        DPad,
        Stick,
    }

    public enum ButtonType : byte
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
    }

    public enum ButtonCommand : byte
    {
        Press = 0,
        Release,
    }

    public enum DPadCommand : byte
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
    }

    public enum StickType : byte
    {
        Left = 0,
        Right,
    }
}
