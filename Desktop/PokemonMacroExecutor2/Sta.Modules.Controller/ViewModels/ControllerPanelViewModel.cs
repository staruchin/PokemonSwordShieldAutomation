using Prism.Commands;
using Prism.Mvvm;
using Sta.SwitchController;

namespace Sta.Modules.Controller.ViewModels
{
    public class ControllerPanelViewModel : BindableBase
    {
        public DelegateCommand<ButtonType?> PushButtonCommand { get; }

        private ISwitchController m_switchController = null;

        public ControllerPanelViewModel(ISwitchController controller)
        {
            m_switchController = controller;

            PushButtonCommand = new DelegateCommand<ButtonType?>(PushButton);
        }

        private void PushButton(ButtonType? button)
        {
            if (!button.HasValue)
            {
                return;
            }

            m_switchController.PressAndReleaseButton(button.Value, Properties.Settings.Default.PressAndReleaseDuration);
        }
    }
}
