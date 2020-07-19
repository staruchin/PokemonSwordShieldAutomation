using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.SwitchController
{
    public class SerialPortService : IDisposable
    {
        private SerialPort m_serialPort = new SerialPort();

        public SerialPortService()
        {
            m_serialPort.BaudRate = 115200;
            m_serialPort.Parity = Parity.None;
            m_serialPort.DataBits = 8;
            m_serialPort.StopBits = StopBits.One;
        }

        public static string[] GetPortNames() => SerialPort.GetPortNames();

        public void SetPortName(string portName)
        {
            m_serialPort.PortName = portName;
        }

        public bool IsOpen => m_serialPort.IsOpen;
        public void Open() => m_serialPort.Open();
        public void Close() => m_serialPort.Close();
        public void Write(byte[] buffer) => m_serialPort.Write(buffer, 0, buffer.Length);

        public void Dispose()
        {
            if (m_serialPort != null)
            {
                if (m_serialPort.IsOpen)
                {
                    m_serialPort.Close();
                }

                m_serialPort.Dispose();
                m_serialPort = null;
            }
        }
    }
}
