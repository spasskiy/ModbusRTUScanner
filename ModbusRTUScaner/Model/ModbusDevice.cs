using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    public class ModbusDevice
    {
        public int Address { get; init; }
        public string PortName { get; init; }
        public int Speed { get; init; }
        public int DataBits { get; init; }
        public StopBits StopBits { get; init; }
        public Parity Parity { get; init; }

        public ModbusDevice(int address, string portName, int speed, int dataBits, StopBits stopBits, Parity parity)
        {
            Address = address;
            PortName = portName;
            Speed = speed;
            DataBits = dataBits;
            StopBits = stopBits;
            Parity = parity;
        }
    }
}
