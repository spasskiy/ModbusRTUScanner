using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    public class SerialPortManager
    {
        private SerialPortSettings _settings;

        public SerialPortManager(SerialPortSettings settings)
        {
            _settings = settings;
        }

        public SerialPort? GetPort()
        {
            if(_settings.PortName == "None")
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
