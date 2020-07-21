using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Sta.SwitchController;
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
        public ReactiveProperty<string> ConnectionStatus { get; }

        private SerialSwitchController m_controller = null;

        public SerialPortSelectorViewModel(SerialSwitchController controller)
        {
            m_controller = controller;

            PortNames = new ObservableCollection<string>(new[] { "COM1", "COM2", "COM3", "COM4" }); // (SerialPortService.GetPortNames());
            SelectedPortName = new ReactiveProperty<string>().AddTo(Disposables);
            SelectedPortNameIndex = new ReactiveProperty<int>().AddTo(Disposables);
            ConnectionStatus = m_controller.IsConnected.ObserveProperty(p => p.Value).Select(o => ToConnectionStatus(o)).ToReactiveProperty().AddTo(Disposables);

            SelectedPortNameIndex.Value = (PortNames.Count == 1) ? 0 : -1;

            SelectedPortName.Subscribe(n => Connect(n)).AddTo(Disposables);
        }

        private string ToConnectionStatus(bool open)
        {
            return open ? Properties.Resources.ConnectionStatusConnected
                        : Properties.Resources.ConnectionStatusNotConnected;
        }

        private void Connect(string portName)
        {
            if (string.IsNullOrEmpty(portName)) { return; }

            try
            {
                m_controller.Connect(portName);
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
