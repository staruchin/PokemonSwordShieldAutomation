using Prism.Mvvm;
using Sta.SwitchController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.AutomationMacro
{
    public class SwitchClock : BindableBase, ISwitchClock
    {
        private DateTime? m_dateTime = null;
        /// <inheritdoc/>
        public DateTime? DateTime
        {
            get { return m_dateTime; }
            set { SetProperty(ref m_dateTime, value); }
        }

        public ISwitchController Controller { get; set; }
        public ICancellationRequest Cancellation { get; set; }

        /// <summary>
        /// <para>
        /// ゲーム画面から開始して、
        /// ホームボタンを押して「設定」→「本体」→「日付と時刻」→「現在の日付と時刻」と進み、
        /// 日付を1日進め、ゲーム画面に戻ってくる。
        /// </para>
        /// <para>
        /// 事前準備：「インターネットで時間をあわせる」をOFFにしておく。
        /// </para>
        /// </summary>
        public void IncreaseOneDayFromGameScreen()
        {
            Controller.PressAndRelease(ButtonType.Home, 50, 800);
            Controller.PressAndRelease(DPadCommand.Down, 50, 50);
            Controller.PressAndRelease(DPadCommand.Right, 50, 50);
            Controller.PressAndRelease(DPadCommand.Right, 50, 50);
            Controller.PressAndRelease(DPadCommand.Right, 50, 50);
            Controller.PressAndRelease(DPadCommand.Right, 50, 50);
            Controller.PressAndRelease(ButtonType.A, 50, 50);
            Controller.PressAndRelease(DPadCommand.Down, 2000, 50);
            Controller.PressAndRelease(ButtonType.A, 50, 50);
            Controller.PressAndRelease(DPadCommand.Down, 50, 50);
            Controller.PressAndRelease(DPadCommand.Down, 50, 50);
            Controller.PressAndRelease(DPadCommand.Down, 50, 50);
            Controller.PressAndRelease(DPadCommand.Down, 50, 50);
            Controller.PressAndRelease(ButtonType.A, 50, 200);
            Controller.PressAndRelease(DPadCommand.Down, 50, 50);
            Controller.PressAndRelease(DPadCommand.Down, 50, 50);
            Controller.PressAndRelease(ButtonType.A, 50, 150);

            Controller.PressAndRelease(DPadCommand.Right, 50, 50);
            Controller.PressAndRelease(DPadCommand.Right, 50, 50);

            IncreaseOneDayCursorOnDate();

            Controller.PressAndRelease(ButtonType.Home, 50, 1000);
            Controller.PressAndRelease(ButtonType.A, 50, 500);
        }

        /// <summary>
        /// 「現在の日付と時刻」画面の「日」にカーソルが当たっている状態から日付を1日進め、「OK」を押す。
        /// </summary>
        private void IncreaseOneDayCursorOnDate()
        {
            var nextDate = DateTime.Value + OneDay;

            Controller.PressAndRelease(DPadCommand.Up, 50, 50); // 日を変更

            if (nextDate.Day == 1)
            {
                Controller.PressAndRelease(DPadCommand.Left, 50, 50);
                Controller.PressAndRelease(DPadCommand.Up, 50, 50); // 月を変更

                if (nextDate.Month == 1)
                {
                    Controller.PressAndRelease(DPadCommand.Left, 50, 50);
                    Controller.PressAndRelease(DPadCommand.Up, 50, 50); // 年を変更

                    Controller.PressAndRelease(ButtonType.A, 50, 50);
                }

                Controller.PressAndRelease(ButtonType.A, 50, 50);
            }

            Controller.PressAndRelease(ButtonType.A, 50, 50);
            Controller.PressAndRelease(ButtonType.A, 50, 50);
            Controller.PressAndRelease(ButtonType.A, 50, 50);
            Controller.PressAndRelease(ButtonType.A, 50, 100); // OK

            DateTime = nextDate;
        }



        /// <summary>
        /// 「日付と時刻」画面の「現在の日付と時刻」にカーソルが当たっている状態から始めて、
        /// 「現在の日付と時刻」画面に入り、日付を1日進め、OKを押して戻ってくる、を指定した日数だけ繰り返す。
        /// </summary>
        /// <param name="date">開始日時を表す<see cref="System.DateTime"/></param>
        /// <param name="days">進める日数</param>
        public void Increase(DateTime date, int days)
        {
            for (int i = 0; i < days; i++)
            {
                if (IsEndOfDays)
                {
                    return;
                }

                date += TimeSpan.FromDays(1);

                Controller.PressAndRelease(ButtonType.A, 50, 100);
                Controller.PressAndRelease(DPadCommand.Left, 50, 50);
                Controller.PressAndRelease(DPadCommand.Left, 50, 50);
                Controller.PressAndRelease(DPadCommand.Left, 50, 50);

                //IncreaseDateByOneDayCore(date);
            }
        }

        private static TimeSpan OneDay { get; } = TimeSpan.FromDays(1);
        private static DateTime EndOfDays { get; } = new DateTime(2060, 12, 31);

        public bool IsEndOfDays => DateTime.Value >= EndOfDays;
    }
}
