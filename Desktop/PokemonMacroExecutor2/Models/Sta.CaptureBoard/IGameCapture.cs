using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.CaptureBoard
{
    public interface IGameCapture : INotifyPropertyChanged
    {
        Bitmap Frame { get; }

        void Start();

        void SaveFrame(string fileName);
    }
}
