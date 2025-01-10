using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    internal class DeviceFinderBuilder
    {

        public DeviceFinder Build(SerialPortManager portManager, Action<bool> swichIsScanRun, Action<string> addStringToConsole, ObservableCollection<ModbusDevice> devices)
        {
            SerialPort? port = portManager.GetCurrentPort();
            var speeds = ExtractSelectedSpeeds(portManager);
            if (port == null)
            {                
                throw new Exception("No port selected");
            }
            return new DeviceFinder(portManager, swichIsScanRun, addStringToConsole, devices);
        }

        private ObservableCollection<SerialPortSpeedNode> ExtractSelectedSpeeds(SerialPortManager portManager)
        {
            return new ObservableCollection<SerialPortSpeedNode>(portManager.SerialPortSpeeds.Where(x => x.IsSelected));
        }
    }
}
