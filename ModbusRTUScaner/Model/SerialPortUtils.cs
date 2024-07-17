using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    public class SerialPortUtils
    {
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
