using System;
using System.Collections.Generic;
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

        public ScannerCommandManager(SerialPortManager portManager, MainWindowViewModelFlags flagsManager)
        {
            SwitchThemeCommand = new RelayCommand<object>((_) => flagsManager.IsNightModeOn = !flagsManager.IsNightModeOn);
            FindDevicesCommand = new RelayCommand<object>((_) => new DeviceFinder().Find());
            CancelCommand = new RelayCommand<object>((_) => System.Windows.MessageBox.Show("CancelCommand"));
            SetDataBitsCommand = new RelayCommand<object>(portManager.SetDataBits);
            SetParityCommand = new RelayCommand<object>(portManager.SetParity);
            SetStopBitsCommand = new RelayCommand<object>(portManager.SetStopBits);
            UpdatePortsCommand = new RelayCommand<object>(portManager.UpdatePorts);
        }

        /// <summary>
        /// Возвращает токен отмены
        /// </summary>
        /// <returns></returns>
        public CancellationTokenSource GetCancelationTokken()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            return _cancellationTokenSource;
        }

        /// <summary>
        /// Отменяет токен 
        /// </summary>
        public void CancelCancelationTokken()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
