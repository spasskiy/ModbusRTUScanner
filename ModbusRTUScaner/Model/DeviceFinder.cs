using ModbusRTUScanner.Model.ModbusML;
using ModbusRTUScanner.Model.RequestsWindowModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace ModbusRTUScanner.Model
{
    /// <summary>
    /// Класс для поиска устройств
    /// </summary>
    public class DeviceFinder
    {        
        private SerialPortManager _portManager;
        private Action<bool> _swichIsScanRun;
        private Action<string> _addStringToConsole;
        private ObservableCollection<ModbusDevice> _devices;

        public DeviceFinder(SerialPortManager portManager, Action<bool> swichIsScanRun, Action<string> addStringToConsole, ObservableCollection<ModbusDevice> devices)
        {
            _portManager = portManager;
            _swichIsScanRun = swichIsScanRun;
            _addStringToConsole = addStringToConsole;
            _devices = devices;
        }



        public void FindDevices(CancellationTokenSource cancellationToken)
        {
            if (!CheckScanRedines(cancellationToken))
                return;

            try
            {
                _swichIsScanRun(true);
                _portManager.CurrentAddress = _portManager.ModbusStartAddress;
                _addStringToConsole("Сканирование запущено");
               
                foreach (var speed in _portManager.SerialPortSpeeds.Reverse().Where(x => x.IsSelected).Select(x => x.PortSpeed))
                {
                    if (speed is null)
                        continue;
                    _portManager.CurrentAddress = _portManager.ModbusStartAddress;
                    _addStringToConsole($"Сканируем диапазон {_portManager.ModbusStartAddress} - {_portManager.ModbusEndAddress} на скорости {speed}");
                    _portManager.SelectedPort.BaudRate = (int)speed;

                    ModbusML.ModbusML modbus = new ModbusRTUEDFactory().CreateModbusML(_portManager.SelectedPort, _portManager.ModbusStartAddress);

                    for (int i = _portManager.ModbusStartAddress; i <= _portManager.ModbusEndAddress; i++)
                    {
                        if (cancellationToken.IsCancellationRequested)
                            break;
                        modbus.SetAddress(i);

                        ushort? response = modbus.ReadSingleRegister(RegisterType.HoldingRegister, 0);
                        if (response is not null)
                        {
                            _addStringToConsole($"Получне ответ от устройства. Адрес {i}");
                            ModbusDevice device = new ModbusDevice(i, _portManager.SelectedPort.PortName, speed.Value, _portManager.SelectedPort.DataBits, _portManager.SelectedPort.StopBits, _portManager.SelectedPort.Parity, _portManager.SelectedPort.ReadTimeout, _portManager.SelectedPort.WriteTimeout);

                            bool isDuplicate = _devices.Any(d => d.Equals(device));

                            if (!isDuplicate)
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    _devices.Add(device);
                                });
                            }
                        }
                        _portManager.CurrentAddress++;
                    }
                    if (cancellationToken.IsCancellationRequested)
                        break;
                }
                _portManager.CurrentAddress = _portManager.ModbusStartAddress;
                if (cancellationToken.IsCancellationRequested)
                    _addStringToConsole("Сканирование отменено");
                else
                    _addStringToConsole("Сканирование окончено");             
            }
            catch (Exception ex)
            {
                new MessageBoxCustom().ShowWarning("Порт не поддерживает заданную скорость");
            }
            finally
            {
                _portManager.SelectedPort.Close();
                _swichIsScanRun(false);
            }
        }

        private bool CheckScanRedines(CancellationTokenSource cancellationToken)
        {
            if (_portManager.SerialPortSpeeds.Count(x => x.IsSelected) == 0)
            {
                _addStringToConsole("Вы не задали ни одной скорости для поиска.");
                return false;
            }
            try
            {
                if(!_portManager.SelectedPort.IsOpen)
                    _portManager.SelectedPort.Open();
            }
            catch (Exception ex)
            {
                cancellationToken.Cancel();
                _addStringToConsole($"Не удалось открыть {_portManager.SelectedPort.PortName}");
                return false;
            }
            return true;
        }
    }
}
