using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.AutomationMacro
{
    public interface ICancellationRequest
    {
        bool IsCancellationRequested { get; }
        void ThrowIfCancellationRequested();
    }
}
