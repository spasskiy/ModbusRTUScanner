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
    /// Логика взаимодействия для DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public string warning { get; set; }
        public string yes { get; set; }
        public string no { get; set; }

        public DialogWindow(string str, string yes = "Да", string no = "Нет")
        {
            this.yes = yes;
            this.no = no;
            warning = str;
            InitializeComponent();
            DataContext = this;
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
