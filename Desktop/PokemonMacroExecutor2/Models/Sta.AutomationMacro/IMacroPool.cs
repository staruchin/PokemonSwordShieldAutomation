using Sta.AutomationMacro.Macro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.AutomationMacro
{
    public interface IMacroPool
    {
        IMacro Get<T>() where T : IMacro;
    }
}
