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
        public ReactiveProperty<bool> IsCanceling { get; set; }

        public ReactiveProperty<DateTime?> GameDate { get; set; }
        public ReactiveCommand DrawLotoIdCommand { get; }
        public ReactiveCommand CancelCommand { get; }

        private IMacroService m_macro = null;
        private ICancelableTaskService m_cancelableTask = null;

        public MacroPanelViewModel(IMacroService macro, IGameDateManager gameDate, ISerialPortService serialPort, ICancelableTaskService cancelableTask)
        {
            m_macro = macro;
            m_cancelableTask = cancelableTask;

            IsConnected = serialPort.ObserveProperty(p => p.IsOpen).ToReactiveProperty().AddTo(Disposables);
            IsBusy = m_macro.ObserveProperty(m => m.IsBusy).ToReactiveProperty().AddTo(Disposables);

            GameDate = gameDate.ToReactivePropertyAsSynchronized(m => m.GameDate).AddTo(Disposables);
            GameDate.Value = DateTime.Now;

            DrawLotoIdCommand = CreateExecuteMacroCommand().AddTo(Disposables);
            DrawLotoIdCommand.Subscribe(DrawLotoId).AddTo(Disposables);

            IsCanceling = m_cancelableTask.ObserveProperty(c => c.IsCancellationRequested).ToReactiveProperty().AddTo(Disposables);
            CancelCommand = new[] { IsBusy, IsCanceling }.CombineLatest(x => x[0] && !x[1]).ToReactiveCommand().AddTo(Disposables);
            CancelCommand.Subscribe(Cancel);
        }

        private ReactiveCommand CreateExecuteMacroCommand()
        {
            return new[] { IsConnected, IsBusy }.CombineLatest(x => x[0] && !x[1]).ToReactiveCommand();
        }

        private void DrawLotoId()
        {
            m_macro.DrawLotoId();
        }

        private void Cancel()
        {
            m_cancelableTask.Cancel();
        }

        public void Dispose()
        {
            Disposables.Dispose();
        }
    }
}
