using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace ModbusRTUScanner.View.Controls.Utility
{
    /// <summary>
    /// Утилита для поиска элементов в визуальном дереве WPF
    /// </summary>
    public class VisualTreeFinder
    {
        /// <summary>
        /// Рекурсивно ищет дочерний элемент указанного типа в визуальном дереве
        /// </summary>
        /// <typeparam name="T">Тип искомого дочернего элемента</typeparam>
        /// <param name="depObj">Родительский элемент, в котором производится поиск</param>
        /// <returns>Найденный дочерний элемент указанного типа или null, если элемент не найден</returns>
        public static T? FindChild<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) return null;

            // Проверка, является ли текущий элемент искомым типом
            if (depObj is T t)
            {
                return t;
            }

            // Рекурсивный поиск в дочерних элементах
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = FindChild<T>(child);
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }
    }
}
