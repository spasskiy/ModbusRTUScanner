using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace ModbusRTUScanner.Model
{
    /// <summary>
    /// Реквизиты подключения Modbus устройства
    /// </summary>
    internal class DeviceConnectionSettings
    {
        /// <summary>
        /// Имя порта для подключения
        /// </summary>
        public string? PortName { get; set; }

        /// <summary>
        /// Адрес устройства
        /// </summary>
        public ushort Address { get; set; }

        /// <summary>
        /// Скорость передачи данных (бод)
        /// </summary>
        public int BaudRate { get; set; }

        /// <summary>
        /// Чётность
        /// </summary>
        public Parity Parity { get; set; }

        /// <summary>
        /// Количество бит данных
        /// </summary>
        public int DataBits { get; set; }

        /// <summary>
        /// Количество стоп-бит
        /// </summary>
        public StopBits StopBits { get; set; }
    }
}
