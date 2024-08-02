using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using ModbusRTUScanner.View.Controls.Utility;
using static System.Net.Mime.MediaTypeNames;


namespace ModbusRTUScanner.View.Controls
{
    public partial class ConsoleTextBox : UserControl
    {
        /// <summary>
        /// Скроллбар текстового поля
        /// </summary>
        private ScrollBar? _scrollBar;

        public static readonly DependencyProperty TextProperty;
        /// <summary>
        /// Текст в текстовом поле
        /// </summary>
        public string? Text
        {
            get => (string)GetValue(TextProperty);
            set
            {
                SetValue(TextProperty, value);                
            }
        }
        static ConsoleTextBox()
        {
            TextProperty = DependencyProperty.Register(
                                nameof(Text),
                                typeof(string),
                                typeof(ConsoleTextBox),
                                new FrameworkPropertyMetadata(
                                        string.Empty,
                                        FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                        new PropertyChangedCallback(OnTextChanged)));
        }
        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is ConsoleTextBox control)
            {
                control.UpdateText();
            }
        }

        private void UpdateText()
        {
            textBox.Text = Text;
        }

        /// <summary>
        /// Конструктор класса ConsoleTextBox
        /// </summary>
        public ConsoleTextBox()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        /// <summary>
        /// Обработчик события загрузки окна
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _scrollBar = VisualTreeFinder.FindChild<ScrollBar>(textBox);
            if (_scrollBar != null)
            {
                _scrollBar.IsVisibleChanged += ScrollBar_IsVisibleChanged;
            }
        }

        /// <summary>
        /// Обработчик события изменения видимости скроллбара
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
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

        /// <summary>
        /// Обработчик события нажатия кнопки очистки текстового поля
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Text = string.Empty;
        }
    }
}
