using Sta.SwitchController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.AutomationMacro.Macro
{
    public class BattleMaxRaidMacro : AbstractMacro
    {
        public override void Execute()
        {
            while (true)
            {
                Controller.PressAndRelease(ButtonType.A, 50, 3000);
                Controller.PressAndRelease(DPadCommand.Down, 50, 1500);
                Controller.PressAndRelease(ButtonType.A, 50, 1500);
                Controller.PressAndRelease(ButtonType.A, 50, 1500);
                Controller.PressAndRelease(ButtonType.A, 50, 1500);
                Controller.PressAndRelease(ButtonType.A, 50, 3000);
                Controller.PressAndRelease(ButtonType.B, 50, 3000);
                Controller.PressAndRelease(ButtonType.B, 50, 1500);
                Controller.PressAndRelease(DPadCommand.Down, 50, 3000);

                CancellationRequest.ThrowIfCancellationRequested();
            }
        }
    }
}
