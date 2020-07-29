using Sta.SwitchController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.AutomationMacro.Macro
{
    public class DrawLotoIdMacro : AbstractMacro
    {
        private ISwitchClock Clock => Param.Clock;
        private ISwitchController Controller => Param.Controller;
        private ICancellationRequest CancellationRequest => Param.CancellationRequest;

        public override void Execute()
        {

            while (!Clock.IsEndOfDays && !CancellationRequest.IsCancellationRequested)
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
        }
    }
}
