using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sta.SwitchControllerLib
{
    public class SwitchController
    {
        private SerialPort m_serialPort = new SerialPort();


        public void Connect(string portName)
        {
            m_serialPort.BaudRate = 115200;
            m_serialPort.Parity = Parity.None;
            m_serialPort.DataBits = 8;
            m_serialPort.StopBits = StopBits.One;
            m_serialPort.PortName = portName;
            m_serialPort.Open();
        }

        public void Disconnect()
        {
            if (m_serialPort.IsOpen)
            {
                m_serialPort.Close();
            }
        }

        public void PushA(int delay1, int delay2)
        {
            Push(ButtonType.A, delay1, delay2);
        }

        public void PushB(int delay1, int delay2)
        {
            Push(ButtonType.B, delay1, delay2);
        }

        public void PushX(int delay1, int delay2)
        {
            Push(ButtonType.X, delay1, delay2);
        }

        public void PushY(int delay1, int delay2)
        {
            Push(ButtonType.Y, delay1, delay2);
        }

        public void PushR(int delay1, int delay2)
        {
            Push(ButtonType.R, delay1, delay2);
        }

        public void PushPlus(int delay1, int delay2)
        {
            Push(ButtonType.Plus, delay1, delay2);
        }

        public void PushHome(int delay1, int delay2)
        {
            Push(ButtonType.Home, delay1, delay2);
        }

        public void PushCapture(int delay1, int delay2)
        {
            Push(ButtonType.Capture, delay1, delay2);
        }

        public void PushUp(int delay1, int delay2)
        {
            Push(DPadCommand.Up, delay1, delay2);
        }

        public void PushRight(int delay1, int delay2)
        {
            Push(DPadCommand.Right, delay1, delay2);
        }

        public void PushDown(int delay1, int delay2)
        {
            Push(DPadCommand.Down, delay1, delay2);
        }

        public void PushLeft(int delay1, int delay2)
        {
            Push(DPadCommand.Left, delay1, delay2);
        }


        private void Push(ButtonType button, int delay1, int delay2)
        {
            PressButton(button);
            Thread.Sleep(delay1);
            ReleaseButton(button);
            Thread.Sleep(delay2);
        }

        private void Push(DPadCommand dPadCommand, int delay1, int delay2)
        {
            MoveHat(dPadCommand);
            Thread.Sleep(delay1);
            MoveHat(DPadCommand.Center);
            Thread.Sleep(delay2);
        }

        private void PressButton(ButtonType button)
        {
            PressOrReleaseButton(button, ButtonCommand.Press);
        }

        private void ReleaseButton(ButtonType button)
        {
            PressOrReleaseButton(button, ButtonCommand.Release);
        }

        private void PressOrReleaseButton(ButtonType button, ButtonCommand command)
        {
            WriteSerialPort(new[] { (byte)ControlType.Button, (byte)button, (byte)command, });
        }

        private void MoveHat(DPadCommand command)
        {
            WriteSerialPort(new[] { (byte)ControlType.DPad, (byte)command, });
        }

        private void WriteSerialPort(byte[] data)
        {
            if (m_serialPort.IsOpen)
            {
                m_serialPort.Write(data, 0, data.Length);
            }
        }
    }
}
