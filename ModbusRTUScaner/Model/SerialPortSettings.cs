using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    /// <summary>
    /// Настройки последовательного порта
    /// </summary>
    public class SerialPortSettings : INotifyPropertyChanged
    {
        private string? _portName;
        /// <summary>
        /// Имя последовательного порта
        /// </summary>
        public string? PortName
        { 
            get => _portName;
            set => SetOptions(nameof(PortName), ref _portName, value);
        }

        private int _baudRate;
        /// <summary>
        /// Скорость передачи данных (бод)
        /// </summary>
        public int BaudRate
        { 
            get => _baudRate;
            set => SetOptions(nameof(BaudRate), ref _baudRate, value);
        }


        private int _dataBits;
        /// <summary>
        /// Количество бит данных
        /// </summary>
        public int DataBits
        {
            get => _dataBits;
            set => SetOptions(nameof(DataBits), ref _dataBits, value);
        }

        private StopBits _stopBits;
        /// <summary>
        /// Количество стоповых бит
        /// </summary>
        public StopBits StopBits
        {
            get => _stopBits;
            set => SetOptions(nameof(StopBits), ref _stopBits, value);
        }

        private Parity _parity;
        /// <summary>
        /// Четность
        /// </summary>
        public Parity Parity
        {
            get => _parity;
            set => SetOptions(nameof(Parity), ref _parity, value);
        }

        private int _writeTimeout;
        /// <summary>
        /// Таймаут записи (в миллисекундах)
        /// </summary>
        public int WriteTimeout
        {
            get => _writeTimeout;
            set => SetOptions(nameof(WriteTimeout), ref _writeTimeout, value);
        }

        private int _readTimeout;
        /// <summary>
        /// Таймаут чтения (в миллисекундах)
        /// </summary>
        public int ReadTimeout {
            get => _readTimeout;
            set => SetOptions(nameof(ReadTimeout), ref _readTimeout, value);
        }

        /// <summary>
        /// Конструктор по умолчанию, инициализирующий настройки с использованием первого доступного порта
        /// </summary>
        public SerialPortSettings()
        {
            PortName = new SerialPortGetter().GetFirstPortName();
            BaudRate = 9600;
            DataBits = 8;
            StopBits = StopBits.One;
            Parity = Parity.None;
            WriteTimeout = 1000;
            ReadTimeout = 1000;
        }

        /// <summary>
        /// Конструктор, инициализирующий настройки из существующего объекта SerialPort
        /// </summary>
        /// <param name="serialPort">Объект SerialPort, из которого загружаются настройки</param>
        public SerialPortSettings(SerialPort? serialPort)
        {
            LoadSettingsFromSerialPort(serialPort);
        }

        /// <summary>
        /// Загружает настройки из указанного объекта SerialPort
        /// </summary>
        /// <param name="serialPort">Объект SerialPort, из которого загружаются настройки</param>
        public void LoadSettingsFromSerialPort(SerialPort? serialPort)
        {
            if (serialPort is not null)
            {
                PortName = serialPort.PortName;
                BaudRate = serialPort.BaudRate;
                DataBits = serialPort.DataBits;
                StopBits = serialPort.StopBits;
                Parity = serialPort.Parity;
                WriteTimeout = serialPort.WriteTimeout == -1 ? 1000 : serialPort.WriteTimeout;
                ReadTimeout = serialPort.ReadTimeout == -1 ? 1000 : serialPort.ReadTimeout;
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
