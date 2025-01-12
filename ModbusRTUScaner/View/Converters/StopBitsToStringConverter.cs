using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ModbusRTUScanner.View.Converters
{
    public class StopBitsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is StopBits stopBits)
            {
                return stopBits switch
                {
                    StopBits.None => "Нет",
                    StopBits.One => "1",
                    StopBits.Two => "2",
                    StopBits.OnePointFive => "1.5",
                    _ => stopBits.ToString()
                };
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                return str switch
                {
                    "Нет" => StopBits.None,
                    "1" => StopBits.One,
                    "2" => StopBits.Two,
                    "1.5" => StopBits.OnePointFive,
                    _ => StopBits.None
                };
            }
            return value;
        }
    }
}
