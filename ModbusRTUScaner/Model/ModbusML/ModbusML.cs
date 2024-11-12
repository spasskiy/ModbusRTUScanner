using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model.ModbusML
{
    public abstract class ModbusML : IDisposable
    {
        protected SerialPort _port;
        protected int _deviceAddress;

        public ModbusML(SerialPort port, int deviceAddress)
        {
            _port = port;
            _deviceAddress = deviceAddress;
        }

        public abstract ushort? ReadSingleRegister(RegisterType type, int address);
        public abstract bool WriteSingleRegister(RegisterType type, int address, int value);
        public abstract ushort[]? ReadMultipleRegisters(RegisterType type, int startAddress, int count);
        public abstract bool WriteMultipleRegisters(RegisterType type, int startAddress, ushort[] values);
        public abstract void SetAddress(int address);

        public void Dispose()
        {
            _port.Close();
        }
    }
}
