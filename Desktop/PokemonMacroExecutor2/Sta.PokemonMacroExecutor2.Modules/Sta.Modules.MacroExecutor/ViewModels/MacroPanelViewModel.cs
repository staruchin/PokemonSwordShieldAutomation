using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Sta.AutomationMacro;
using Sta.SwitchController;
using Sta.Utilities;
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

        public ReactiveProperty<DateTime?> Clock { get; set; }
        public ReactiveCommand DrawLotoIdCommand { get; }
        public ReactiveCommand CancelCommand { get; }

        private IMacroService m_macro = null;
        private ICancelableTaskService m_cancelableTask = null;

        public MacroPanelViewModel(IMacroService macro, IWorkSituation work, ISwitchClock clock, ISerialPortService serialPort, ICancelableTaskService cancelableTask)
        {
            m_macro = macro;
            m_cancelableTask = cancelableTask;

            IsConnected = serialPort.ObserveProperty(p => p.IsOpen).ToReactiveProperty().AddTo(Disposables);
            IsBusy = work.ObserveProperty(w => w.IsBusy).ToReactiveProperty().AddTo(Disposables);

            Clock = clock.ToReactivePropertyAsSynchronized(m => m.DateTime).AddTo(Disposables);
            Clock.Value = DateTime.Now;

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
