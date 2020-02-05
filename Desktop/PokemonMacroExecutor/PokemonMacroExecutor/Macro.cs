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
                if (date.Year == 2060 && date.Month == 12 && date.Day == 31)
                {
                    return;
                }

                int previousYear = date.Year;

                date += TimeSpan.FromDays(1);

                if (previousYear < date.Year)
                {
                    // 日月年を変更
                    m_controller.PushA(50, 125);
                    m_controller.PushLeft(50, 75);
                    m_controller.PushLeft(50, 75);
                    m_controller.PushLeft(50, 75);
                    m_controller.PushUp(50, 75);

                    m_controller.PushLeft(50, 75);
                    m_controller.PushUp(50, 75);

                    m_controller.PushLeft(50, 75);
                    m_controller.PushUp(50, 75);

                    m_controller.PushA(50, 75);
                    m_controller.PushA(50, 75);

                    m_controller.PushA(50, 75);
                    m_controller.PushA(50, 75);
                    m_controller.PushA(50, 75);
                    m_controller.PushA(50, 125);
                    continue;
                }

                if (date.Day == 1)
                {
                    // 日月を変更
                    m_controller.PushA(50, 125);
                    m_controller.PushLeft(50, 75);
                    m_controller.PushLeft(50, 75);
                    m_controller.PushLeft(50, 75);
                    m_controller.PushUp(50, 75);

                    m_controller.PushLeft(50, 75);
                    m_controller.PushUp(50, 75);

                    m_controller.PushA(50, 75);

                    m_controller.PushA(50, 75);
                    m_controller.PushA(50, 75);
                    m_controller.PushA(50, 75);
                    m_controller.PushA(50, 125);
                    continue;
                }

                // 日を変更
                m_controller.PushA(50, 125);
                m_controller.PushLeft(50, 75);
                m_controller.PushLeft(50, 75);
                m_controller.PushLeft(50, 75);
                m_controller.PushUp(50, 75);

                m_controller.PushA(50, 75);
                m_controller.PushA(50, 75);
                m_controller.PushA(50, 75);
                m_controller.PushA(50, 125);
            }
        }
    }
}
