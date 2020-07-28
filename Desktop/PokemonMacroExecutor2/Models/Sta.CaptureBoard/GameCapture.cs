using OpenCvSharp;
using OpenCvSharp.Extensions;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Sta.CaptureBoard
{
    public class GameCapture : BindableBase
    {
        private Bitmap m_frame = null;
        public Bitmap Frame
        {
            get { return m_frame; }
            set { SetProperty(ref m_frame, value); }
        }

        public void Start()
        {
            Task.Run(() =>
            {
                while (!Capture())
                {
                }
            });
        }

        private bool Capture()
        {
            using (var capture = new VideoCapture(Properties.Settings.Default.CaptureDeviceId))
            {
                if (!capture.IsOpened())
                {
                    return false;
                }

                capture.FrameWidth = Properties.Settings.Default.CaptureFrameWidth;
                capture.FrameHeight = Properties.Settings.Default.CaptureFrameHeight;
                capture.Fps = Properties.Settings.Default.CaptureFps;

                using (var image = new Mat())
                {
                    while (true)
                    {
                        if (!capture.Grab())
                        {
                            return false;
                        }

                        if (!capture.Retrieve(image))
                        {
                            return false;
                        }

                        if (image.Empty())
                        {
                            return false;
                        }

                        Frame = image.Resize(new OpenCvSharp.Size(Properties.Settings.Default.DisplayFrameWidth,
                                                                  Properties.Settings.Default.DisplayFrameHeight)).ToBitmap();

                        Thread.Sleep(Properties.Settings.Default.DisplayInterval);
                    }
                }
            }
        }

    }
}
