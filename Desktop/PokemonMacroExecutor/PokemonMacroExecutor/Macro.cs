using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Sta.SwitchControllerLib;

namespace Sta.PokemonMacroExecutor
{
    public class Macro
    {
        private SwitchController m_controller = null;

        public Macro(SwitchController controller)
        {
            m_controller = controller;
        }

        /// <summary>
        /// 「日付と時刻」画面の「現在の日付と時刻」にカーソルが当たっている状態から始めて、
        /// 「現在の日付と時刻」画面に入り、日付を1日進め、OKを押して戻ってくる、を指定した日数だけ繰り返す。
        /// </summary>
        /// <param name="date">開始日時を表すDateTime</param>
        /// <param name="days">進める日数</param>
        public void IncreaseDate(DateTime date, int days)
        {
            for (int i = 0; i < days; i++)
            {
                if (IsEndOfDays(date))
                {
                    return;
                }

                date += TimeSpan.FromDays(1);

                m_controller.PushA(50, 100);
                m_controller.PushLeft(50, 50);
                m_controller.PushLeft(50, 50);
                m_controller.PushLeft(50, 50);

                IncreaseDateByOneDayCore(date);
            }
        }



        /// <summary>
        /// レイドの巣穴の前から始めて、「みんなで　挑戦！」を選んでからホームボタンを押して
        /// 日付を1日進め、ゲーム画面に戻ってきて巣穴の前に戻る、を3日分繰り返す。
        /// </summary>
        /// <param name="date">開始日時を表すDateTime</param>
        public void IncreaseDateByThreeDays(DateTime date)
        {
            IncreaseDateBySpecifiedDays(date, 3);
        }

        /// <summary>
        /// レイドの巣穴の前から始めて、「みんなで　挑戦！」を選んでからホームボタンを押して
        /// 日付を1日進め、ゲーム画面に戻ってきて巣穴の前に戻る、を3日分繰り返す。
        /// </summary>
        /// <param name="date">開始日時を表すDateTime</param>
        public void IncreaseDateBySpecifiedDays(DateTime date, int days)
        {
            for (int i = 0; i < days; i++)
            {
                if (IsEndOfDays(date))
                {
                    return;
                }

                date += TimeSpan.FromDays(1);

                m_controller.PushA(50, 400);
                m_controller.PushA(50, 400);
                m_controller.PushA(50, 800);
                m_controller.PushA(50, 2400);

                IncreaseDateByOne(date);

                m_controller.PushB(50, 1000);
                m_controller.PushA(50, 4500);
            }

            m_controller.PushA(50, 400);
            m_controller.PushA(50, 400);
            m_controller.PushA(50, 50);
        }

        public void Reset()
        {
            m_controller.PushHome(50, 800);
            m_controller.PushX(50, 250);
            m_controller.PushA(50, 3000);
            m_controller.PushA(50, 900);
            m_controller.PushA(50, 15000);

            m_controller.PushA(50, 500);
            m_controller.PushA(50, 500);
            m_controller.PushA(50, 500);
            m_controller.PushA(50, 500);

            m_controller.PushA(50, 7500);
        }

        public void ResetAndIncreaseDateByThreeDays(DateTime date)
        {
            Reset();
            IncreaseDateByThreeDays(date);
        }


        /// <summary>
        /// 巣穴の前から始めて、ねがいのかたまりを持っている分だけ投入してバトルを繰り返す。
        /// </summary>
        /// <param name="ct">キャンセル用のCancellationToken</param>
        /// <remarks>
        /// 事前準備：
        /// バッグの「かいふく」と「ボール」を空にしておく。
        /// 使用するポケモンを手持ちの先頭にしておく。
        /// 技を一つだけにして他は忘れさせておく。
        /// </remarks>
        public void BattleMaxRaid(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                m_controller.PushA(50, 3000);
                m_controller.PushDown(50, 1500);
                m_controller.PushA(50, 1500);
                m_controller.PushA(50, 1500);
                m_controller.PushA(50, 1500);
                m_controller.PushA(50, 3000);
                m_controller.PushB(50, 3000);
                m_controller.PushB(50, 1500);
                m_controller.PushDown(50, 3000);
            }
        }

