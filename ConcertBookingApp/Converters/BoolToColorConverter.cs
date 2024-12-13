using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Converters
{
    class BoolToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isSelected && parameter is string param)
            {
                if (param == "Category")
                {
                    return isSelected ? Color.FromArgb("#C0C9FF") : Colors.White;
                }
                else if (param == "Heart")
                {
                    return isSelected ? "filledheart.png" : "heart.png";
                }
            }
            return Colors.Transparent;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
