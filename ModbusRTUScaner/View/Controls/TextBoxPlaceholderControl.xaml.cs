using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ModbusRTUScanner.View.Controls
{
    /// <summary>
    /// Контрол для отображения TextBox с подсказкой
    /// </summary>
    public partial class TextBoxPlaceholderControl : UserControl, INotifyPropertyChanged
    {
        /// <summary>
        /// DependencyProperty для Text
        /// </summary>
        public static readonly DependencyProperty TextProperty;

        static TextBoxPlaceholderControl()
        {
            TextProperty = DependencyProperty.Register(
                nameof(Text),
                typeof(string),
                typeof(TextBoxPlaceholderControl),
                new FrameworkPropertyMetadata(
                        string.Empty,
                        FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));                
        }



        /// <summary>
        /// Текст в TextBox
        /// </summary>
        private string? _text;
        public string? Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
                SetOptions(nameof(Text), ref _text, value);
            }
        }

        /// <summary>
        /// Маркер для блокировки не числового ввода
        /// </summary>
        private bool _isDigitalOnly;
        public bool IsDigitalOnly
        {
            get => _isDigitalOnly;
            set
            {
                SetOptions(nameof(IsDigitalOnly), ref _isDigitalOnly, value);
            }
        }

        /// <summary>
        /// DependencyProperty для свойства Background
        /// </summary>
        public static readonly new DependencyProperty BackgroundProperty =
            DependencyProperty.Register(
                nameof(Background),
                typeof(Brush),
                typeof(TextBoxPlaceholderControl),
                new PropertyMetadata(Brushes.White, OnBackgroundChanged));

        /// <summary>
        /// Фон TextBox
        /// </summary>
        public new Brush Background
        {
            get { return (Brush)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }

        /// <summary>
        /// Обработчик изменения свойства Background
        /// </summary>
        /// <param name="d">DependencyObject</param>
        /// <param name="e">Аргументы события</param>
        private static void OnBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as TextBoxPlaceholderControl;
            if (control != null)
            {
                control.UpdateBackground();
            }
        }

        /// <summary>
        /// Обновление фона TextBox
        /// </summary>
        private void UpdateBackground()
        {
            if (MainGrid.Children.OfType<TextBox>().FirstOrDefault() is TextBox textBox)
            {
                textBox.Background = Background;
            }
        }

        /// <summary>
        /// DependencyProperty для свойства Foreground
        /// </summary>
        public static readonly new DependencyProperty ForegroundProperty =
            DependencyProperty.Register(
                nameof(Foreground),
                typeof(Brush),
                typeof(TextBoxPlaceholderControl),
                new PropertyMetadata(Brushes.Black, OnForegroundChanged));

        /// <summary>
        /// Цвет текста TextBox
        /// </summary>
        public new Brush Foreground
        {
            get { return (Brush)GetValue(ForegroundProperty); }
            set { SetValue(ForegroundProperty, value); }
        }

        /// <summary>
        /// Обработчик изменения свойства Foreground
        /// </summary>
        /// <param name="d">DependencyObject</param>
        /// <param name="e">Аргументы события</param>
        private static void OnForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as TextBoxPlaceholderControl;
            if (control != null)
            {
                control.UpdateForeground();
            }
        }

        /// <summary>
        /// Обновление цвета текста TextBox
        /// </summary>
        private void UpdateForeground()
        {
            if (MainGrid.Children.OfType<TextBox>().FirstOrDefault() is TextBox textBox)
            {
                textBox.Foreground = Foreground;
            }
        }

        /// <summary>
        /// DependencyProperty для свойства PlaceholderText
        /// </summary>
        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register(
                nameof(PlaceholderText),
                typeof(string),
                typeof(TextBoxPlaceholderControl),
                new PropertyMetadata(string.Empty, OnPlaceholderTextChanged));

        /// <summary>
        /// Текст подсказки
        /// </summary>
        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }

        /// <summary>
        /// Обработчик изменения свойства PlaceholderText
        /// </summary>
        /// <param name="d">DependencyObject</param>
        /// <param name="e">Аргументы события</param>
        private static void OnPlaceholderTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as TextBoxPlaceholderControl;
            if (control != null)
            {
                control.UpdatePlaceholderText();
            }
        }

        /// <summary>
        /// Обновление текста подсказки
        /// </summary>
        private void UpdatePlaceholderText()
        {
            if (MainGrid.Children.OfType<TextBlock>().FirstOrDefault() is TextBlock textBlock)
            {
                textBlock.Text = PlaceholderText;
            }
        }

        /// <summary>
        /// DependencyProperty для свойства PlaceholderColor
        /// </summary>
        public static readonly DependencyProperty PlaceholderColorProperty =
            DependencyProperty.Register(
                nameof(PlaceholderColor),
                typeof(Brush),
                typeof(TextBoxPlaceholderControl),
                new PropertyMetadata(Brushes.Gray, OnPlaceholderColorChanged));

        /// <summary>
        /// Цвет текста подсказки
        /// </summary>
        public Brush PlaceholderColor
        {
            get { return (Brush)GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }

        /// <summary>
        /// Обработчик изменения свойства PlaceholderColor
        /// </summary>
        /// <param name="d">DependencyObject</param>
        /// <param name="e">Аргументы события</param>
        private static void OnPlaceholderColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as TextBoxPlaceholderControl;
            if (control != null)
            {
                control.UpdatePlaceholderColor();
            }
        }

        /// <summary>
        /// Обновление цвета текста подсказки
        /// </summary>
        private void UpdatePlaceholderColor()
        {
            if (MainGrid.Children.OfType<TextBlock>().FirstOrDefault() is TextBlock textBlock)
            {
                textBlock.Foreground = PlaceholderColor;
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public TextBoxPlaceholderControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        /// <summary>
        /// Обработчик события MouseDown для TextBlock
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            myTextBox.Focus();
            e.Handled = true; // Отмечаем событие как обработанное, чтобы оно не всплывало дальше
        }

        /// <summary>
        /// Обработчик события PreviewTextInput для TextBox проверяющий, что введено число
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            base.OnPreviewTextInput(e);

            // Проверяем, включено ли ограничение на ввод только цифр и является ли вводимый символ цифрой
            if (IsDigitalOnly && !char.IsDigit(e.Text, e.Text.Length - 1))
            {
                // Если нет, то отменяем ввод
                e.Handled = true;
            }
        }

        #region INotifyPropertyChanged
        /// <summary>
        /// Событие изменения свойства
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Вызов события изменения свойства
        /// </summary>
        /// <param name="e">Аргументы события</param>
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Установка значения свойства и вызов события изменения свойства
        /// </summary>
        /// <typeparam name="T">Тип свойства</typeparam>
        /// <param name="Property">Имя свойства</param>
        /// <param name="variable">Переменная свойства</param>
        /// <param name="value">Новое значение</param>
        protected void SetOptions<T>(string Property, ref T variable, T value)
        {
            variable = value;
            OnPropertyChanged(new PropertyChangedEventArgs(Property));
        }
        #endregion
    }
}
