using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    internal class SerialPortSpeedNodeBuilder
    {
        public ObservableCollection<SerialPortSpeedNode> Build() 
        {
            return new ObservableCollection<SerialPortSpeedNode>() 
            {
                new SerialPortSpeedNode(300),
                new SerialPortSpeedNode(600),
                new SerialPortSpeedNode(1200),
                new SerialPortSpeedNode(2400),
                new SerialPortSpeedNode(4800),
                new SerialPortSpeedNode(7200),
                new SerialPortSpeedNode(9600),
                new SerialPortSpeedNode(14400),
                new SerialPortSpeedNode(19200),
                new SerialPortSpeedNode(38400),
                new SerialPortSpeedNode(56000),
                new SerialPortSpeedNode(57600),
                new SerialPortSpeedNode(115200),
                new SerialPortSpeedNode(128000),
                new SerialPortSpeedNode(144000),
                new SerialPortSpeedNode(256000),
                new SerialPortSpeedNode()
            };
        }
    }
}
