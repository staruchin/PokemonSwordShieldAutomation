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
        public ISwitchClock Clock { get; set; }
        public ISwitchController Controller { get; set; }
        public ICancellationRequest CancellationRequest { get; set; }

        public abstract void Execute();
    }
}
