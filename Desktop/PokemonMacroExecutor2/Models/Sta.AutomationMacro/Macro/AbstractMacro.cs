using Sta.CaptureBoard;
using Sta.SwitchController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.AutomationMacro.Macro
{
    public abstract class AbstractMacro : IMacro
    {
        public MacroParameter Param { get; set; }

        public abstract void Execute();
    }
}
