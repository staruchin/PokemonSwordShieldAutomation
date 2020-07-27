using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.AutomationMacro
{
    public interface ISwitchClock : INotifyPropertyChanged
    {
        DateTime? DateTime { get; set; }
        bool IsEndOfDays { get; }

        void IncreaseOneDayFromGameScreen();
    }
}
