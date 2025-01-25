using ModbusRTUScanner.Model.RequestsWindowModel;
using ModbusRTUScanner.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ModbusRTUScanner.Model
{
    public class ScannerCommandManager
    {
        SerialPortManager _portManager;
        MainWindowViewModelFlags _flagsManager;
        ConsoleManager _console;
        DeviceManager _deviceManager;

        /// <summary>
        /// Токен отмены
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource = null!;
        public ICommand SwitchThemeCommand { get; }
        public ICommand FindDevicesCommand { get; }
        public ICommand DeleteDevicesCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SetDataBitsCommand { get; }
        public ICommand SetParityCommand { get; }
        public ICommand SetStopBitsCommand { get; }
        public ICommand UpdatePortsCommand { get; }
        public ICommand RequestCommand { get; }
        public ICommand CloseAppCommand { get; }
        public ICommand TestCommand { get; }

        public ScannerCommandManager(SerialPortManager portManager, MainWindowViewModelFlags flagsManager, ConsoleManager console, DeviceManager deviceManager)
        {
            _portManager = portManager;
            _flagsManager = flagsManager;
            _console = console;
            _deviceManager = deviceManager;

            SwitchThemeCommand = new RelayCommand<object>((_) => flagsManager.IsNightModeOn = !flagsManager.IsNightModeOn);
            FindDevicesCommand = new RelayCommand<object>(async (_) => await FindDevicesAsync());
            DeleteDevicesCommand = new RelayCommand<object>((_) => deviceManager.Devices.Remove(deviceManager.SelectedDevice));
            RequestCommand = new RelayCommand<object>((_) => new RequestsWindow(deviceManager.SelectedDevice).ShowDialog());
            CancelCommand = new RelayCommand<object>((_) => _cancellationTokenSource.Cancel());
            SetDataBitsCommand = new RelayCommand<object>(portManager.SetDataBits);
            SetParityCommand = new RelayCommand<object>(portManager.SetParity);
            SetStopBitsCommand = new RelayCommand<object>(portManager.SetStopBits);
            UpdatePortsCommand = new RelayCommand<object>(portManager.UpdatePorts);
            TestCommand = new RelayCommand<object>((_) => portManager.CurrentAddress++);
            CloseAppCommand = new RelayCommand<object>((_) => System.Windows.Application.Current.Shutdown());
        }

        private async Task FindDevicesAsync()
        {
            if(_portManager.SelectedPort == null)
            {
                new MessageBoxCustom().ShowWarning("Выберите порт");
            }
            else
            {
                await Task.Run(() =>
                {
                    var deviceFinder = new DeviceFinderBuilder()
                        .Build(_portManager, _flagsManager.IsScanRunSet, _console.AddNode, _deviceManager.Devices);
                    deviceFinder.FindDevices(GetCancelationTokken());
                });
            }
        }

        /// <summary>
        /// Возвращает токен отмены
        /// </summary>
        /// <returns></returns>
        private CancellationTokenSource GetCancelationTokken()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            return _cancellationTokenSource;
        }

        /// <summary>
        /// Отменяет токен 
        /// </summary>
        public void CancelCancelationTokken()
        {
            if (_cancellationTokenSource is not null)                
                _cancellationTokenSource.Cancel();
        }
    }
}
