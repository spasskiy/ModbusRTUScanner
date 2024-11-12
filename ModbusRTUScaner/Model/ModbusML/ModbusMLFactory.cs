using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model.ModbusML
{

    public abstract class ModbusMLFactory
    {
        public abstract ModbusML? CreateModbusML(SerialPort port, int deviceAddress);
    }

    public class ModbusRTUEDFactory : ModbusMLFactory
    {
        /// <summary>
        /// Создает экземпляр объекта ModbusED на основе указанного порта SerialPort и адреса устройства.
        /// Возвращает объект ModbusED, если порт не равен null и адрес устройства находится в диапазоне [1, 255].
        /// Если порт равен null или адрес устройства вне диапазона [1, 255], возвращает null.
        /// </summary>
        /// <param name="port">Последовательный порт SerialPort для связи с устройством.</param>
        /// <param name="deviceAddress">Адрес устройства в диапазоне [1, 255].</param>
        /// <returns>Экземпляр объекта ModbusED или null в случае некорректных параметров.</returns>
        public override ModbusML? CreateModbusML(SerialPort port, int deviceAddress)
        {
            string[] availablePorts = SerialPort.GetPortNames();
            if (port is not null && availablePorts.Contains(port.PortName) && deviceAddress >= 0 && deviceAddress < 256)
            {
                return new ModbusRTUML(port, deviceAddress);
            }
            else
            {
                return null;
            }
        }
    }
}
