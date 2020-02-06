using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                m_controller.PushUp(50, 50);

                if (date.Day == 1)
                {
                    m_controller.PushLeft(50, 50);
                    m_controller.PushUp(50, 50);

                    if (date.Month == 1)
                    {
                        m_controller.PushLeft(50, 50);
                        m_controller.PushUp(50, 50);
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
                m_controller.PushA(50, 100);
            }
        }

        private static bool IsEndOfDays(DateTime date)
        {
            return (date.Year == 2060 && date.Month == 12 && date.Day == 31);
        }

        public void IncreaseDateByThreeDays(DateTime date)
        {
            for (int i = 0; i < 3; i++)
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
                m_controller.PushA(50, 50); // OK
                m_controller.PushHome(50, 1000);
                m_controller.PushA(50, 500);
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
    }
}
