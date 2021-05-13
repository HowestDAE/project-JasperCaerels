using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Globalization;
using System.Windows.Data;
namespace Runescape_GrandExchange.View.Converters
{
    class TrendToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string trend = (string)value;
            if(trend.Equals("positive"))
                return new BitmapImage(new Uri("pack://Application:,,,/Resources/Graphics/ArrowIconUp.png", UriKind.Absolute));
            else if (trend.Equals("negative"))
                return new BitmapImage(new Uri("pack://Application:,,,/Resources/Graphics/ArrowIconDown.png", UriKind.Absolute));
            else
                return new BitmapImage(new Uri("pack://Application:,,,/Resources/Graphics/Rect.png", UriKind.Absolute));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
