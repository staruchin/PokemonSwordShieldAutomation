using Prism.Mvvm;
using Sta.SwitchController;
using Sta.Utilities;
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
        public IWorkSituation Work { get; set; }

        private bool IsBusy
        {
            get { return Work.IsBusy; }
            set { Work.IsBusy = value; }
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
