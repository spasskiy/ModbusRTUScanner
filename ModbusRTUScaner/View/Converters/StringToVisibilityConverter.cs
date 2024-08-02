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
    /// Конвертер для преобразования строкового значения в значение видимости
    /// </summary>
    public class StringToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Преобразует строковое значение в значение видимости
        /// </summary>
        /// <param name="value">Строковое значение для преобразования</param>
        /// <param name="targetType">Тип целевого свойства (не используется)</param>
        /// <param name="parameter">Параметр конвертера (не используется)</param>
        /// <param name="culture">Культура (не используется)</param>
        /// <returns>Visibility.Visible, если строка пуста или null, иначе Visibility.Collapsed</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrEmpty((string)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Преобразует значение видимости обратно в строковое значение
        /// </summary>
        /// <param name="value">Значение видимости для преобразования</param>
        /// <param name="targetType">Тип целевого свойства (не используется)</param>
        /// <param name="parameter">Параметр конвертера (не используется)</param>
        /// <param name="culture">Культура (не используется)</param>
        /// <returns>Выбрасывает NotImplementedException, так как обратное преобразование не реализовано</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
