using Prism.Mvvm;
using Sta.SwitchController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.AutomationMacro
{
    public class MacroService : BindableBase, IMacroService
    {
        public ICancelableTaskService CancelableTask { get; set; }

        public ISwitchController Controller { get; set; }
        public ISwitchClock Clock { get; set; }

        private bool m_isBusy = false;
        /// <inheritdoc/>
        public bool IsBusy
        {
            get { return m_isBusy; }
            set { SetProperty(ref m_isBusy, value); }
        }


        /// <inheritdoc/>
        public void DrawLotoId()
        {
            ExecuteMacro(() =>
            {
                while (!Clock.IsEndOfDays && !CancelableTask.IsCancellationRequested)
                {
                    Clock.IncreaseOneDayFromGameScreen();

                    Controller.PressAndRelease(ButtonType.A, 50, 500);
                    Controller.PressAndRelease(ButtonType.B, 50, 500);
                    Controller.PressAndRelease(DPadCommand.Down, 50, 200);
                    Controller.PressAndRelease(ButtonType.A, 50, 800);

                    Controller.PressAndRelease(ButtonType.B, 50, 500);
                    Controller.PressAndRelease(ButtonType.B, 50, 500);
                    Controller.PressAndRelease(ButtonType.B, 50, 800);
                    Controller.PressAndRelease(ButtonType.A, 50, 100); // レポート「はい」

                    for (int i = 0; i < 80; i++)
                    {
                        Controller.PressAndRelease(ButtonType.B, 50, 100);
                    }
                }
            });
        }

        private async void ExecuteMacro(Action action)
        {
            if (IsBusy) { return; }

            try
            {
                IsBusy = true;
                await CancelableTask.Run(action);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
