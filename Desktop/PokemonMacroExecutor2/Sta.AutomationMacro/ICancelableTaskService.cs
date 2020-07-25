using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sta.AutomationMacro
{
    public interface ICancelableTaskService : INotifyPropertyChanged
    {
        void Cancel();
        bool IsCancellationRequested { get; }
        void ThrowIfCancellationRequested();

        Task Run(Action action);
        Task<TResult> Run<TResult>(Func<TResult> function);
    }
}
