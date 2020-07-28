using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Sta.AutomationMacro;

namespace Sta.Modules.MacroExecutor
{
    public class MacroExecutorModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var rm = containerProvider.Resolve<IRegionManager>();
            rm.RegisterViewWithRegion("MacroSelectorArea", typeof(Views.MacroPanel));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}