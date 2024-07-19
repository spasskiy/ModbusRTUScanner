using ModbusRTUScanner.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    public class ViewModelManager : INotifyPropertyChanged
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
            ScanRunLockObject = new();
        }

        public object ScanRunLockObject;


        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void SetOptions<T>(string Property, ref T variable, T value)
        {
            variable = value;
            OnPropertyChanged(new PropertyChangedEventArgs(Property));
        }
        #endregion
    }
}
