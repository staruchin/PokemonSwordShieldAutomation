using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Sta.SwitchController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Sta.Modules.MacroExecutor.ViewModels
{
    public class MacroSelectorViewModel : BindableBase, IDisposable
    {
        private CompositeDisposable Disposables { get; } = new CompositeDisposable();

        public ReactiveProperty<bool> IsControllerConnected { get; }
        public ReactiveProperty<bool> IsBusy { get; }

        public ReactiveProperty<DateTime?> GameDate { get; set; }
        public ReactiveCommand DrawLotoIdCommand { get; }

        private SerialSwitchController m_controller = null;

        public MacroSelectorViewModel(SerialSwitchController controller)
        {
            m_controller = controller;

            IsControllerConnected = m_controller.IsConnected.ObserveProperty(p => p.Value).ToReactiveProperty().AddTo(Disposables);
            IsBusy = new ReactiveProperty<bool>().AddTo(Disposables);

            GameDate = new ReactiveProperty<DateTime?>(DateTime.Now).AddTo(Disposables);

            DrawLotoIdCommand = CreateReactiveCommand();
            DrawLotoIdCommand.Subscribe(DrawLotoId);

        }

        private ReactiveCommand CreateReactiveCommand()
        {
            return IsControllerConnected.CombineLatest(IsBusy, (connected, busy) => connected && !busy).ToReactiveCommand().AddTo(Disposables);
        }

        private async void DrawLotoId()
        {
            try
            {
                IsBusy.Value = true;
                await Task.Delay(3000);
            }
            finally
            {
                IsBusy.Value = false;
            }
        }

        public void Dispose()
        {
            Disposables.Dispose();
        }
    }
}
