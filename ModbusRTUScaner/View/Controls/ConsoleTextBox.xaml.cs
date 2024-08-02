using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using ModbusRTUScanner.View.Controls.Utility;
using static System.Net.Mime.MediaTypeNames;


namespace ModbusRTUScanner.View.Controls
{
    public partial class ConsoleTextBox : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// Скроллбар текстового поля
        /// </summary>
        private ScrollBar? _scrollBar;

        public static readonly DependencyProperty TextProperty;
        
        //=DependencyProperty.Register("Text", typeof(string), typeof(ConsoleTextBox), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextPropertyChanged, CoerceText));

        private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            string newPropertyValue = (string)e.NewValue;
            ConsoleTextBox instance = (ConsoleTextBox)d;
        }

        private static object CoerceText(DependencyObject d, object baseValue)
        {
            return baseValue;
        }

        private string? _text;
        /// <summary>
        /// Текст в текстовом поле
        /// </summary>
        public string? Text
        {
            get => (string)GetValue(TextProperty);
            set 
            {
                SetValue(TextProperty, value);
                SetOptions(nameof(Text), ref _text, value);
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
                                new PropertyChangedCallback(OnTextChanged)));
        }
        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //...............................
        }

        /// <summary>
        /// Конструктор класса ConsoleTextBox
        /// </summary>
        public ConsoleTextBox()
        {
            DataContext = this;
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

        #region INotifyPropertyChanged
        /// <summary>
        /// Событие, возникающее при изменении свойства
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Вызывает событие PropertyChanged
        /// </summary>
        /// <param name="e">Аргументы события</param>
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Устанавливает значение свойства и вызывает событие PropertyChanged
        /// </summary>
        /// <typeparam name="T">Тип свойства</typeparam>
        /// <param name="property">Имя свойства</param>
        /// <param name="variable">Ссылка на переменную свойства</param>
        /// <param name="value">Новое значение свойства</param>
        protected void SetOptions<T>(string property, ref T variable, T value)
        {
            variable = value;
            OnPropertyChanged(new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
