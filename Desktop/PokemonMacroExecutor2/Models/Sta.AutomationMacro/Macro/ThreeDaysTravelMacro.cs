using OpenCvSharp;
using OpenCvSharp.Extensions;
using Sta.CaptureBoard;
using Sta.SwitchController;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Sta.AutomationMacro.Macro
{
    public class ThreeDaysTravelMacro : AbstractMacro
    {
        private ISwitchClock Clock => Param.Clock;
        private ISwitchController Controller => Param.Controller;
        private ICancellationRequest CancellationRequest => Param.CancellationRequest;
        private IGameCapture GameCapture => Param.GameCapture;

        private bool m_canGoNext = false;
        public bool CanGoNext
        {
            get { return m_canGoNext; }
            set { SetProperty(ref m_canGoNext, value); }
        }

        /// <summary>
        /// 光の出ている巣穴の前から始めて、日付を3日進め、4日目のポケモンのシルエットが表示されたところで止まる。
        /// </summary>
        /// <remarks>事前準備
        /// <list type="bullet">
        /// <item>「インターネットで時間をあわせる」をOFFにしておく。</item>
        /// <item>インターネットから切断しておく。</item>
        /// <item>巣穴にねがいのかたまりを投げ入れて光を出しておく。</item>
        /// <item>最初のワットは回収しておく。</item>
        /// </list>
        public override void Execute()
        {
            try
            {
                CheckSearchTemplates();
                InternalExecute();
            }
            finally
            {
                CanGoNext = false;
            }
        }

        private IList<Mat> SearchTemplates { get; } = new List<Mat>();
        private void CheckSearchTemplates()
        {
            ClearSearchTemplates();
            foreach (var file in Directory.EnumerateFiles(@"Images\SearchTemplates", "*.png", SearchOption.TopDirectoryOnly))
            {
                SearchTemplates.Add(new Mat(file));
            }
        }

        private void ClearSearchTemplates()
        {
            foreach (var template in SearchTemplates)
            {
                template.Dispose();
            }
            SearchTemplates.Clear();
        }

        private void InternalExecute()
        {
            while (!CancellationRequest.IsCancellationRequested)
            {
                Controller.PressAndRelease(ButtonType.A, 50, 900);       // 巣穴を選択

                Clock.DaysCount = 3;
                while (Clock.DaysCount > 0)
                {
                    Controller.PressAndRelease(ButtonType.A, 50, 900);       // みんなで挑戦選択
                    Controller.PressAndRelease(ButtonType.A, 50, 2700);      // ボールがありません＞はい選択（ボールがなければ）

                    Clock.IncreaseOneDayFromGameScreen();

                    Controller.PressAndRelease(ButtonType.B, 50, 900);   // やめる
                    Controller.PressAndRelease(ButtonType.A, 50, 4900);  // 募集をやめる＞はい選択

                    CancellationRequest.ThrowIfCancellationRequested();

                    Controller.PressAndRelease(ButtonType.A, 50, 700);   // 巣穴を選択
                    Controller.PressAndRelease(ButtonType.A, 50, 700);   // エネルギーがあふれでてる
                    Controller.PressAndRelease(ButtonType.A, 50, 900);   // ワット手に入れた

                    CancellationRequest.ThrowIfCancellationRequested();
                }

                GameCapture.SaveFrame(null);
                bool stop = CheckFourthDayPokemon();
                if (stop)
                {
                    WaitGoNext();
                }
                CheckSearchTemplates();

                Controller.PressAndRelease(ButtonType.Home, 50, 600);
                Controller.PressAndRelease(ButtonType.X, 50, 250);
                Controller.PressAndRelease(ButtonType.A, 50, 3500);

                Controller.PressAndRelease(ButtonType.A, 50, 1250);
                Controller.PressAndRelease(ButtonType.A, 50, 19000);
                Controller.PressAndRelease(ButtonType.A, 50, 9000);
            }
        }

        private bool CheckFourthDayPokemon()
        {
            if (!SearchTemplates.Any())
            {
                return true;
            }

            return MatchTemplate();
        }

        private bool WaitGoNext()
        {
            CanGoNext = true;
            using (var wait = new EventWaitHandle(false, EventResetMode.ManualReset, "WAIT_NEXT_3_DAYS"))
            {
                while (!wait.WaitOne(1000))
                {
                    CancellationRequest.ThrowIfCancellationRequested();
                }
            }
            CanGoNext = false;
            return true;
        }

        private bool MatchTemplate()
        {
            using (var target = GameCapture.Frame.ToMat())
            {
                foreach (var template in SearchTemplates)
                {
                    if (MatchTemplate(target, template))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool MatchTemplate(Mat image, Mat template, double threshold = 0.96d)
        {
            using (var result = image.MatchTemplate(template, TemplateMatchModes.CCoeffNormed))
            {
                Point minLoc, maxLoc;
                double minVal, maxVal;
                result.MinMaxLoc(out minVal, out maxVal, out minLoc, out maxLoc);

                return maxVal >= threshold;
            }
        }
    }
}
