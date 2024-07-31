using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    /// <summary>
    /// Класс для хранения настроек сканера.
    /// </summary>
    internal class ScannerSettings
    {
        /// <summary>
        /// Список скоростей последовательного порта.
        /// </summary>
        public List<long> SerialPortSpeeds { get; set; }

        /// <summary>
        /// Начальный адрес для сканирования.
        /// </summary>
        public short StartAddress { get; set; }

        /// <summary>
        /// Конечный адрес для сканирования.
        /// </summary>
        public short EndAddress { get; set; }

        /// <summary>
        /// Конструктор по умолчанию, инициализирующий настройки сканера.
        /// </summary>
        public ScannerSettings()
        {
            SerialPortSpeeds = new List<long>();
            StartAddress = 0;
            EndAddress = 255;
        }
    }
}
