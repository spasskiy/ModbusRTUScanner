using ModbusRTUScanner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ModbusRTUScanner.ViewModel
{
    /// <summary>
    ///  ViewModel для главного окна приложения.
    /// </summary>
    public partial class MainWindowViewModel
    {
        /// <summary>
        /// Команда для переключения темы.
        /// </summary>
        private readonly RelayCommand<object> _switchThemeCommand;

        /// <summary>
        /// Команда для поиска устройств.
        /// </summary>
        private readonly RelayCommand<object> _findDevicesCommand;

        /// <summary>
        /// Команда для отмены операции.
        /// </summary>
        private readonly RelayCommand<object> _cancelCommand;

        /// <summary>
        /// Менеджер ViewModel.
        /// </summary>
        public ViewModelManager ViewManager { get; init; }

        /// <summary>
        /// Конструктор для MainWindowViewModel.
        /// </summary>
        public MainWindowViewModel()
        {
            // Инициализация свойств
            ViewManager = new ViewModelManagerBuilder().Build();

            // Инициализация команд
            _switchThemeCommand = new RelayCommand<object>((_) => ViewManager.FlagsManager.IsNightModeOn = !ViewManager.FlagsManager.IsNightModeOn);
            _findDevicesCommand = new RelayCommand<object>(FindDevices);
            _cancelCommand = new RelayCommand<object>((_) => MessageBox.Show("CancelCommand"));
        }

        /// <summary>
        /// Команда для переключения темы.
        /// </summary>
        public ICommand SwitchThemeCommand => _switchThemeCommand;

        /// <summary>
        /// Команда для поиска устройств.
        /// </summary>
        public ICommand FindDevicesCommand => _findDevicesCommand;

        /// <summary>
        /// Команда для отмены операции.
        /// </summary>
        public ICommand CancelCommand => _cancelCommand;

        /// <summary>
        /// Метод для поиска устройств.
        /// </summary>
        /// <param name="obj">Параметр команды.</param>
        private async void FindDevices(object obj)
        {
            ViewManager.Console.AddNode("FindDevicesCommand");
            //await Task.Run(() =>
            //{
            //    lock (ViewManager.ScanRunLockObject)
            //    {
            //        try
            //        {
            //            ViewManager.FlagsManager.IsScanRun = true;
            //            MessageBox.Show("FindDevicesCommand");
            //            Task.Delay(5000).Wait();
            //        }
            //        finally
            //        {
            //            ViewManager.FlagsManager.IsScanRun = false;
            //        }
            //    }
            //});
        }
    }
}
