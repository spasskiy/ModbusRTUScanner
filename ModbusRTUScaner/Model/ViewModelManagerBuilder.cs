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
    internal class ViewModelManagerBuilder
    {
        public ViewModelManager Build()
        {
            ObservableCollection<SerialPort>  ports = new ObservableCollection<SerialPort>(new SerialPortUtils().GetAvailableSerialPorts());
            SerialPortSettings portSettings = new SerialPortSettings(ports.FirstOrDefault());            
            MainWindowViewModelFlags flagsManager = new MainWindowViewModelFlags();

            //Привязка обработчиков смены темы оформления
            new AppThemeManager(flagsManager);

            return new ViewModelManager(ports, portSettings, flagsManager);
        }
    }
}
