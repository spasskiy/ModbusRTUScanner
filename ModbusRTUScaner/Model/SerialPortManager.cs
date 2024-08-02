using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    /// <summary>
    /// Управляет настройками и созданием последовательных портов
    /// </summary>
    public class SerialPortManager
    {
        /// <summary>
        /// Настройки последовательного порта
        /// </summary>
        private SerialPortSettings _settings;

        /// <summary>
        /// Конструктор класса SerialPortManager
        /// </summary>
        /// <param name="settings">Настройки последовательного порта</param>
        public SerialPortManager(SerialPortSettings settings)
        {
            _settings = settings;
        }

        /// <summary>
        /// Возвращает объект SerialPort на основе текущих настроек
        /// </summary>
        /// <returns>Объект SerialPort или null, если имя порта "None"</returns>
        public SerialPort? GetPort()
        {
            if (_settings.PortName == "None")
                return null;
            return new SerialPort
            {
                PortName = _settings.PortName,
                BaudRate = _settings.BaudRate,
                DataBits = _settings.DataBits,
                StopBits = _settings.StopBits,
                Parity = _settings.Parity,
                WriteTimeout = _settings.WriteTimeout,
                ReadTimeout = _settings.ReadTimeout
            };
        }

        /// <summary>
        /// Применяет текущие настройки к указанному объекту SerialPort
        /// </summary>
        /// <param name="serialPort">Объект SerialPort, к которому применяются настройки</param>
        public void ApplySettingsToSerialPort(SerialPort serialPort)
        {
            if (serialPort.PortName == _settings.PortName)
            {
                serialPort.BaudRate = _settings.BaudRate;
                serialPort.DataBits = _settings.DataBits;
                serialPort.StopBits = _settings.StopBits;
                serialPort.Parity = _settings.Parity;
                serialPort.WriteTimeout = _settings.WriteTimeout;
                serialPort.ReadTimeout = _settings.ReadTimeout;
            }
        }
    }
}
