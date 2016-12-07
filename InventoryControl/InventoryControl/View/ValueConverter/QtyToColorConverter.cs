using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InventoryControl
{
    public class QtyToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int quantity = (int)value;
            Color convertedColor = Color.Default;
            if (quantity >= 15)
                convertedColor = Color.Green;
            else if (quantity >= 5)
                convertedColor = Color.Blue;
            else
                convertedColor = Color.Red;

            return convertedColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
