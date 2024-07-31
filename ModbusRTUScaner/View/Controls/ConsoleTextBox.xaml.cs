using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ModbusRTUScanner.View.Controls.Utility;


namespace ModbusRTUScanner.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для ConsoleTextBox.xaml
    /// </summary>
    public partial class ConsoleTextBox : UserControl, INotifyPropertyChanged
    {
        private ScrollBar? _scrollBar;
        private string? _text;
        public string? Text
        {
            get => _text;            
            set => SetOptions(nameof(Text), ref _text, value);            
        }
        public ConsoleTextBox()
        {
            DataContext = this;
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _scrollBar = VisualTreeFinder.FindChild<ScrollBar>(textBox);
            if (_scrollBar != null)
            {
                _scrollBar.IsVisibleChanged += ScrollBar_IsVisibleChanged;
            }
        }
        private void ScrollBar_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {           
            if (_scrollBar is not null)
            {
                if (_scrollBar.IsVisible)
                {
                    clearButton.Margin = new Thickness(0, 4, 22, 0);
                }
                else
                {
                    clearButton.Margin = new Thickness(0, 4, 4, 0);
                }
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Text = string.Empty;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void SetOptions<T>(string Property, ref T variable, T value)
        {
            variable = value;
            OnPropertyChanged(new PropertyChangedEventArgs(Property));
        }
        #endregion

    }

}
