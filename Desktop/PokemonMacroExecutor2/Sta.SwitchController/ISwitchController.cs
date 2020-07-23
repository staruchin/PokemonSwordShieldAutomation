using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.SwitchController
{
    public interface ISwitchController
    {
        void PressButton(ButtonType button);
        void ReleaseButton(ButtonType button);
        void PressDPad(DPadCommand dPad);
        void ReleaseDPad();
        void MoveStick(StickType stick, byte x, byte y);
        void ReleaseStick(StickType stick);
    }
}
