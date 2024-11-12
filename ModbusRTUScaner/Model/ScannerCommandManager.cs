using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModbusRTUScanner.Model
{
    public class ScannerCommandManager
    {
        /// <summary>
        /// Токен отмены
        /// </summary>
        private CancellationTokenSource? _cancellationTokenSource;
        public ICommand SwitchThemeCommand { get; }
        public ICommand FindDevicesCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SetDataBitsCommand { get; }
        public ICommand SetParityCommand { get; }
        public ICommand SetStopBitsCommand { get; }
        public ICommand UpdatePortsCommand { get; }
        public ICommand CloseAppCommand { get; }
        public ICommand TestCommand { get; }

        public ScannerCommandManager(SerialPortManager portManager, MainWindowViewModelFlags flagsManager, ConsoleManager console, ObservableCollection<ModbusDevice> devices)
        {
            SwitchThemeCommand = new RelayCommand<object>((_) => flagsManager.IsNightModeOn = !flagsManager.IsNightModeOn);
            FindDevicesCommand = new RelayCommand<object>(async (_) => await Task.Run(() => new DeviceFinderBuilder().Build(portManager, flagsManager.IsScanRunSet, console.AddNode, devices).FindDevices(GetCancelationTokken())));
            CancelCommand = new RelayCommand<object>((_) => _cancellationTokenSource.Cancel());
            SetDataBitsCommand = new RelayCommand<object>(portManager.SetDataBits);
            SetParityCommand = new RelayCommand<object>(portManager.SetParity);
            SetStopBitsCommand = new RelayCommand<object>(portManager.SetStopBits);
            UpdatePortsCommand = new RelayCommand<object>(portManager.UpdatePorts);
            TestCommand = new RelayCommand<object>((_) => portManager.CurrentAddress++);
            CloseAppCommand = new RelayCommand<object>((_) => System.Windows.Application.Current.Shutdown());
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
