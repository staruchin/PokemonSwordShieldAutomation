﻿using Prism.Commands;
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
        private CompositeDisposable Disposable { get; } = new CompositeDisposable();

        public ReactiveProperty<Bitmap> Frame { get; set; }

        private GameCapture m_capture = null;

        public ImageViewerControlViewModel(GameCapture capture)
        {
            m_capture = capture;

            Frame = m_capture.ObserveProperty(x => x.Frame).ToReactiveProperty().AddTo(Disposable);

            m_capture.Start();
        }

        public void Dispose()
        {
            Disposable.Dispose();
        }
    }
}