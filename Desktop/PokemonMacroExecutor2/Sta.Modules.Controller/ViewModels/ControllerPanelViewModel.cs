using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Sta.AutomationMacro;
using Sta.SwitchController;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Sta.Modules.Controller.ViewModels
{
    public class ControllerPanelViewModel : BindableBase, IDisposable
    {
        private CompositeDisposable Disposables { get; } = new CompositeDisposable();

        public ReactiveProperty<bool> IsConnected { get; }
        public ReactiveProperty<bool> IsBusy { get; }

        public ReactiveCommand<ButtonType?> PushButtonCommand { get; }

        private ISwitchController m_switchController = null;

        public ControllerPanelViewModel(ISwitchController controller, ISerialPortService serialPort, IMacroService macro)
        {
            m_switchController = controller;

            IsConnected = serialPort.ObserveProperty(p => p.IsOpen).ToReactiveProperty().AddTo(Disposables);
            IsBusy = macro.ObserveProperty(m => m.IsBusy).ToReactiveProperty().AddTo(Disposables);

            PushButtonCommand = new[] { IsConnected, IsBusy }.CombineLatest(x => x[0] && !x[1]).ToReactiveCommand<ButtonType?>().AddTo(Disposables);
            PushButtonCommand.Subscribe(PushButton).AddTo(Disposables);
        }

        private void PushButton(ButtonType? button)
        {
            if (!button.HasValue)
            {
                return;
            }

            m_switchController.PressAndRelease(button.Value, Properties.Settings.Default.PressAndReleaseDuration);
        }

        public void Dispose()
        {
            Disposables.Dispose();
        }
    }
}
