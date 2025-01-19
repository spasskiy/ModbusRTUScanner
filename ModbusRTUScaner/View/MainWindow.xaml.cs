using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ModbusRTUScanner.ViewModel;
using ModbusRTUScanner.View.MessageBoxCustom;

namespace ModbusRTUScanner
{
    /// <summary>
    /// Основное окно приложения
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {            
            InitializeComponent();            
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((this.DataContext as MainWindowViewModel).ViewManager.ScannerConsole.ConsoleOutput);
        }

        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("THIS");
            
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
    }
}