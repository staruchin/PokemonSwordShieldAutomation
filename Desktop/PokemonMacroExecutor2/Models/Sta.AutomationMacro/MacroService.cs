﻿using Prism.Mvvm;
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
            Execute<DrawLotoIdMacro>();
        }

        public void BattleMaxRaid()
        {
            Execute<BattleMaxRaidMacro>();
        }

        private bool IsBusy
        {
            get { return Work.IsBusy; }
            set { Work.IsBusy = value; }
        }

        private async void Execute<T>() where T : IMacro
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
