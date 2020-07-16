using Sta.PokemonMacroExecutor2.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;
using Sta.Modules.ImageViewer;
using Sta.CaptureBoard;

namespace Sta.PokemonMacroExecutor2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<GameCapture>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ImageViewerModule>();
        }
    }
}
