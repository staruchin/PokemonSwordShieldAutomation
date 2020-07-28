using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.Utilities
{
    public class WorkSituation : BindableBase, IWorkSituation
    {
        private bool m_isBusy = false;
        public bool IsBusy
        {
            get { return m_isBusy; }
            set { SetProperty(ref m_isBusy, value); }
        }
    }
}
