using System;
using System.Threading;

namespace Sta.SwitchController
{
    public class SerialSwitchController
    {
        public SerialPortService SerialPort { get; set; }

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

        private void PressOrReleaseButton(ButtonType button, ButtonCommand command)
        {
            WriteSerialPort(new[] { (byte)ControlType.Button, (byte)button, (byte)command, });
        }

        private void MoveHat(DPadCommand command)
        {
            WriteSerialPort(new[] { (byte)ControlType.DPad, (byte)command, });
        }

        private void WriteSerialPort(byte[] buffer)
        {
            SerialPort.Write(buffer);
        }
    }
}
