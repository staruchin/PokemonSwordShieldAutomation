using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Sta.SwitchController;
using Sta.Utilities;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Sta.Modules.Controller.ViewModels
{
    public class SerialPortSelectorViewModel : BindableBase, IDisposable
    {
        private CompositeDisposable Disposables { get; } = new CompositeDisposable();

        public ObservableCollection<string> PortNames { get; }
        public ReactiveProperty<string> SelectedPortName { get; }
        public ReactiveProperty<int> SelectedPortNameIndex { get; }
        public ReactiveProperty<bool> IsConnected { get; }
        public ReactiveProperty<bool> IsBusy { get; }

        private ISerialPortService m_serialPort = null;

        public SerialPortSelectorViewModel(ISerialPortService serialPort, IWorkSituation work)
        {
            m_serialPort = serialPort;

            //PortNames = new ObservableCollection<string>(SerialPortService.GetPortNames());
            PortNames = new ObservableCollection<string>(new[] { "COM1", "COM2", "COM3", "COM4" });

            SelectedPortName = new ReactiveProperty<string>().AddTo(Disposables);
            SelectedPortName.Subscribe(n => Connect(n)).AddTo(Disposables);

            SelectedPortNameIndex = new ReactiveProperty<int>().AddTo(Disposables);
            SelectedPortNameIndex.Value = (PortNames.Count == 1) ? 0 : -1;

            IsConnected = m_serialPort.ObserveProperty(p => p.IsOpen).ToReactiveProperty().AddTo(Disposables);
            IsBusy = work.ObserveProperty(w => w.IsBusy).ToReactiveProperty().AddTo(Disposables);
        }

        private void Connect(string portName)
        {
            if (string.IsNullOrEmpty(portName)) { return; }

            try
            {
                if (m_serialPort.IsOpen)
                {
                    m_serialPort.Close();
                }
                m_serialPort.PortName = portName;
                m_serialPort.Open();
            }
            catch (IOException)
            {
            }
        }

        public void Dispose()
        {
            Disposables.Dispose();
        }
    }
}
