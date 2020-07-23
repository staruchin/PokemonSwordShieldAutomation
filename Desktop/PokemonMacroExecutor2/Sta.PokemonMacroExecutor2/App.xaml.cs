using Prism.Ioc;
using Prism.Modularity;
using Sta.AutomationMacro;
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

            var controller = new SerialSwitchController();
            var serialPort = new SerialPortService();
            var macro = new MacroService();
            var gameDate = new GameDateManager();

            controller.SerialPort = serialPort;
            macro.Controller = controller;
            macro.GameDateManager = gameDate;
            gameDate.Controller = controller;

            containerRegistry.RegisterInstance<ISwitchController>(controller);
            containerRegistry.RegisterInstance<ISerialPortService>(serialPort);
            containerRegistry.RegisterInstance<IMacroService>(macro);
            containerRegistry.RegisterInstance<IGameDateManager>(gameDate);
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ImageViewerModule>();
            moduleCatalog.AddModule<ControllerModule>();
            moduleCatalog.AddModule<MacroExecutorModule>();
        }
    }
}
