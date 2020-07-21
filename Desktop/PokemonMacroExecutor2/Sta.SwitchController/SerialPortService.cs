using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.IO.Ports;

namespace Sta.SwitchController
{
    public class SerialPortService : IDisposable
    {
        private SerialPort m_serialPort = new SerialPort();

        public ReactiveProperty<bool> IsOpen { get; } = new ReactiveProperty<bool>();

        public string PortName
        {
            get { return m_serialPort.PortName; }
            set { m_serialPort.PortName = value; }
        }

        public SerialPortService()
        {
            m_serialPort.BaudRate = 115200;
            m_serialPort.Parity = Parity.None;
            m_serialPort.DataBits = 8;
            m_serialPort.StopBits = StopBits.One;
        }

        public static string[] GetPortNames() => SerialPort.GetPortNames();

        public void Open()
        {
            try
            {
                m_serialPort.Open();
            }
            finally
            {
                IsOpen.Value = m_serialPort.IsOpen;
            }
        }

        public void Close()
        {
            try
            {
                m_serialPort.Close();
            }
            finally
            {
                IsOpen.Value = m_serialPort.IsOpen;
            }
        }

        public void Write(byte[] buffer) => m_serialPort.Write(buffer, 0, buffer.Length);

        public void Dispose()
        {
            if (m_serialPort != null)
            {
                m_serialPort.Dispose();
                m_serialPort = null;
            }
        }
    }
}
