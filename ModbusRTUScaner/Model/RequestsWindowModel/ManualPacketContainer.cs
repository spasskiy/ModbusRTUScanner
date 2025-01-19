using System;
using System.ComponentModel;
using System.IO.Ports;

namespace ModbusRTUScanner.Model.RequestsWindowModel
{
    internal class ManualPacketContainer : INotifyPropertyChanged
    {
        private SerialPort? _manualPacketContainerPort;
        public SerialPort? ManualPacketContainerPort
        {
            get => _manualPacketContainerPort;
            set
            {
                SetOptions(nameof(ManualPacketContainerPort), ref _manualPacketContainerPort, value);
                if (ManualPacketContainerPort is not null)
                {
                    if (ManualPacketContainerPortSettings is null)
                        ManualPacketContainerPortSettings = new SerialPortSettings(ManualPacketContainerPort);
                    else
                        ManualPacketContainerPortSettings.LoadSettingsFromSerialPort(ManualPacketContainerPort);
                }
            }
        }

        private SerialPortSettings? _manualPacketContainerPortSettings;
        public SerialPortSettings? ManualPacketContainerPortSettings
        {
            get => _manualPacketContainerPortSettings;
            set => SetOptions(nameof(ManualPacketContainerPortSettings), ref _manualPacketContainerPortSettings, value);
        }

        private byte? _address;
        public byte? Address
        {
            get => _address;
            set => SetOptions(nameof(Address), ref _address, value);
        }

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
                {
                    SetOptions(nameof(CRCView), ref _crcView, value);
                }
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
                {
                    SetOptions(nameof(PDUView), ref _pduView, value);
                }
            }
        }

        public ManualPacketContainer(ModbusDevice device)
        {
            if (device == null)
                throw new ArgumentNullException(nameof(device));

            // Заполняем свойства на основе ModbusDevice
            Address = (byte)device.Address;

            // Создаем и настраиваем SerialPort
            ManualPacketContainerPort = new SerialPort(device.PortName, device.Speed, device.Parity, device.DataBits, device.StopBits);

            // Инициализируем настройки порта
            ManualPacketContainerPortSettings = new SerialPortSettings(ManualPacketContainerPort);
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