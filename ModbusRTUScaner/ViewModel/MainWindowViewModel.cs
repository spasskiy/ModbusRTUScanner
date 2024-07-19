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
    public partial class MainWindowViewModel
    {        
        private readonly RelayCommand<object> _switchThemeCommand;
        private readonly RelayCommand<object> _findDevicesCommand;
        private readonly RelayCommand<object> _cancelCommand;
        public ViewModelManager ViewManager {get; init;}

        public MainWindowViewModel()
        {
            //Инициализация свойств
            ViewManager = new ViewModelManagerBuilder().Build();

            //Инициализация команд
            _switchThemeCommand = new RelayCommand<object>((_) => ViewManager.FlagsManager.IsNightModeOn = !ViewManager.FlagsManager.IsNightModeOn);
            _findDevicesCommand = new RelayCommand<object>(FindDevices);
            _cancelCommand = new RelayCommand<object>((_) => MessageBox.Show("CancelCommand"));

        }               


        public ICommand SwitchThemeCommand => _switchThemeCommand;
        public ICommand FindDevicesCommand => _findDevicesCommand;
        public ICommand CancelCommand => _cancelCommand;

        private async void FindDevices(object obj)
        {
            await Task.Run(() =>
            {
                lock (ViewManager.ScanRunLockObject)
                {
                    try
                    {
                        ViewManager.FlagsManager.IsScanRun = true;
                        MessageBox.Show("FindDevicesCommand");
                        Task.Delay(5000).Wait();
                    }
                    finally
                    {
                        ViewManager.FlagsManager.IsScanRun = false;
                    }
                }

            });
        }

    }
}
