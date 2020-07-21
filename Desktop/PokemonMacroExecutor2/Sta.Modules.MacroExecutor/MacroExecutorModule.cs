using Sta.Modules.MacroExecutor.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Sta.Modules.MacroExecutor
{
    public class MacroExecutorModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var rm = containerProvider.Resolve<IRegionManager>();
            rm.RegisterViewWithRegion("MacroSelectorArea", typeof(Views.MacroSelector));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}