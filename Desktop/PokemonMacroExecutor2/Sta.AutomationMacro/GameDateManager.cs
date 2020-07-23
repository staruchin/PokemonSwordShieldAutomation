using Prism.Mvvm;
using Sta.SwitchController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.AutomationMacro
{
    public class GameDateManager : BindableBase, IGameDateManager
    {
        private DateTime? m_gameDate = null;
        /// <inheritdoc/>
        public DateTime? GameDate
        {
            get { return m_gameDate; }
            set { SetProperty(ref m_gameDate, value); }
        }

        public ISwitchController Controller { get; set; }

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

                Controller.PressAndReleaseButton(ButtonType.A, 50, 100);
                Controller.PressAndReleaseDPad(DPadCommand.Left, 50, 50);
                Controller.PressAndReleaseDPad(DPadCommand.Left, 50, 50);
                Controller.PressAndReleaseDPad(DPadCommand.Left, 50, 50);

                //IncreaseDateByOneDayCore(date);
            }
        }


        private static DateTime EndOfDays { get; } = new DateTime(2060, 12, 31);

        public bool IsEndOfDays => GameDate.Value >= EndOfDays;
    }
}
