using ModbusRTUScanner.Model;
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

namespace ModbusRTUScanner.View
{
    /// <summary>
    /// Логика взаимодействия для RequestsWindow.xaml
    /// </summary>
    public partial class RequestsWindow : Window
    {
        private ModbusDevice _device;
        public string TestString {  get; set; }
        public RequestsWindow(ModbusDevice device)
        {
            InitializeComponent();
            DataContext = this;
            _device = device;
            TestString = $"RequestWindow {_device.Address}";
        }

        private void ToggleRightPanel(object sender, RoutedEventArgs e)
        {
            // Переключаем видимость правой панели
            RightPanel.Visibility = RightPanel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
