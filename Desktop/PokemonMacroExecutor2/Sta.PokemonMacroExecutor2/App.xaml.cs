using Prism.Ioc;
using Prism.Modularity;
using Sta.CaptureBoard;
using Sta.Modules.Controller;
using Sta.Modules.ImageViewer;
using Sta.Modules.MacroExecutor;
using Sta.PokemonMacroExecutor2.Views;
using Sta.SwitchController;
using System.Windows;

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
            containerRegistry.RegisterInstance(new GameCapture());
            containerRegistry.RegisterInstance(new SerialSwitchController());
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ImageViewerModule>();
            moduleCatalog.AddModule<ControllerModule>();
            moduleCatalog.AddModule<MacroExecutorModule>();
        }
    }
}
