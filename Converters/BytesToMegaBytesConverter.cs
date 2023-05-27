using System;
using System.Globalization;
using System.Windows.Data;

namespace ArbdTorrentClient.Converters
{
    public class BytesToMegaBytesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (long)value;

            return $"{v / (1024.0 * 1024.0):0.00} MB";            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
