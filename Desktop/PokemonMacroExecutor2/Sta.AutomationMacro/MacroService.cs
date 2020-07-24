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
        /// <inheritdoc/>
        public ISwitchController Controller { get; set; }
        /// <inheritdoc/>
        public IGameDateManager GameDateManager { get; set; }

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
                while (!GameDateManager.IsEndOfDays)
                {
                    //GameDateManager.IncreaseOneDay();

                    Controller.PressAndReleaseButton(ButtonType.A, 50, 500);
                    Controller.PressAndReleaseButton(ButtonType.B, 50, 500);
                    Controller.PressAndReleaseDPad(DPadCommand.Down, 50, 200);
                    Controller.PressAndReleaseButton(ButtonType.A, 50, 800);

                    Controller.PressAndReleaseButton(ButtonType.B, 50, 500);
                    Controller.PressAndReleaseButton(ButtonType.B, 50, 500);
                    Controller.PressAndReleaseButton(ButtonType.B, 50, 800);
                    Controller.PressAndReleaseButton(ButtonType.A, 50, 100); // レポート「はい」

                    for (int i = 0; i < 80; i++)
                    {
                        Controller.PressAndReleaseButton(ButtonType.B, 50, 100);
                    }

                    GameDateManager.GameDate += OneDay;
                }
            });
        }

        private void ExecuteMacro(Action action)
        {
            try
            {
                IsBusy = true;
                action?.Invoke();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private static TimeSpan OneDay { get; } = TimeSpan.FromDays(1);
    }
}
