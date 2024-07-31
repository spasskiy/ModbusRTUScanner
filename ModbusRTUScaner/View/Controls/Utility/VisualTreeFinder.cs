using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace ModbusRTUScanner.View.Controls.Utility
{
    public class VisualTreeFinder
    {
        public static T? FindChild<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) return null;

            if (depObj is T t)
            {
                return t;
            }

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
