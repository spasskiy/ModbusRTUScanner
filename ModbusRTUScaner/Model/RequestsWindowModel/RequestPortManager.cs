using ModbusRTUScanner.Model.ModbusML;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model.RequestsWindowModel
{
    public class RequestPortManager
    {
        // Коллекция для хранения имен COM-портов
        public ObservableCollection<string> AvailablePorts { get; set; }
        //Выбранный порт
        public SerialPort SerialPort { get; set; }

        public SerialPortFieldsCollections SerialPortFieldsCollections { get; set; }

        //Переданное устройство
        public ModbusDevice Device { get; set; }

        public RequestPortManager(ModbusDevice device) 
        {
            AvailablePorts = new ObservableCollection<string>();
            RefreshAvailablePorts(); // Заполняем список портов при создании объекта
            Device = (ModbusDevice)device.Clone();
            SerialPort = new SerialPortBuilder().Build(device);
            SerialPortFieldsCollections = new();
        }

        // Метод для обновления списка доступных COM-портов
        public void RefreshAvailablePorts()
        {
            AvailablePorts.Clear(); // Очищаем текущий список

            // Получаем список доступных портов
            string[] ports = SerialPort.GetPortNames();

            // Добавляем порты в коллекцию
            foreach (string port in ports)
            {
                AvailablePorts.Add(port);
            }
        }
    }
}
