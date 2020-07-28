using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.AutomationMacro
{
    public interface ICancellationRequest : INotifyPropertyChanged
    {
        bool IsCancellationRequested { get; }
        void ThrowIfCancellationRequested();
    }
}
