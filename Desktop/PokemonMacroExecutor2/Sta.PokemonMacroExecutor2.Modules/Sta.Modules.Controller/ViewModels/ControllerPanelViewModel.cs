﻿using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Sta.SwitchController;
using Sta.Utilities;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Sta.Modules.Controller.ViewModels
{
    public class ControllerPanelViewModel : BindableBase, IDisposable
    {
        private CompositeDisposable Disposables { get; } = new CompositeDisposable();

        public ReactiveProperty<bool> IsConnected { get; }
        public ReactiveProperty<bool> IsBusy { get; }

        public ReactiveCommand<ButtonType?> PushButtonCommand { get; }
        public ReactiveCommand<DPadCommand?> PushDPadCommand { get; }
        public ReactiveCommand<StickParameter> MoveStickCommand { get; }

        private ISwitchController m_switchController = null;

        public ControllerPanelViewModel(ISwitchController controller, ISerialPortService serialPort, IWorkSituation work)
        {
            m_switchController = controller;

            IsConnected = serialPort.ObserveProperty(p => p.IsOpen).ToReactiveProperty().AddTo(Disposables);
            IsBusy = work.ObserveProperty(w => w.IsBusy).ToReactiveProperty().AddTo(Disposables);

            PushButtonCommand = new[] { IsConnected, IsBusy }.CombineLatest(x => x[0] && !x[1]).ToReactiveCommand<ButtonType?>().AddTo(Disposables);
            PushButtonCommand.Subscribe(PushButton).AddTo(Disposables);

            PushDPadCommand = new[] { IsConnected, IsBusy }.CombineLatest(x => x[0] && !x[1]).ToReactiveCommand<DPadCommand?>().AddTo(Disposables);
            PushDPadCommand.Subscribe(PushDPad).AddTo(Disposables);

            MoveStickCommand = new[] { IsConnected, IsBusy }.CombineLatest(x => x[0] && !x[1]).ToReactiveCommand<StickParameter>().AddTo(Disposables);
            MoveStickCommand.Subscribe(MoveStick).AddTo(Disposables);
        }

        private void PushButton(ButtonType? button)
        {
            Task.Run(() =>
            {
                m_switchController.PressAndRelease(button.Value, Properties.Settings.Default.PressAndReleaseButtonDuration);
            });
        }

        private void PushDPad(DPadCommand? dPad)
        {
            Task.Run(() =>
            {
                m_switchController.PressAndRelease(dPad.Value, Properties.Settings.Default.PressAndReleaseDPadDuration);
            });
        }

        private void MoveStick(StickParameter stick)
        {
            Task.Run(() =>
            {
                m_switchController.MoveAndRelease(stick.StickType, stick.X, stick.Y, Properties.Settings.Default.MoveAndReleaseStickDuration);
            });
        }

        public void Dispose()
        {
            Disposables.Dispose();
        }
    }
}
