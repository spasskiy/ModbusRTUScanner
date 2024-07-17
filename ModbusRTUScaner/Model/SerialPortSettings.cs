using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    public class SerialPortSettings
    {
        public string? PortName { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public StopBits StopBits { get; set; }
        public Parity Parity { get; set; }
        public int WriteTimeout { get; set; }
        public int ReadTimeout { get; set; }

        public SerialPortSettings()
        {
            PortName = new SerialPortGetter().GetFirstPortName();
            BaudRate = 9600;
            DataBits = 8;
            StopBits = StopBits.One;
            Parity = Parity.None;
            WriteTimeout = 1000;
            ReadTimeout = 1000;
        }

        public SerialPortSettings(SerialPort? serialPort)
        {
            LoadSettingsFromSerialPort(serialPort);
        }

        public void LoadSettingsFromSerialPort(SerialPort? serialPort)
        {
            if(serialPort is not null)
            {
                PortName = serialPort.PortName;
                BaudRate = serialPort.BaudRate;
                DataBits = serialPort.DataBits;
                StopBits = serialPort.StopBits;
                Parity = serialPort.Parity;
                WriteTimeout = serialPort.WriteTimeout;
                ReadTimeout = serialPort.ReadTimeout;
            }
        }
    }
}
