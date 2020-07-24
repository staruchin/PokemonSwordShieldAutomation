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
        ISwitchController Controller { get; set; }

        IGameDateManager GameDateManager { get; set; }

        bool IsBusy { get; }

        void DrawLotoId();
    }
}
