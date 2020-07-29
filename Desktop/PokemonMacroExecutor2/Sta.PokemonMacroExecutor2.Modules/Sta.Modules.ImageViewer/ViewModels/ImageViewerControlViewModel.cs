using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Sta.CaptureBoard;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reactive.Disposables;

namespace Sta.Modules.ImageViewer.ViewModels
{
    public class ImageViewerControlViewModel : BindableBase, IDisposable
    {
        private CompositeDisposable Disposables { get; } = new CompositeDisposable();

        public ReactiveProperty<Bitmap> Frame { get; set; }

        private IGameCapture m_capture = null;

        public ImageViewerControlViewModel(IGameCapture capture)
        {
            m_capture = capture;

            Frame = m_capture.ObserveProperty(x => x.Frame).ToReactiveProperty().AddTo(Disposables);

            m_capture.Start();
        }

        public void Dispose()
        {
            Disposables.Dispose();
        }
    }
}
