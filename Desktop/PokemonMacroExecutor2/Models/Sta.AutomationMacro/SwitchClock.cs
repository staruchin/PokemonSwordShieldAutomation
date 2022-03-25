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

        private int m_daysCount = 0;
        /// <inheritdoc/>
        public int DaysCount
        {
            get { return m_daysCount; }
            set { SetProperty(ref m_daysCount, value); }
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
            Controller.PressAndRelease(DPadCommand.Right, 50, 50);
            Controller.PressAndRelease(DPadCommand.Right, 50, 50);
            Controller.PressAndRelease(DPadCommand.Down, 50, 50);
            Controller.PressAndRelease(DPadCommand.Right, 50, 50);
            Controller.PressAndRelease(ButtonType.A, 50, 50);
            Controller.PressAndRelease(DPadCommand.Down, 2000, 50);
            Controller.PressAndRelease(ButtonType.A, 50, 50);
            Controller.PressAndRelease(DPadCommand.Down, 700, 50);
            Controller.PressAndRelease(ButtonType.A, 50, 200);
            Controller.PressAndRelease(DPadCommand.Down, 50, 50);
            Controller.PressAndRelease(DPadCommand.Down, 50, 50);
            Controller.PressAndRelease(ButtonType.A, 50, 150);

            Controller.PressAndRelease(DPadCommand.Right, 50, 50);
            Controller.PressAndRelease(DPadCommand.Right, 50, 50);

            IncreaseOneDayCursorOnDate();

            Controller.PressAndRelease(ButtonType.Home, 50, 1000);
            Controller.PressAndRelease(ButtonType.A, 50, 600);
        }

        /// <summary>
        /// 「現在の日付と時刻」画面の「日」にカーソルが当たっている状態から日付を1日進め、「OK」を押す。
        /// </summary>
        public void IncreaseOneDayCursorOnDate()
        {
            bool backToStartOfDays = false;

            var nextDate = DateTime.Value + OneDay;

            Controller.PressAndRelease(DPadCommand.Up, 40, 40); // 日を変更

            if (nextDate.Day == 1)
            {
                Controller.PressAndRelease(DPadCommand.Left, 40, 40);
                Controller.PressAndRelease(DPadCommand.Up, 40, 40); // 月を変更

                if (nextDate.Month == 1)
                {
                    Controller.PressAndRelease(DPadCommand.Left, 40, 40);

                    if (nextDate.Year <= EndOfDays.Year)
                    {
                        Controller.PressAndRelease(DPadCommand.Up, 40, 40); // 年を変更(+1)
                    }
                    else
                    {
                        Controller.PressAndRelease(DPadCommand.Down, 6400, 50); // 年を変更(2000年まで戻す)
                        backToStartOfDays = true;
                    }

                    Controller.PressAndRelease(ButtonType.A, 40, 40);
                }

                Controller.PressAndRelease(ButtonType.A, 40, 40);
            }

            Controller.PressAndRelease(ButtonType.A, 40, 40);
            Controller.PressAndRelease(ButtonType.A, 40, 40);
            Controller.PressAndRelease(ButtonType.A, 40, 40);
            Controller.PressAndRelease(ButtonType.A, 40, 150); // OK

            if (!backToStartOfDays)
            {
                DaysCount--;
                DateTime = nextDate;
            }
            else
            {
                DateTime = StartOfDays;
            }
        }

        private static TimeSpan OneDay { get; } = TimeSpan.FromDays(1);
        private static DateTime EndOfDays { get; } = new DateTime(2060, 12, 31);
        private static DateTime StartOfDays { get; } = new DateTime(2000, 1, 1);

        public bool IsEndOfDays => DateTime.Value >= EndOfDays;
    }
}
