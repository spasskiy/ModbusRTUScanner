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

        public string[] GetAllPortsNames()
        {
            return SerialPort.GetPortNames();
        }
    }
}
