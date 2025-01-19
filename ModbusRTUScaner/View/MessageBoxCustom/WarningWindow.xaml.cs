using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ModbusRTUScanner.View.MessageBoxCustom
{
    /// <summary>
    /// Логика взаимодействия для WarningWindow.xaml
    /// </summary>
    public partial class WarningWindow : Window
    {
        public string warning { get; set; }
        public WarningWindow(string str)
        {
            warning = str;

            // Проверяем, что вызывающий поток является STA
            if (Application.Current.Dispatcher.CheckAccess())
            {
                InitializeComponent();
                DataContext = this;
            }
            else
            {
                // Если вызывающий поток не является STA, используйте Dispatcher для выполнения в STA
                Application.Current.Dispatcher.Invoke(() =>
                {
                    InitializeComponent();
                    DataContext = this;
                });
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.SizeToContent = SizeToContent.WidthAndHeight;
        }

    }
}
