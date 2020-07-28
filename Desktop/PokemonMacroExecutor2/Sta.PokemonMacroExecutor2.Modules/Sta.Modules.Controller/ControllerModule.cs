using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Sta.SwitchController;

namespace Sta.Modules.Controller
{
    public class ControllerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var rm = containerProvider.Resolve<IRegionManager>();
            rm.RegisterViewWithRegion("ControllerArea", typeof(Views.ControllerPanel));
            rm.RegisterViewWithRegion("SerialPortSelectorArea", typeof(Views.SerialPortSelector));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}