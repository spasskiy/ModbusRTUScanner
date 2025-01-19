using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ModbusRTUScanner.View.Converters
{
    public class HexConverter : IValueConverter
    {
        // Преобразование числа в HEX-строку
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return intValue.ToString("X"); // Форматирование в HEX
            }
            else if (value is byte byteValue)
            {
                return byteValue.ToString("X"); // Форматирование в HEX
            }
            return string.Empty;
        }

        // Преобразование HEX-строки в число
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string hexString)
            {
                if (int.TryParse(hexString, NumberStyles.HexNumber, culture, out int result))
                {
                    return result;
                }
            }
            return 0; // Возвращаем 0, если преобразование не удалось
        }
    }
}
