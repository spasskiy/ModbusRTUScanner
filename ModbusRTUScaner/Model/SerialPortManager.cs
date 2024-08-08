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
            set => SetOptions(nameof(ModbusStartAddress), ref _modbusStartAddress, value);
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
            set => SetOptions(nameof(SelectedPort), ref _selectedPort, value);
        }



        /// <summary>
        /// Применяет текущие настройки к указанному объекту SerialPort
        /// </summary>
        /// <param name="serialPort">Объект SerialPort, к которому применяются настройки</param>
        public void ApplySettingsToSerialPort(SerialPort serialPort, int baudRate)
        {
            if (serialPort.PortName == _portSettings.PortName)
            {
                serialPort.BaudRate = baudRate;
                serialPort.DataBits = _portSettings.DataBits;
                serialPort.StopBits = _portSettings.StopBits;
                serialPort.Parity = _portSettings.Parity;
                serialPort.WriteTimeout = _portSettings.WriteTimeout;
                serialPort.ReadTimeout = _portSettings.ReadTimeout;
            }
        }

        public void SetDataBits(object param)
        {
            if (param is string str && str != PortSettings.DataBits.ToString())
            {
                _scannerConsole.AddNode($"DataBits переключено на {param}");
                int.TryParse(str, out int dataBits);
                PortSettings.DataBits = dataBits;
            }
        }

        public void SetParity(object param)
        {
            if (param is string str)
            {
                switch (str)
                {
                    case "None":
                        if(PortSettings.Parity != Parity.None)
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

        public void SetStopBits(object param)
        {
            if (param is string str)
            {
                switch (str)
                {
                    case "1":
                        if(PortSettings.StopBits != StopBits.One)
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
