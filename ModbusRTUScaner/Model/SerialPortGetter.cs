using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    /// <summary>
    /// Возвращает первый серийный порт в системе
    /// </summary>
    internal class SerialPortGetter
    {
        /// <summary>
        /// Возвращает имя первого доступного последовательного порта в системе
        /// </summary>
        /// <returns>Имя первого последовательного порта или "None", если портов нет</returns>
        public string GetFirstPortName()
        {
            string result = "None";
            string[] ports = GetAllPortsNames();
            if (ports.Length > 0)
            {
                result = ports[0];
            }
            return result;
        }

        /// <summary>
        /// Возвращает имена всех доступных последовательных портов в системе
        /// </summary>
        /// <returns>Массив имен последовательных портов</returns>
        public string[] GetAllPortsNames()
        {
            return SerialPort.GetPortNames();
        }
    }
}
