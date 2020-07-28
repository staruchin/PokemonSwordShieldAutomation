using Sta.SwitchController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.AutomationMacro
{
    public interface IMacroService : INotifyPropertyChanged
    {
        void DrawLotoId();
    }
}
