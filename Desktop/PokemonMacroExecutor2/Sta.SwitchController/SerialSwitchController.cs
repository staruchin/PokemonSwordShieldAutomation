using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.IO;
using System.Reactive.Disposables;
using System.Threading;

namespace Sta.SwitchController
{
    public class SerialSwitchController : AbstractSwitchController, IDisposable
    {
        private CompositeDisposable Disposables { get; } = new CompositeDisposable();

        private SerialPortService m_serialPort = new SerialPortService();

        public ReactiveProperty<bool> IsConnected { get; }

        public SerialSwitchController()
        {
            IsConnected = m_serialPort.IsOpen.ObserveProperty(o => o.Value).ToReactiveProperty().AddTo(Disposables);
        }

        public void Connect(string portName)
        {
            if (m_serialPort.IsOpen.Value)
            {
                m_serialPort.Close();
            }
            m_serialPort.PortName = portName;
            m_serialPort.Open();
        }

        protected override void PressOrReleaseButton(ButtonType button, ButtonCommand command)
        {
            WriteSerialPort(new[] { (byte)ControlType.Button, (byte)button, (byte)command, });
        }

        protected override void MoveHat(DPadCommand command)
        {
            WriteSerialPort(new[] { (byte)ControlType.DPad, (byte)command, });
        }

        private void WriteSerialPort(byte[] buffer)
        {
            m_serialPort.Write(buffer);
        }

        public void Dispose()
        {
            Disposables.Dispose();
            m_serialPort.Dispose();
        }
    }
}
