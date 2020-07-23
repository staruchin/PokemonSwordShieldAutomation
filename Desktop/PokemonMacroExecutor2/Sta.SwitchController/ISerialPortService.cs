using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.SwitchController
{
    public interface ISerialPortService : INotifyPropertyChanged
    {
        string PortName { get; set; }
        bool IsOpen { get; }
        void Open();
        void Close();
        void Write(byte[] buffer);
    }
}
