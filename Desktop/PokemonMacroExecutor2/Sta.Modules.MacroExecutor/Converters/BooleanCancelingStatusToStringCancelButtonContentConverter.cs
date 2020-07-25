using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Sta.Modules.MacroExecutor.Converters
{
    public class BooleanCancelingStatusToStringCancelButtonContentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
            {
                throw new InvalidOperationException("The source must be a boolean");
            }

            bool isCanceling = (bool)value;
            return !isCanceling ? Properties.Resources.CancelButtonContent : Properties.Resources.CancelButtonContentWhenCanceling;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
