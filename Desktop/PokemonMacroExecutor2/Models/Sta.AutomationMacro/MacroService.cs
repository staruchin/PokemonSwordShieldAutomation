using Prism.Mvvm;
using Sta.AutomationMacro.Macro;
using Sta.SwitchController;
using Sta.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.AutomationMacro
{
    public class MacroService : IMacroService
    {
        public IMacroPool MacroPool { get; set; }
        public ITaskService TaskService { get; set; }
        public IWorkSituation Work { get; set; }


        /// <inheritdoc/>
        public void DrawLotoId()
        {
            ExecuteInternal<DrawLotoIdMacro>();
        }

        public void BattleMaxRaid()
        {
            ExecuteInternal<BattleMaxRaidMacro>();
        }

        public void Execute<T>() where T : IMacro
        {
            ExecuteInternal<T>();
        }

        private bool IsBusy
        {
            get { return Work.IsBusy; }
            set { Work.IsBusy = value; }
        }

        private async void ExecuteInternal<T>() where T : IMacro
        {
            if (IsBusy) { return; }

            try
            {
                IsBusy = true;
                await TaskService.Run(MacroPool.Get<T>().Execute);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
