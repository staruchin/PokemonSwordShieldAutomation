using Sta.SwitchController;

namespace Sta.AutomationMacro.Macro
{
    public class MashAButtonMacro : AbstractMacro
    {
        private ICancellationRequest CancellationRequest => Param.CancellationRequest;
        private ISwitchController Controller => Param.Controller;

        /// <summary>
        /// Aボタンを連打する。
        /// </summary>
        public override void Execute()
        {
            while (!CancellationRequest.IsCancellationRequested)
            {
                Controller.PressAndRelease(ButtonType.A, 50, 200);
            }
        }
    }
}
