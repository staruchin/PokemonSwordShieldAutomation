using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Sta.AutomationMacro;
using Sta.SwitchController;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Sta.Modules.MacroExecutor.ViewModels
{
    public class MacroPanelViewModel : BindableBase, IDisposable
    {
        private CompositeDisposable Disposables { get; } = new CompositeDisposable();

        public ReactiveProperty<bool> IsConnected { get; }
        public ReactiveProperty<bool> IsBusy { get; }

        public ReactiveProperty<DateTime?> GameDate { get; set; }
        public ReactiveCommand DrawLotoIdCommand { get; }

        private IMacroService m_macro = null;

        public MacroPanelViewModel(IMacroService macro, ISerialPortService serialPort)
        {
            m_macro = macro;

            IsConnected = serialPort.ObserveProperty(p => p.IsOpen).ToReactiveProperty().AddTo(Disposables);
            IsBusy = m_macro.ObserveProperty(m => m.IsBusy).ToReactiveProperty().AddTo(Disposables);

            GameDate = m_macro.GameDateManager.ToReactivePropertyAsSynchronized(m => m.GameDate).AddTo(Disposables);
            GameDate.Value = DateTime.Now;

            DrawLotoIdCommand = CreateReactiveCommand().AddTo(Disposables);
            DrawLotoIdCommand.Subscribe(DrawLotoId).AddTo(Disposables);
        }

        private ReactiveCommand CreateReactiveCommand()
        {
            return IsConnected.CombineLatest(IsBusy, (connected, busy) => connected && !busy).ToReactiveCommand();
        }

        private async void DrawLotoId()
        {
            await Task.Run(() => m_macro.DrawLotoId());
        }

        public void Dispose()
        {
            Disposables.Dispose();
        }
    }
}
