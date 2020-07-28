namespace Sta.SwitchController
{
    public class SerialSwitchController : ISwitchController
    {
        public ISerialPortService SerialPort { get; set; }

        public void PressButton(ButtonType button)
        {
            PressOrReleaseButton(button, ButtonCommand.Press);
        }

        public void ReleaseButton(ButtonType button)
        {
            PressOrReleaseButton(button, ButtonCommand.Release);
        }

        private void PressOrReleaseButton(ButtonType button, ButtonCommand command)
        {
            WriteSerialPort(new[] { (byte)ControlType.Button, (byte)button, (byte)command, });
        }

        public void PressDPad(DPadCommand dPad)
        {
            WriteSerialPort(new[] { (byte)ControlType.DPad, (byte)dPad, });
        }

        public void ReleaseDPad()
        {
            PressDPad(DPadCommand.Center);
        }

        public void MoveStick(StickType stick, byte x, byte y)
        {
            WriteSerialPort(new[] { (byte)ControlType.Stick, (byte)stick, x, y, });
        }

        public void ReleaseStick(StickType stick)
        {
            const byte Center = 128;
            MoveStick(stick, Center, Center);
        }

        private void WriteSerialPort(byte[] buffer)
        {
            SerialPort.Write(buffer);
        }
    }
}
