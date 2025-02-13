using ModbusRTUScanner.Model.ModbusML;
using ModbusRTUScanner.View.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model.RequestsWindowModel
{
    public class RequestPortManager : INotifyPropertyChanged
    {
        // Коллекция для хранения имен COM-портов
        public ObservableCollection<string> AvailablePorts { get; set; }
        //Выбранный порт
        public SerialPort SerialPort { get; set; }

        public SerialPortSettings PortSettings { get; set; }


        public SerialPortFieldsCollections SerialPortFieldsCollections { get; set; }

        //Переданное устройство
        public ModbusDevice Device { get; set; }


        private byte[]? _crc;
        public byte[]? CRC
        {
            get => _crc;
            set => SetOptions(nameof(CRC), ref _crc, value);
        }

        private string? _crcView;
        public string? CRCView
        {
            get => _crcView;
            set
            {
                if (value is not null)
                {
                    string trimmedValue = value.TrimStart().TrimStart(':').Replace("::", ":").Replace("  ", " ");
                    SetOptions(nameof(CRCView), ref _crcView, trimmedValue);

                    int? convertedValue = new HexConverter().ConvertHexToInt(trimmedValue);
                    if (convertedValue.HasValue)
                    {
                        CRC = BitConverter.GetBytes(convertedValue.Value);
                    }
                }
                else
                    SetOptions(nameof(CRCView), ref _crcView, value);

            }
        }

        private byte[]? _pdu;
        public byte[]? PDU
        {
            get => _pdu;
            set => SetOptions(nameof(PDU), ref _pdu, value);
        }

        private string? _pduView;
        public string? PDUView
        {
            get => _pduView;
            set
            {
                if (value is not null)
                {
                    string trimmedValue = value.TrimStart().TrimStart(':').Replace("::", ":").Replace("  ", " ");
                    SetOptions(nameof(PDUView), ref _pduView, trimmedValue);
                }
                else
                    SetOptions(nameof(PDUView), ref _pduView, value);
            }
        }





        public RequestPortManager(ModbusDevice device) 
        {
            AvailablePorts = new ObservableCollection<string>();
            RefreshAvailablePorts(); // Заполняем список портов при создании объекта
            Device = (ModbusDevice)device.Clone();
            SerialPort = new SerialPortBuilder().Build(device);
            SerialPortFieldsCollections = new();
            PortSettings = new(SerialPort);
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

        //Применить настройки устройства к порту
        public void SetupPort()
        {
            if (SerialPort == null)
                throw new InvalidOperationException("SerialPort не инициализирован.");
            if (Device == null)
                throw new InvalidOperationException("Device не инициализирован.");

            // Закрываем порт, если он открыт
            if (SerialPort.IsOpen)
            {
                SerialPort.Close();
            }

            // Применяем настройки из Device к SerialPort
            SerialPort.PortName = Device.PortName;
            SerialPort.BaudRate = Device.Speed;
            SerialPort.DataBits = Device.DataBits;
            SerialPort.StopBits = Device.StopBits;
            SerialPort.Parity = Device.Parity;

            // Логирование (опционально)
            Console.WriteLine($"Порт {SerialPort.PortName} настроен: BaudRate={SerialPort.BaudRate}, DataBits={SerialPort.DataBits}, StopBits={SerialPort.StopBits}, Parity={SerialPort.Parity}");
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
