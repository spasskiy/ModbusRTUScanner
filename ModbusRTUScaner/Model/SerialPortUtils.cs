using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    /// <summary>
    /// Утилиты для работы с последовательными портами
    /// </summary>
    public class SerialPortUtils
    {
        /// <summary>
        /// Возвращает массив доступных последовательных портов
        /// </summary>
        /// <returns>Массив объектов SerialPort, представляющих доступные последовательные порты</returns>
        public SerialPort[] GetAvailableSerialPorts()
        {
            string[] portNames = SerialPort.GetPortNames();
            SerialPort[] ports = new SerialPort[portNames.Length];

            for (int i = 0; i < portNames.Length; i++)
            {
                ports[i] = new SerialPort(portNames[i]);
            }

            return ports;
        }
    }
}
