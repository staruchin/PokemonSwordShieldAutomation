﻿using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Sta.AutomationMacro;
using Sta.AutomationMacro.Macro;
using Sta.CaptureBoard;
using Sta.SwitchController;
using Sta.Utilities;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

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
        public ReactiveCommand GainWattsCommand { get; }
        public ReactiveCommand BattleMaxRaidCommand { get; }
        public ReactiveCommand CancelCommand { get; }
        public ReactiveCommand SaveImageCommand { get; }

        private IMacroService m_macro = null;
        private ICanceler m_macroCanceler = null;

        public MacroPanelViewModel(IMacroService macro, ICanceler canceler, IWorkSituation work, ISwitchClock clock, ISerialPortService serialPort, ICancellationRequest cancelRequest, IGameCapture gameCapture)
        {
            m_macro = macro;
            m_macroCanceler = canceler;

            IsConnected = serialPort.ObserveProperty(p => p.IsOpen).ToReactiveProperty().AddTo(Disposables);
            IsBusy = work.ObserveProperty(w => w.IsBusy).ToReactiveProperty().AddTo(Disposables);

            Clock = clock.ToReactivePropertyAsSynchronized(m => m.DateTime).AddTo(Disposables);
            Clock.Value = DateTime.Now;

            DrawLotoIdCommand = CreateExecuteMacroCommand<DrawLotoIdMacro>();
            GainWattsCommand = CreateExecuteMacroCommand<GainWattsMacro>();
            BattleMaxRaidCommand = CreateExecuteMacroCommand<BattleMaxRaidMacro>();

            IsCanceling = cancelRequest.ObserveProperty(c => c.IsCancellationRequested).ToReactiveProperty().AddTo(Disposables);
            CancelCommand = new[] { IsBusy, IsCanceling }.CombineLatest(x => x[0] && !x[1]).ToReactiveCommand().AddTo(Disposables);
            CancelCommand.Subscribe(m_macroCanceler.Cancel);

            SaveImageCommand = new ReactiveCommand().AddTo(Disposables);
            SaveImageCommand.Subscribe(() => gameCapture.SaveFrame(null));
        }

        private ReactiveCommand CreateExecuteMacroCommand<T>() where T : IMacro
        {
            var command = CreateExecuteMacroCommand().AddTo(Disposables);
            command.Subscribe(m_macro.Execute<T>).AddTo(Disposables);
            return command;
        }

        private ReactiveCommand CreateExecuteMacroCommand()
        {
            return new[] { IsConnected, IsBusy }.CombineLatest(x => x[0] && !x[1]).ToReactiveCommand();
        }

        public void Dispose()
        {
            Disposables.Dispose();
        }
    }
}
