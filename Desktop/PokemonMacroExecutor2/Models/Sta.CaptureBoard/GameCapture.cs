using OpenCvSharp;
using OpenCvSharp.Extensions;
using Prism.Mvvm;
using Sta.Utilities;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;


namespace Sta.CaptureBoard
{
    public class GameCapture : BindableBase, IGameCapture
    {
        private object m_frameLock = new object();

        private void Lock(Action action)
        {
            lock (m_frameLock)
            {
                action?.Invoke();
            }
        }

        private T Lock<T>(Func<T> func)
        {
            lock (m_frameLock)
            {
                return (func != null) ? func.Invoke() : default(T);
            }
        }

        private Bitmap m_frame = null;
        public Bitmap Frame
        {
            get
            {
                return Lock(() => m_frame);
            }
            set
            {
                Lock(() => SetProperty(ref m_frame, value));
            }
        }

        public void SaveFrame(string fileName)
        {
            Lock(() =>
            {
                Frame?.ToMat().SaveImage(!string.IsNullOrEmpty(fileName) ? fileName : SaveImageFileName);
            });
        }

        private static string SaveImageDir { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "PokemonMacroExecutor2");
        private static string SaveImageFileName => Path.Combine(SaveImageDir, DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".bmp");

        public void Start()
        {
            Task.Run(() =>
            {
                while (!Capture())
                {
                }
            })
            .NoWait();
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

                        //Frame = image.Resize(new OpenCvSharp.Size(Properties.Settings.Default.DisplayFrameWidth,
                        //                                          Properties.Settings.Default.DisplayFrameHeight)).ToBitmap();
                        Lock(() =>
                        {
                            Frame = image.ToBitmap();
                        });
                        Thread.Sleep(Properties.Settings.Default.DisplayInterval);
                    }
                }
            }
        }

    }
}
