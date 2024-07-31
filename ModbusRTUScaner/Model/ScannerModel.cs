using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    /// <summary>
    /// Класс модели сканера, содержащий информацию о найденных устройствах.
    /// </summary>
    internal class ScannerModel
    {
        /// <summary>
        /// Коллекция найденных устройств с настройками подключения.
        /// </summary>
        public ObservableCollection<DeviceConnectionSettings> FindedDevices { get; set; }

        /// <summary>
        /// Конструктор по умолчанию, инициализирующий коллекцию найденных устройств.
        /// </summary>
        public ScannerModel()
        {
            FindedDevices = new ObservableCollection<DeviceConnectionSettings>();
        }
    }
}
