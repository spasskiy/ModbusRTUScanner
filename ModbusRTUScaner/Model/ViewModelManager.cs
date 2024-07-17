using ModbusRTUScanner.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    public class ViewModelManager
    {
        public ObservableCollection<SerialPort> Ports { get; init; }        
        public SerialPortSettings PortSettings { get; init; }
        public MainWindowViewModelFlags FlagsManager { get; init; }
        public SerialPort? SelectedPort { get; set; }
        public ViewModelManager(ObservableCollection<SerialPort> ports, SerialPortSettings portSettings, MainWindowViewModelFlags flagsManager)
        {  
            Ports = ports;           
            PortSettings = portSettings;
            FlagsManager = flagsManager;
            SelectedPort = ports.FirstOrDefault();
        }

    }
}
