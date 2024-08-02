using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace ModbusRTUScanner.View.Converters
{
    /// <summary>
    /// Конвертер для преобразования логического значения в обратное значение видимости и обратно
    /// </summary>
    public class BooleanToVisibilityReverseConverter : IValueConverter
    {
        /// <summary>
        /// Преобразует логическое значение в обратное значение видимости
        /// </summary>
        /// <param name="value">Логическое значение для преобразования</param>
        /// <param name="targetType">Тип целевого свойства (не используется)</param>
        /// <param name="parameter">Параметр конвертера (не используется)</param>
        /// <param name="culture">Культура (не используется)</param>
        /// <returns>Visibility.Collapsed, если значение true, иначе Visibility.Visible</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Collapsed : Visibility.Visible;
            }
            return Visibility.Visible;
        }

        /// <summary>
        /// Преобразует обратное значение видимости обратно в логическое значение
        /// </summary>
        /// <param name="value">Значение видимости для преобразования</param>
        /// <param name="targetType">Тип целевого свойства (не используется)</param>
        /// <param name="parameter">Параметр конвертера (не используется)</param>
        /// <param name="culture">Культура (не используется)</param>
        /// <returns>true, если значение Visibility.Collapsed, иначе false</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibilityValue)
            {
                return visibilityValue != Visibility.Visible;
            }
            return true;
        }
    }
}
