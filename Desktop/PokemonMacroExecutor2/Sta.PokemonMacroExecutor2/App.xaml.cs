using Prism.Ioc;
using Prism.Modularity;
using Sta.AutomationMacro;
using Sta.CaptureBoard;
using Sta.Modules.Controller;
using Sta.Modules.ImageViewer;
using Sta.Modules.MacroExecutor;
using Sta.PokemonMacroExecutor2.Views;
using Sta.SwitchController;
using Sta.Utilities;
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
            var controller = new SerialSwitchController();
            var serialPort = new SerialPortService();
            var macro = new MacroService();
            var cancelableTask = new CancelableTaskService();
            var clock = new SwitchClock();
            var work = new WorkSituation();
            var gameCapture = new GameCapture();
            var macroPool = new MacroPool(clock, controller, cancelableTask, gameCapture);

            controller.SerialPort = serialPort;
            macro.TaskService = cancelableTask;
            macro.Work = work;
            macro.MacroPool = macroPool;
            clock.Controller = controller;
            clock.Cancellation = cancelableTask;

            containerRegistry.RegisterInstance<IGameCapture>(gameCapture);
            containerRegistry.RegisterInstance<ISwitchController>(controller);
            containerRegistry.RegisterInstance<ISerialPortService>(serialPort);
            containerRegistry.RegisterInstance<IMacroService>(macro);
            containerRegistry.RegisterInstance<IMacroPool>(macroPool);
            containerRegistry.RegisterInstance<ITaskService>(cancelableTask);
            containerRegistry.RegisterInstance<ICanceler>(cancelableTask);
            containerRegistry.RegisterInstance<ICancellationRequest>(cancelableTask);
            containerRegistry.RegisterInstance<ISwitchClock>(clock);
            containerRegistry.RegisterInstance<IWorkSituation>(work);
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ImageViewerModule>();
            moduleCatalog.AddModule<ControllerModule>();
            moduleCatalog.AddModule<MacroExecutorModule>();
        }
    }
}
