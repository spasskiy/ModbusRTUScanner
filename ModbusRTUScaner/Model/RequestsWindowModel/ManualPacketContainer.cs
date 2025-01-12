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
    internal class ManualPacketContainer : INotifyPropertyChanged
    {
        private string? _responseView;
        public string? ResponseView
        {
            get => _responseView;
            set => SetOptions(nameof(ResponseView), ref _responseView, value);
        }

        private byte[]? _response;
        public byte[]? Response
        {
            get => _response;
            set => SetOptions(nameof(Response), ref _response, value);
        }


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
        private string? _addressView;
        public string? AddressView
        {
            get => _addressView;
            set
            {
                if (value is not null)
                {
                    string trimmedValue = value.TrimStart().TrimStart(':').Replace("::", ":").Replace("  ", " ");
                    SetOptions(nameof(AddressView), ref _addressView, trimmedValue);
                    Address = (byte?)new HexConverter().ConvertHexToInt(trimmedValue);
                }
                else
                    SetOptions(nameof(AddressView), ref _addressView, value);



            }
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

        public ManualPacketContainer()
        {


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
