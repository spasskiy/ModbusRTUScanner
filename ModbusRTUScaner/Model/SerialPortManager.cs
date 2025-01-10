using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    /// <summary>
    /// Управляет настройками и созданием последовательных портов
    /// </summary>
    public class SerialPortManager : INotifyPropertyChanged
    {
        private ConsoleManager _scannerConsole;

        private int _currentAddress;
        public int CurrentAddress
        {
            get => _currentAddress;
            set => SetOptions(nameof(CurrentAddress), ref _currentAddress, value);
        }

        private int _modbusStartAddress;
        /// <summary>
        /// Начальный адрес поиска
        /// </summary>
        public int ModbusStartAddress
        {
            get => _modbusStartAddress;
            set
            {
                SetOptions(nameof(ModbusStartAddress), ref _modbusStartAddress, value);
                CurrentAddress = ModbusStartAddress;
            }                
        }
        private int _modbusEndAddress;
        /// <summary>
        /// Конечный адрес поиска
        /// </summary>
        public int ModbusEndAddress
        {
            get => _modbusEndAddress;
            set => SetOptions(nameof(ModbusEndAddress), ref _modbusEndAddress, value);
        }

        /// <summary>
        /// Коллекция доступных последовательных портов
        /// </summary>
        public ObservableCollection<SerialPort> Ports
        {
            get;
            init;
        }

        /// <summary>
        /// Настройки последовательного порта
        /// </summary>
        private SerialPortSettings _portSettings;
        public SerialPortSettings PortSettings
        {
            get => _portSettings;
            set => SetOptions(nameof(PortSettings), ref _portSettings, value);
        }

        public ObservableCollection<SerialPortSpeedNode> SerialPortSpeeds { get; init; }

        /// <summary>
        /// Конструктор класса SerialPortManager
        /// </summary>
        /// <param name="settings">Настройки последовательного порта</param>
        public SerialPortManager(ObservableCollection<SerialPort> ports, SerialPortSettings settings, ConsoleManager scannerConsole)
        {
            _scannerConsole = scannerConsole;
            Ports = ports;
            SelectedPort = Ports.FirstOrDefault();
            _portSettings = settings;
            SerialPortSpeeds = new SerialPortSpeedNodeBuilder().Build();
            ModbusStartAddress = 0;
            ModbusEndAddress = 255;
            CurrentAddress = ModbusStartAddress;
        }

        private SerialPort? _selectedPort;
        /// <summary>
        /// Выбранный последовательный порт
        /// </summary>
        public SerialPort? SelectedPort
        {
            get => _selectedPort;
            set
            {
                if (value is not null)
                {
                    SetOptions(nameof(SelectedPort), ref _selectedPort, value);
                }                
            }
        }

        /// <summary>
        /// Возвращает текущий выбранный порт
        /// </summary>
        /// <returns></returns>
        public SerialPort? GetCurrentPort()
        {
            ApplySettingsToSerialPort();
            return _selectedPort;
        }

        /// <summary>
        /// Применяет текущие настройки к указанному объекту SerialPort
        /// </summary>
        /// <param name="serialPort">Объект SerialPort, к которому применяются настройки</param>
        private void ApplySettingsToSerialPort()
        {
            if (_selectedPort is not null)
            {
                var selectedSpeed = SerialPortSpeeds.FirstOrDefault(x => x.IsSelected);
                _selectedPort.BaudRate = selectedSpeed?.PortSpeed ?? 9600;
                _selectedPort.DataBits = _portSettings.DataBits;
                _selectedPort.StopBits = _portSettings.StopBits;
                _selectedPort.Parity = _portSettings.Parity;
                _selectedPort.WriteTimeout = _portSettings.WriteTimeout;
                _selectedPort.ReadTimeout = _portSettings.ReadTimeout;
            }
        }

        /// <summary>
        /// Устанавливает значение DataBits
        /// </summary>
        /// <param name="param"></param>
        public void SetDataBits(object param)
        {
            if (param is string str && str != PortSettings.DataBits.ToString())
            {
                _scannerConsole.AddNode($"DataBits переключено на {param}");
                int.TryParse(str, out int dataBits);
                PortSettings.DataBits = dataBits;
            }
        }

        /// <summary>
        /// Устанавливает значение Parity
        /// </summary>
        /// <param name="param"></param>
        public void SetParity(object param)
        {
            if (param is string str)
            {
                switch (str)
                {
                    case "None":
                        if (PortSettings.Parity != Parity.None)
                        {
                            _scannerConsole.AddNode("Parity переключено на None");
                            PortSettings.Parity = Parity.None;
                        }
                        break;
                    case "Even":
                        if (PortSettings.Parity != Parity.Even)
                        {
                            _scannerConsole.AddNode("Parity переключено на Even");
                            PortSettings.Parity = Parity.Even;
                        }
                        break;
                    case "Odd":
                        if (PortSettings.Parity != Parity.Odd)
                        {
                            _scannerConsole.AddNode("Parity переключено на Odd");
                            PortSettings.Parity = Parity.Odd;
                        }
                        break;
                    default:
                        _scannerConsole.AddNode("Ошибка выбора Parity!");
                        break;
                }
            }
        }

        /// <summary>
        /// Устанавливает значение StopBits
        /// </summary>
        /// <param name="param"></param>
        public void SetStopBits(object param)
        {
            if (param is string str)
            {
                switch (str)
                {
                    case "1":
                        if (PortSettings.StopBits != StopBits.One)
                        {
                            _scannerConsole.AddNode("StopBits переключено на One");
                            PortSettings.StopBits = StopBits.One;
                        }
                        break;
                    case "2":
                        if (PortSettings.StopBits != StopBits.Two)
                        {
                            _scannerConsole.AddNode("StopBits переключено на Two");
                            PortSettings.StopBits = StopBits.Two;
                        }
                        break;
                    default:
                        _scannerConsole.AddNode("Ошибка выбора StopBits!");
                        break;
                }
            }
        }

        /// <summary>
        /// Обновляет список портов
        /// </summary>
        /// <param name="param"></param>
        public void UpdatePorts(object param)
        {
            Ports.Clear();
            foreach (var port in SerialPort.GetPortNames())
            {
                Ports.Add(new SerialPort(port));
            }
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
