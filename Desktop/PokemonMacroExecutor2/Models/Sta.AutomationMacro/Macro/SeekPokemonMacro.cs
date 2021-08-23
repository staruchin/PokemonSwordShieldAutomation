using Sta.CaptureBoard;
using Sta.SwitchController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.AutomationMacro.Macro
{
    public class SeekPokemonMacro : AbstractMacro
    {
        private ISwitchClock Clock => Param.Clock;
        private ISwitchController Controller => Param.Controller;
        private ICancellationRequest CancellationRequest => Param.CancellationRequest;
        private IGameCapture GameCapture => Param.GameCapture;

        /// <summary>
        /// 光の出ている巣穴の前から始めて、指定したポケモンのシルエットが出るまで日付を変更する。
        /// </summary>
        /// <remarks>事前準備
        /// <list type="bullet">
        /// <item>「インターネットで時間をあわせる」をOFFにしておく。</item>
        /// <item>巣穴にねがいのかたまりを投げ入れて光を出しておく。</item>
        /// <item>インターネットから切断しておく。</item>
        /// </list>
        public override void Execute()
        {
            Controller.PressAndRelease(ButtonType.A, 50, 900);       // 巣穴を選択
            GameCapture.SaveFrame(null);
            if (Found())
            {
                return;
            }
            
            Controller.PressAndRelease(ButtonType.A, 50, 900);       // みんなで挑戦選択
            Controller.PressAndRelease(ButtonType.A, 50, 2700);      // ボールがありません＞はい選択（ボールがなければ）

            while (!Clock.IsEndOfDays)
            {
                Clock.IncreaseOneDayFromGameScreen();

                Controller.PressAndRelease(ButtonType.B, 50, 900);   // やめる
                Controller.PressAndRelease(ButtonType.A, 50, 4500);  // 募集をやめる＞はい選択

                CancellationRequest.ThrowIfCancellationRequested();

                Controller.PressAndRelease(ButtonType.A, 50, 700);   // 巣穴を選択
                Controller.PressAndRelease(ButtonType.A, 50, 700);   // エネルギーがあふれでてる
                Controller.PressAndRelease(ButtonType.A, 50, 900);   // ワット手に入れた
                
                GameCapture.SaveFrame(null);
                if (Found())
                {
                    return;
                }

                Controller.PressAndRelease(ButtonType.A, 50, 900);   // みんなで挑戦選択
                Controller.PressAndRelease(ButtonType.A, 50, 2700);  // ボールがありません＞はい選択（ボールがなければ）
            }
        }

        private bool Found()
        {
            return true;
        }
    }
}
