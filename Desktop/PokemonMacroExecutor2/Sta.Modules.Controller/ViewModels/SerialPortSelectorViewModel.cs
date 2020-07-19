using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Sta.SwitchController;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive.Disposables;

namespace Sta.Modules.Controller.ViewModels
{
    public class SerialPortSelectorViewModel : BindableBase, IDisposable
    {
        private CompositeDisposable Disposables { get; } = new CompositeDisposable();

        public ObservableCollection<string> PortNames { get; }
        public ReactiveProperty<string> SelectedPortName { get; }
        public ReactiveProperty<int> SelectedPortNameIndex { get; }
        public ReactiveProperty<string> ConnectionStatus { get; }

        private SerialPortService m_serialPort = null;

        public SerialPortSelectorViewModel(SerialPortService serialPort)
        {
            m_serialPort = serialPort;

            PortNames = new ObservableCollection<string>(SerialPortService.GetPortNames());
            SelectedPortName = new ReactiveProperty<string>().AddTo(Disposables);
            SelectedPortNameIndex = new ReactiveProperty<int>().AddTo(Disposables);
            ConnectionStatus = new ReactiveProperty<string>().AddTo(Disposables);

            SelectedPortNameIndex.Value = (PortNames.Count == 1) ? 0 : -1;

            SelectedPortName.Subscribe(n => Connect(n)).AddTo(Disposables);
        }

        private void Connect(string portName)
        {
            try
            {
                if (m_serialPort.IsOpen)
                {
                    m_serialPort.Close();
                }

                m_serialPort.SetPortName(portName);
                m_serialPort.Open();

                ConnectionStatus.Value = Properties.Resources.ConnectionStatusConnected;
            }
            catch (Exception)
            {
                ConnectionStatus.Value = Properties.Resources.ConnectionStatusNotConnected;
            }
        }

        public void Dispose()
        {
            Disposables.Dispose();
        }
    }
}
