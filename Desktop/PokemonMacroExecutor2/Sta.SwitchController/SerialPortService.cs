using Prism.Mvvm;
using System;
using System.IO.Ports;

namespace Sta.SwitchController
{
    public class SerialPortService : BindableBase, ISerialPortService, IDisposable
    {
        private SerialPort m_serialPort = new SerialPort();

        /// <inheritdoc/>
        public string PortName
        {
            get { return m_serialPort.PortName; }
            set { m_serialPort.PortName = value; }
        }

        private bool m_isOpen = false;
        /// <inheritdoc/>
        public bool IsOpen
        {
            get { return m_isOpen; }
            private set { SetProperty(ref m_isOpen, value); }
        }

        public SerialPortService()
        {
            m_serialPort.BaudRate = 115200;
            m_serialPort.Parity = Parity.None;
            m_serialPort.DataBits = 8;
            m_serialPort.StopBits = StopBits.One;
        }

        /// <summary>
        /// 現在のコンピューターのシリアルポート名の配列を取得します。
        /// </summary>
        public static string[] GetPortNames() => SerialPort.GetPortNames();

        /// <inheritdoc/>
        public void Open()
        {
            try
            {
                m_serialPort.Open();
            }
            finally
            {
                IsOpen = m_serialPort.IsOpen;
            }
        }

        /// <inheritdoc/>
        public void Close()
        {
            try
            {
                m_serialPort.Close();
            }
            finally
            {
                IsOpen = m_serialPort.IsOpen;
            }
        }

        /// <inheritdoc/>
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
