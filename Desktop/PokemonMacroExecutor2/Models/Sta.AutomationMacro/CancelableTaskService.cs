using Prism.Mvvm;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sta.AutomationMacro
{
    public class CancelableTaskService : BindableBase, ICancelableTaskService
    {
        private AsyncLock m_lock = new AsyncLock();
        private CancellationTokenSource m_cts = null;

        private bool m_cancelRequested = false;
        public bool IsCancellationRequested
        {
            get { return m_cancelRequested; }
            set { SetProperty(ref m_cancelRequested, value); }
        }

        public void Cancel()
        {
            try
            {
                m_cts.Cancel();
                IsCancellationRequested = m_cts.IsCancellationRequested;
            }
            catch (Exception ex) when (ex is NullReferenceException || ex is ObjectDisposedException)
            {
            }
        }

        public void ThrowIfCancellationRequested() => m_cts?.Token.ThrowIfCancellationRequested();

        public async Task Run(Action action)
        {
            using (await m_lock.LockAsync())
            {
                m_cts = new CancellationTokenSource();

                try
                {
                    await Task.Run(action, m_cts.Token);
                }
                catch (OperationCanceledException)
                {

                }
                finally
                {
                    m_cts.Dispose();
                    m_cts = null;
                    IsCancellationRequested = false;
                }
            }
        }

        public async Task<TResult> Run<TResult>(Func<TResult> function)
        {
            using (await m_lock.LockAsync())
            {
                m_cts = new CancellationTokenSource();

                try
                {
                    return await Task.Run(function, m_cts.Token);
                }
                catch (OperationCanceledException)
                {
                    return default;
                }
                finally
                {
                    m_cts.Dispose();
                    m_cts = null;
                    IsCancellationRequested = false;
                }
            }
        }
    }
}
