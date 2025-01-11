using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    public class DeviceManager
    {
        public ObservableCollection<ModbusDevice> Devices { get; init; }

        
        public ModbusDevice SelectedDevice { get; set; }
        public DeviceManager()
        {
            Devices = new ObservableCollection<ModbusDevice>();
        }

        
    }
}
