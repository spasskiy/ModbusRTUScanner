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

        /// <summary>
        /// Коллекция доступных последовательных портов
        /// </summary>
        public ObservableCollection<SerialPort> Ports { get; init; }

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
        public SerialPortManager(ObservableCollection<SerialPort> ports, SerialPortSettings settings)
        {
            Ports = ports;
            SelectedPort = Ports.FirstOrDefault();
            _portSettings = settings;
            SerialPortSpeeds = new SerialPortSpeedNodeBuilder().Build();
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
        /// Возвращает объект SerialPort на основе текущих настроек
        /// </summary>
        /// <returns>Объект SerialPort или null, если имя порта "None"</returns>
        public SerialPort? GetPort()
        {
            if (_portSettings.PortName == "None")
                return null;
            return new SerialPort
            {
                PortName = _portSettings.PortName,
                BaudRate = _portSettings.BaudRate,
                DataBits = _portSettings.DataBits,
                StopBits = _portSettings.StopBits,
                Parity = _portSettings.Parity,
                WriteTimeout = _portSettings.WriteTimeout,
                ReadTimeout = _portSettings.ReadTimeout
            };
        }

        /// <summary>
        /// Применяет текущие настройки к указанному объекту SerialPort
        /// </summary>
        /// <param name="serialPort">Объект SerialPort, к которому применяются настройки</param>
        public void ApplySettingsToSerialPort(SerialPort serialPort)
        {
            if (serialPort.PortName == _portSettings.PortName)
            {
                serialPort.BaudRate = _portSettings.BaudRate;
                serialPort.DataBits = _portSettings.DataBits;
                serialPort.StopBits = _portSettings.StopBits;
                serialPort.Parity = _portSettings.Parity;
                serialPort.WriteTimeout = _portSettings.WriteTimeout;
                serialPort.ReadTimeout = _portSettings.ReadTimeout;
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
