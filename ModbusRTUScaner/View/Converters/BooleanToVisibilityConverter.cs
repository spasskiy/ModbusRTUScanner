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
    /// Конвертер для преобразования логического значения в значение видимости и обратно
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Преобразует логическое значение в значение видимости
        /// </summary>
        /// <param name="value">Логическое значение для преобразования</param>
        /// <param name="targetType">Тип целевого свойства (не используется)</param>
        /// <param name="parameter">Параметр конвертера (не используется)</param>
        /// <param name="culture">Культура (не используется)</param>
        /// <returns>Visibility.Visible, если значение true, иначе Visibility.Collapsed</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        /// <summary>
        /// Преобразует значение видимости обратно в логическое значение
        /// </summary>
        /// <param name="value">Значение видимости для преобразования</param>
        /// <param name="targetType">Тип целевого свойства (не используется)</param>
        /// <param name="parameter">Параметр конвертера (не используется)</param>
        /// <param name="culture">Культура (не используется)</param>
        /// <returns>true, если значение Visibility.Visible, иначе false</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibilityValue)
            {
                return visibilityValue == Visibility.Visible;
            }
            return false;
        }
    }
}
