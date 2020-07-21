using Prism.Commands;
using Prism.Mvvm;
using Sta.SwitchController;

namespace Sta.Modules.Controller.ViewModels
{
    public class ControllerPanelViewModel : BindableBase
    {
        public DelegateCommand<ButtonType?> PushButtonCommand { get; }

        private SerialSwitchController m_switchController = null;

        public ControllerPanelViewModel(SerialSwitchController controller)
        {
            m_switchController = controller;

            PushButtonCommand = new DelegateCommand<ButtonType?>(PushButtonCommandExecute);
        }

        private void PushButtonCommandExecute(ButtonType? button)
        {
            if (!button.HasValue)
            {
                return;
            }

            m_switchController.Push(button.Value);
        }
    }
}
