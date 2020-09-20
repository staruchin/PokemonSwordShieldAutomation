using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.XImgProc;
using Sta.CaptureBoard;
using Sta.SwitchController;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sta.AutomationMacro.Macro
{
    public class BattleMaxRaidMacro : AbstractMacro, IDisposable
    {
        private ISwitchController Controller => Param.Controller;
        private ICancellationRequest CancellationRequest => Param.CancellationRequest;
        private IGameCapture GameCapture => Param.GameCapture;

        private bool m_isLightEmitted = false;

        /// <summary>
        /// 光の出ていない巣穴の前から始めて、ねがいのかたまりを投げ入れてレイドバトルを行う。
        /// ねがいのかたまりがなくなるまで繰り返す。
        /// </summary>
        /// <remarks>事前準備
        /// <list type="bullet">
        /// <item>インターネットから切断しておく。</item>
        /// <item>リュックの「ボール」ポケットを空にしておく。</item>
        /// </list>
        /// </remarks>
        public override void Execute()
        {
            do
            {
                if (!m_isLightEmitted)
                {
                    Controller.PressAndRelease(ButtonType.A, 50, 1000);     // 巣穴を選択
                    Controller.PressAndRelease(ButtonType.A, 50, 1000);     // かたまり＞はい選択
                    CancellationRequest.ThrowIfCancellationRequested();
                    Controller.PressAndRelease(ButtonType.A, 50, 3000);     // レポート＞はい選択
                    Controller.PressAndRelease(ButtonType.B, 50, 1000);     // 書き残した
                    Controller.PressAndRelease(ButtonType.B, 50, 1000);     // 投げ込んだ
                    m_isLightEmitted = true;
                    CancellationRequest.ThrowIfCancellationRequested();
                }

                Controller.PressAndRelease(ButtonType.A, 50, 1000);      // 巣穴を選択
                GameCapture.SaveFrame(null);
                Controller.PressAndRelease(DPadCommand.Down, 50, 500);   // みんな→ひとりで挑戦
                Controller.PressAndRelease(ButtonType.A, 50, 1000);      // ひとりで挑戦選択
                CancellationRequest.ThrowIfCancellationRequested();
                Controller.PressAndRelease(ButtonType.A, 50, 1000);      // ボールがありません＞はい選択
                Controller.PressAndRelease(ButtonType.A, 50, 10000);     // サポートのトレーナー

                // バトル開始
                do
                {
                    Controller.PressAndRelease(ButtonType.A, 50, 250);
                    Controller.PressAndRelease(ButtonType.A, 50, 250);
                    Controller.PressAndRelease(ButtonType.A, 50, 250);
                    Controller.PressAndRelease(ButtonType.A, 50, 250);
                    Controller.PressAndRelease(ButtonType.A, 50, 50);

                    if (IsWon())
                    {
                        Controller.PressAndRelease(DPadCommand.Down, 50, 500);  // つかまえる→つかまえない
                        Controller.PressAndRelease(ButtonType.A, 50, 5500);     // ＞つかまえない選択
                        Controller.PressAndRelease(ButtonType.A, 50, 6000);     // つぎへ
                        m_isLightEmitted = false;
                        break;
                    }

                    if (IsLost())
                    {
                        Thread.Sleep(7000);
                        break;
                    }
                }
                while (!CancellationRequest.IsCancellationRequested);
            }
            while (!CancellationRequest.IsCancellationRequested);
        }

        private Mat RaidFinishedTemplate { get; } = new Mat(@"Images\RaidFinishedTemplate.png");

        private bool IsWon()
        {
            using (var target = GameCapture.Frame.ToMat())
            {
                return MatchTemplate(target, RaidFinishedTemplate);
            }
        }

        private bool IsLost()
        {
            using (var image = GameCapture.Frame.ToMat())
            {
                return IsBlackScreen(image);
            }
        }

        private bool MatchTemplate(Mat image, Mat template, double threshold = 0.9d)
        {
            using (var result = image.MatchTemplate(template, TemplateMatchModes.CCoeffNormed))
            {
                OpenCvSharp.Point minLoc, maxLoc;
                double minVal, maxVal;
                result.MinMaxLoc(out minVal, out maxVal, out minLoc, out maxLoc);

                return maxVal >= threshold;
            }
        }

        private bool IsBlackScreen(Mat image)
        {
            using (var grayImage = image.CvtColor(ColorConversionCodes.RGB2GRAY))
            using (var binaryImage = grayImage.Threshold(100d, 255d, ThresholdTypes.Binary))
            {
                int whitePixels = binaryImage.CountNonZero();
                return whitePixels == 0;
            }
        }

        public void Dispose()
        {
            RaidFinishedTemplate.Dispose();
        }
    }
}
