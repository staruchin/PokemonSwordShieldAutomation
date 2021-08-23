using Sta.CaptureBoard;
using Sta.SwitchController;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.AutomationMacro.Macro
{
    public class RapidTimeTravelMacro : AbstractMacro
    {
        private ISwitchClock Clock => Param.Clock;
        private ISwitchController Controller => Param.Controller;
        private ICancellationRequest CancellationRequest => Param.CancellationRequest;

        /// <summary>
        /// <para>
        /// 「日付と時刻」画面の「現在の日付と時刻」にカーソルが当たっている状態から始めて、
        /// 「現在の日付と時刻」画面に入り、日付を1日進め、OKを押して戻ってくる、を指定した日数だけ繰り返す。
        /// </para>
        /// <para>
        /// 事前準備：一度「現在の日付と時刻」画面に入り、OKを押しておく。
        /// すなわち、次に「現在の日付と時刻」画面に入った際に、カーソルがOKに当たっている状態となるようにしておく。
        /// </summary>
        public override void Execute()
        {
            while (Clock.DaysCount > 0 && !CancellationRequest.IsCancellationRequested)
            {
                Controller.PressAndRelease(ButtonType.A, 50, 150);
                Controller.PressAndRelease(DPadCommand.Left, 50, 50);
                Controller.PressAndRelease(DPadCommand.Left, 50, 50);
                Controller.PressAndRelease(DPadCommand.Left, 50, 50); // 「日」にカーソルが当たる

                Clock.IncreaseOneDayCursorOnDate();
            }
        }
    }
}
