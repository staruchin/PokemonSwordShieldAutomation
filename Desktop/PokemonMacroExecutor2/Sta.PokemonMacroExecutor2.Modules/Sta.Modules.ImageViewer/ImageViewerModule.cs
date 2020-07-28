using Sta.Modules.ImageViewer.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Sta.Modules.ImageViewer
{
    public class ImageViewerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var rm = containerProvider.Resolve<IRegionManager>();
            rm.RegisterViewWithRegion("ImageViewerArea", typeof(Views.ImageViewerControl));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}