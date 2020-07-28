using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.Utilities
{
    public interface IWorkSituation : INotifyPropertyChanged
    {
        bool IsBusy { get; set; }
    }
}
