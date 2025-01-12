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
    public class ParityToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Parity parity)
            {
                return parity switch
                {
                    Parity.None => "Нет",
                    Parity.Odd => "Нечётная",
                    Parity.Even => "Чётная",
                    Parity.Mark => "Mark",
                    Parity.Space => "Space",
                    _ => parity.ToString()
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
                    "Нет" => Parity.None,
                    "Нечётная" => Parity.Odd,
                    "Чётная" => Parity.Even,
                    "Mark" => Parity.Mark,
                    "Space" => Parity.Space,
                    _ => Parity.None
                };
            }
            return value;
        }
    }
}
