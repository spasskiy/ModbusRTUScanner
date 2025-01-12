using ModbusRTUScanner.Model;
using ModbusRTUScanner.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ModbusRTUScanner.View
{
    /// <summary>
    /// Логика взаимодействия для RequestsWindow.xaml
    /// </summary>
    public partial class RequestsWindow : Window
    {


        public RequestsWindow(ModbusDevice device)
        {
            Owner = Application.Current.MainWindow;
            DataContext = new RequestsWindowViewModel(device);
            InitializeComponent();
        }

        private void TextBox_PreviewHexInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void TextBox_PreviewDigitInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowedDec(e.Text);
        }

        private bool IsTextAllowed(string text)
        {
            {
                // Регулярное выражение для проверки HEX-значений
                Regex regex = new Regex(@"^[0-9a-fA-F:]*$");
                return regex.IsMatch(text);
            }
        }

        private bool IsTextAllowedDec(string text)
        {
            {
                // Регулярное выражение для проверки HEX-значений
                Regex regex = new Regex(@"^\d*$");
                return regex.IsMatch(text);
            }
        }
    }
}
