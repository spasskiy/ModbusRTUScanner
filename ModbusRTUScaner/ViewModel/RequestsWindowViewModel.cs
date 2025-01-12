using ModbusRTUScanner.Model;
using ModbusRTUScanner.Model.ModbusML;
using ModbusRTUScanner.Model.RequestsWindowModel;
using ModbusRTUScanner.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ModbusRTUScanner.ViewModel
{
    class RequestsWindowViewModel
    {
        public ICommand CalculateCRCCommand { get; }
        public ICommand SendRequestCommand { get; }


        public RequestPortManager PortManager { get; set; }        
        public RequestsWindowViewModel(ModbusDevice device) 
        { 
            PortManager = new RequestPortManager(device);
            CalculateCRCCommand = new RelayCommand<object>(CalculateCRC);
            SendRequestCommand = new RelayCommand<object>( (_)=> MessageBox.Show("Send Request"));
        }

        private void CalculateCRC(object _)
        {
            //Что-то делать имеет смысл только если вбит адрес
            if (PortManager.Device.Address != 0)
            {
                PortManager.PDU = new HexConverter().ConvertToByteArray(PortManager.PDUView);

                //И если в PDU что-то есть
                if (PortManager.PDU is not null && PortManager.PDU.Length > 0)
                {
                    byte[] ADU = new byte[] { (byte)PortManager.Device.Address }.Concat(PortManager.PDU).ToArray();
                    PortManager.CRC = new Crc16ModbusCalculator().CalculateCRC16(ADU);
                    PortManager.CRCView = new HexConverter().ConvertToString(PortManager.CRC);
                }
            }
        }
    }
}
