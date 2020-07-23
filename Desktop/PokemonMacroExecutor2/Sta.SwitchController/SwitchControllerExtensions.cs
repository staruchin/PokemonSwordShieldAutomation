using System;
using System.Threading;

namespace Sta.SwitchController
{
    public static class SwitchControllerExtensions
    {
        public static void PressAndReleaseButton(this ISwitchController controller, ButtonType button, int duration, int wait = 0)
        {
            if (controller == null) { throw new ArgumentNullException(nameof(controller)); }

            controller.PressButton(button);
            Thread.Sleep(duration);
            controller.ReleaseButton(button);
            Thread.Sleep(wait);
        }

        public static void PressAndReleaseDPad(this ISwitchController controller, DPadCommand dPad, int duration, int wait = 0)
        {
            if (controller == null) { throw new ArgumentNullException(nameof(controller)); }

            controller.PressDPad(dPad);
            Thread.Sleep(duration);
            controller.ReleaseDPad();
            Thread.Sleep(wait);
        }

        public static void MoveAndReleaseStick(this ISwitchController controller, StickType stick, byte x, byte y, int duration, int wait = 0)
        {
            if (controller == null) { throw new ArgumentNullException(nameof(controller)); }

            controller.MoveStick(stick, x, y);
            Thread.Sleep(duration);
            controller.ReleaseStick(stick);
            Thread.Sleep(wait);
        }
    }
}
