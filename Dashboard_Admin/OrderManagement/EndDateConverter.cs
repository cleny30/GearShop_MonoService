using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Dashboard_Admin.OrderManagement
{
    public class EndDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || (value is DateTime && ((DateTime)value) == DateTime.MinValue))
            {
                return "In Process";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Conversion back is not necessary in this scenario
            throw new NotImplementedException();
        }
    }
}
