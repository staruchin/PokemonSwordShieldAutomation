using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Sta.Modules.Controller.Converteres
{
    public class BooleanSerialPortConnectionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool)) { return Binding.DoNothing; }

            bool isOpen = (bool)value;
            return isOpen ? Properties.Resources.ConnectionStatusConnected : Properties.Resources.ConnectionStatusNotConnected;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
