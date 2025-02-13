using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    public class SerialPortBuilder
    {
        // Метод для создания и настройки SerialPort на основе ModbusDevice
        public SerialPort Build(ModbusDevice device)
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device), "Устройство не может быть null.");
            }

            // Создаем объект SerialPort
            var serialPort = new SerialPort
            {
                PortName = device.PortName,   // Имя порта
                BaudRate = device.Speed,      // Скорость передачи данных
                DataBits = device.DataBits,   // Количество бит данных
                StopBits = device.StopBits,   // Стоповые биты
                Parity = device.Parity,        // Четность                
                WriteTimeout = device.WriteTimeout, //Тайминг записи
                ReadTimeout = device.ReadTimeout //Тайминг чтения
            };

            return serialPort;
        }
    }
}
