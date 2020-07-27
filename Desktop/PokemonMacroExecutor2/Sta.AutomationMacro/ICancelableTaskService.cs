using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sta.AutomationMacro
{
    public interface ICancelableTaskService : ICancellationRequest, INotifyPropertyChanged
    {
        void Cancel();

        Task Run(Action action);
        Task<TResult> Run<TResult>(Func<TResult> function);
    }
}
