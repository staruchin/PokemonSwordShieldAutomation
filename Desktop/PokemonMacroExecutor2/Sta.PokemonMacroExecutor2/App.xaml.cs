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
            containerRegistry.RegisterInstance(new GameCapture());

            var controller = new SerialSwitchController();
            var serialPort = new SerialPortService();
            var macro = new MacroService();
            var cancelableTask = new CancelableTaskService();
            var clock = new SwitchClock();
            var work = new WorkSituation();

            controller.SerialPort = serialPort;
            macro.CancelableTask = cancelableTask;
            macro.Controller = controller;
            macro.Clock = clock;
            macro.Work = work;
            clock.Controller = controller;
            clock.Cancellation = cancelableTask;

            containerRegistry.RegisterInstance<ISwitchController>(controller);
            containerRegistry.RegisterInstance<ISerialPortService>(serialPort);
            containerRegistry.RegisterInstance<IMacroService>(macro);
            containerRegistry.RegisterInstance<ICancelableTaskService>(cancelableTask);
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
