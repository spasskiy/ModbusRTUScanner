using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace ModbusMonitor.View.Style
{
    public partial class CustomWindow : ResourceDictionary
    {
        static CustomWindow()
        {
            CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(MinimizeCommand, MinimizeExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(MaximizeRestoreCommand, MaximizeRestoreExecuted));
            CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(CloseCommand, CloseExecuted));

        }
        public static readonly RoutedCommand MinimizeCommand = new("Minimize", typeof(WindowStyle));

        public static readonly RoutedCommand MaximizeRestoreCommand = new("MaximizeRestore", typeof(WindowStyle));

        public static readonly RoutedCommand CloseCommand = new("Close", typeof(WindowStyle));

        private static void MinimizeExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var window = (Window)sender;
            window.WindowState = WindowState.Minimized;
        }
        private static void MaximizeRestoreExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var window = (Window)sender;
            if (window.WindowState == WindowState.Normal)
            {
                window.WindowState = WindowState.Maximized;
            }
            else
            {
                window.WindowState = WindowState.Normal;
            }
        }
        private static void CloseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var window = (Window)sender;
            window.Close();
        }
    }
}
