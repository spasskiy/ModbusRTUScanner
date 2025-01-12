using ModbusRTUScanner.Model;
using ModbusRTUScanner.ViewModel;
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


        public RequestsWindow(ModbusDevice device)
        {
            Owner = Application.Current.MainWindow;
            DataContext = new RequestsWindowViewModel(device);
            InitializeComponent();
        }

        //private void ToggleRightPanel(object sender, RoutedEventArgs e)
        //{
        //    // Переключаем видимость правой панели
        //    RightPanel.Visibility = RightPanel.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        //}
    }
}
