using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Reactive.Disposables;
using System.Threading;

namespace Sta.SwitchController
{
    public abstract class AbstractSwitchController
    {
        public void Push(ButtonType button)
        {
            Push(button, DefaultPushDuration);
        }

        public void Push(ButtonType button, TimeSpan duration)
        {
            Press(button);
            Thread.Sleep(duration);
            Release(button);
        }

        public void Press(ButtonType button)
        {
            PressOrReleaseButton(button, ButtonCommand.Press);
        }

        public void Release(ButtonType button)
        {
            PressOrReleaseButton(button, ButtonCommand.Release);
        }

        public void Push(DPadCommand dPad)
        {
            Push(dPad, DefaultPushDuration);
        }

        public void Push(DPadCommand dPad, TimeSpan duration)
        {
            Press(dPad);
            Thread.Sleep(duration);
            ReleaseDPad();
        }

        public void Press(DPadCommand dPad)
        {
            MoveHat(dPad);
        }

        public void ReleaseDPad()
        {
            MoveHat(DPadCommand.Center);
        }


        private TimeSpan DefaultPushDuration { get; } = TimeSpan.FromMilliseconds(Properties.Settings.Default.PushDuration);

        protected abstract void PressOrReleaseButton(ButtonType button, ButtonCommand command);
        protected abstract void MoveHat(DPadCommand command);
    }
}
