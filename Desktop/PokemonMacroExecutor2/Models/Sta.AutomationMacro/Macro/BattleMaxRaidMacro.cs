using Sta.CaptureBoard;
using Sta.SwitchController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.AutomationMacro.Macro
{
    public class BattleMaxRaidMacro : AbstractMacro
    {
        private ISwitchController Controller => Param.Controller;
        private ICancellationRequest CancellationRequest => Param.CancellationRequest;
        private IGameCapture GameCapture => Param.GameCapture;

        public override void Execute()
        {
            while (true)
            {
                Controller.PressAndRelease(ButtonType.A, 50, 1500);     // 巣穴を選択
                Controller.PressAndRelease(ButtonType.A, 50, 1500);     // かたまり＞はい選択
                Controller.PressAndRelease(ButtonType.A, 50, 3000);    // レポート＞はい選択
                Controller.PressAndRelease(ButtonType.B, 50, 1500);     // 書き残した
                Controller.PressAndRelease(ButtonType.B, 50, 1500);     // 投げ込んだ
                Controller.PressAndRelease(ButtonType.A, 50, 3000);     // 巣穴を選択
                GameCapture.SaveFrame(null);
                Controller.PressAndRelease(DPadCommand.Down, 50, 1500); // みんな→ひとりで挑戦
                Controller.PressAndRelease(ButtonType.A, 50, 1500);     // ひとりで挑戦選択
                Controller.PressAndRelease(ButtonType.A, 50, 1500);     // ボールがありません＞はい選択
                Controller.PressAndRelease(ButtonType.A, 50, 1500);     // サポートのトレーナー

                // バトル開始

                CancellationRequest.ThrowIfCancellationRequested();

                do
                {
                    // ToDo: 負けたときも考慮

                    for (int i = 0; i < 20; i++)
                    {
                        Controller.PressAndRelease(ButtonType.A, 50, 500);
                    }

                    CancellationRequest.ThrowIfCancellationRequested();
                }
                while (!IsFinished());

                Controller.PressAndRelease(DPadCommand.Down, 50, 1500); // 捕まえる→捕まえない
                Controller.PressAndRelease(ButtonType.A, 50, 8000);     // ＞捕まえない選択
                Controller.PressAndRelease(ButtonType.A, 50, 8000);     // つぎへ

                CancellationRequest.ThrowIfCancellationRequested();
            }
        }

        private bool IsFinished()
        {
            //var target = GameCapture.Frame;
            //var template = 
            return false;
        }
    }
}
