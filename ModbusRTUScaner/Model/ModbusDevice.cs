using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    public class ModbusDevice : IEquatable<ModbusDevice>
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

        public bool Equals(ModbusDevice other)
        {
            if (other == null)
                return false;

            return Address == other.Address &&
                   PortName == other.PortName &&
                   Speed == other.Speed &&
                   DataBits == other.DataBits &&
                   StopBits == other.StopBits &&
                   Parity == other.Parity;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ModbusDevice);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Address, PortName, Speed, DataBits, StopBits, Parity);
        }
    }
}
