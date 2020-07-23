using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.AutomationMacro
{
    public interface IGameDateManager : INotifyPropertyChanged
    {
        DateTime? GameDate { get; set; }
        bool IsEndOfDays { get; }
    }
}
