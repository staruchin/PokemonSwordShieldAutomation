using Sta.AutomationMacro.Macro;
using Sta.SwitchController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.AutomationMacro
{
    public interface IMacroService
    {
        void DrawLotoId();
        void BattleMaxRaid();

        void Execute<T>() where T : IMacro;
    }
}
