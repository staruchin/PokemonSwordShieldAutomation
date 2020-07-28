using Sta.SwitchController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sta.Modules.Controller.ViewModels
{
    public class StickParameter
    {
        public StickType StickType { get; set; }
        public byte X { get; set; }
        public byte Y { get; set; }
    }
}
