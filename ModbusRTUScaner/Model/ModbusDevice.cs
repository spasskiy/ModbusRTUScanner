using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    public class ModbusDevice : IEquatable<ModbusDevice>, ICloneable, INotifyPropertyChanged
    {
        private byte? _address;
        public byte? Address
        {
            get => _address;
            set
            {
                if (value > 0 && value < 256)
                    SetOptions(nameof(Address), ref _address, value);
            }


        }
        public string PortName { get; init; }
        public int Speed { get; init; }
        public int DataBits { get; init; }
        public StopBits StopBits { get; init; }
        public Parity Parity { get; init; }

        public ModbusDevice(int address, string portName, int speed, int dataBits, StopBits stopBits, Parity parity)
        {
            Address = (byte)address;
            PortName = portName;
            Speed = speed;
            DataBits = dataBits;
            StopBits = stopBits;
            Parity = parity;
        }

#if DEBUG
        public ModbusDevice(string message)
        {
            if (message == "DEBUG")
            {
                Address = 10;
                PortName = "COM1";
                Speed = 115200;
                DataBits = 8;
                StopBits = StopBits.One;
                Parity = Parity.None;
            }
        }
#endif

        public bool Equals(ModbusDevice other)
        {
            if (other == null)
                return false;

            return Address == other.Address &&
                   PortName == other.PortName &&
                   Speed == other.Speed &&
                   DataBits == other.DataBits &&
                   StopBits == other.StopBits &&
                   Parity == other.Parity;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ModbusDevice);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Address, PortName, Speed, DataBits, StopBits, Parity);
        }
        public object Clone()
        {
            // Создаем новый объект ModbusDevice с теми же значениями свойств
            return new ModbusDevice(
                Address.Value,    // Копируем значение Address
                PortName,   // Копируем значение PortName
                Speed,      // Копируем значение Speed
                DataBits,   // Копируем значение DataBits
                StopBits,   // Копируем значение StopBits
                Parity      // Копируем значение Parity
            );
        }

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
