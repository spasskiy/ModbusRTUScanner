using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model.ModbusML
{
    /// <summary>
    /// Класс содержащий контейнеры со значениями полей
    /// элементов SerialPort
    /// </summary>
    public class SerialPortFieldsCollections
    {
        public ObservableCollection<int> DataBits { get; set; }
        public ObservableCollection<int> SpeedRate { get; set; }

        public ObservableCollection<Parity> Parities { get; set; }
        public ObservableCollection<StopBits> StopBits { get; set; }

        public SerialPortFieldsCollections()
        {
            DataBits = new ObservableCollection<int>(new int[] { 7, 8 });
            SpeedRate = new ObservableCollection<int>(new int[]
            {
                110,
                300,
                600,
                1200,
                2400,
                4800,
                9600,
                14400,
                19200,
                38400,
                56000,
                57600,
                115200,
                128000,
                230400,
                256000,
                460800,
                921600
            });
            Parities = new ObservableCollection<Parity>(new Parity[] { Parity.None, Parity.Odd, Parity.Even, Parity.Mark, Parity.Space });
            StopBits = new ObservableCollection<StopBits>(new StopBits[] { System.IO.Ports.StopBits.One, System.IO.Ports.StopBits.Two, System.IO.Ports.StopBits.OnePointFive, System.IO.Ports.StopBits.None });
        }

    }
}