        public void DrawLotoID(DateTime date)
        {
            while (!IsEndOfDays(date))
            {
                date += TimeSpan.FromDays(1);

                IncreaseDateByOne(date);

                m_controller.PushA(50, 500);
                m_controller.PushB(50, 500);
                m_controller.PushDown(50, 200);
                m_controller.PushA(50, 800);

                m_controller.PushB(50, 500);
                m_controller.PushB(50, 500);
                m_controller.PushB(50, 800);
                m_controller.PushA(50, 100); // レポート「はい」

                for (int i = 0; i < 80; i++)
                {
                    m_controller.PushB(50, 100);
                }

                //m_controller.PushB(50, 500);
                //m_controller.PushB(50, 800);
                //m_controller.PushB(50, 500);
                //m_controller.PushB(50, 500);
                //m_controller.PushB(50, 500);
                //m_controller.PushB(50, 2500); // くじの結果待ち

                //m_controller.PushB(50, 500);
                //m_controller.PushB(50, 500);
                //m_controller.PushB(50, 500);
                //m_controller.PushB(50, 500);
                //m_controller.PushB(50, 500);
                //m_controller.PushB(50, 2500); // 景品受け取り中

                //m_controller.PushB(50, 500);
                //m_controller.PushB(50, 500);
                //m_controller.PushB(50, 500);
            }
        }

        private static bool IsEndOfDays(DateTime date)
        {
            return (date.Year == 2060 && date.Month == 12 && date.Day == 31);
        }

        /// <summary>
        /// ホームボタンを押すところから始めて、日付を1日進め、ゲーム画面に戻ってくる。
        /// </summary>
        /// <param name="date"></param>
        private void IncreaseDateByOne(DateTime date)
        {
            m_controller.PushHome(50, 800);
            m_controller.PushDown(50, 50);
            m_controller.PushRight(50, 50);
            m_controller.PushRight(50, 50);
            m_controller.PushRight(50, 50);
            m_controller.PushRight(50, 50);
            m_controller.PushA(50, 50);
            m_controller.PushDown(2000, 50);
            m_controller.PushA(50, 50);
            m_controller.PushDown(50, 50);
            m_controller.PushDown(50, 50);
            m_controller.PushDown(50, 50);
            m_controller.PushDown(50, 50);
            m_controller.PushA(50, 200);
            m_controller.PushDown(50, 50);
            m_controller.PushDown(50, 50);
            m_controller.PushA(50, 150);

            m_controller.PushRight(50, 50);
            m_controller.PushRight(50, 50);

            IncreaseDateByOneDayCore(date);

            m_controller.PushHome(50, 1000);
            m_controller.PushA(50, 500);
        }

        /// <summary>
        /// 「現在の日付と時刻」画面の「日」にカーソルが当たっている状態から日付を1日進め、「OK」を押す。
        /// </summary>
        /// <param name="date">現在の日付</param>
        private void IncreaseDateByOneDayCore(DateTime date)
        {
            m_controller.PushUp(50, 50); // 日を変更

            if (date.Day == 1)
            {
                m_controller.PushLeft(50, 50);
                m_controller.PushUp(50, 50); // 月を変更

                if (date.Month == 1)
                {
                    m_controller.PushLeft(50, 50);
                    m_controller.PushUp(50, 50); // 年を変更
                }

                m_controller.PushA(50, 50);

                if (date.Month == 1)
                {
                    m_controller.PushA(50, 50);
                }
            }

            m_controller.PushA(50, 50);
            m_controller.PushA(50, 50);
            m_controller.PushA(50, 50);
            m_controller.PushA(50, 100); // OK
        }
    }
}
