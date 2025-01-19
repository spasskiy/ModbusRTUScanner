using ModbusRTUScanner.Model;
using ModbusRTUScanner.Model.ModbusML;
using ModbusRTUScanner.Model.RequestsWindowModel;
using ModbusRTUScanner.View;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ModbusRTUScanner.ViewModel
{
    class RequestsWindowViewModel
    {
        private bool isNoOneExecuted = true;
        public ManualPacketContainer Container { get; set; }
        public ICommand CalculateCRCCommand { get; }
        public ICommand SendRequestCommand { get; }


        public RequestPortManager PortManager { get; set; }        
        public RequestsWindowViewModel(ModbusDevice device) 
        { 
            PortManager = new RequestPortManager(device);
            CalculateCRCCommand = new RelayCommand<object>(CalculateCRC);
            SendRequestCommand = new RelayCommand<object>(WriteManualADU);

            Container = new ManualPacketContainer(device);
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


        private OperationStatus operationStatus;
        /// <summary>
        /// Статус операции
        /// </summary>
        public OperationStatus OperationStatus
        {
            get => operationStatus;
            set
            {
                SetOptions(nameof(OperationStatus), ref operationStatus, value);
            }
        }

        /// <summary>
        /// Записать вручную собранное сообщение в текущий порт
        /// </summary>
        /// <param name="_"></param>
        private async void WriteManualADU(object _)
        {
            isNoOneExecuted = false;

            Container.ResponseView = "";
            OperationStatus = OperationStatus.Waiting;

            if (Container.Address.HasValue && 
                Container.PDUView != null && 
                Container.PDUView.Length > 0 && 
                Container.CRCView != null && 
                Container.CRCView.Length > 0)
            {
                Container.PDU = new HexConverter().ConvertToByteArray(Container.PDUView);
                Container.CRC = new HexConverter().ConvertToByteArray(Container.CRCView);
                if (Container.CRC is not null && Container.CRC.Length == 2 && Container.PDU is not null && Container.PDU.Length > 0)
                {
                    byte[] message = new byte[] { Container.Address.Value }.Concat(Container.PDU).ToArray().Concat(Container.CRC).ToArray();

                    if (Container.ManualPacketContainerPort is not null && Container.ManualPacketContainerPortSettings is not null)
                    {
                        Container.ManualPacketContainerPortSettings.DownloadSettingsToSerialPort(Container.ManualPacketContainerPort);

                        string logMessage = Container.Address.Value + " " + Container.PDUView + " " + Container.CRCView;


                        try
                        {
                            // Создание буфера для чтения данных
                            byte[] buffer = new byte[1024]; // Размер буфера для чтения данных

                            if (Container.ManualPacketContainerPort.IsOpen == false)
                                Container.ManualPacketContainerPort.Open();
                            Container.ManualPacketContainerPort.Write(message, 0, message.Length);
                            // Чтение данных из порта
                            await Task.Delay(Container.ManualPacketContainerPort.ReadTimeout / 2);
                            int bytesRead = Container.ManualPacketContainerPort.Read(buffer, 0, buffer.Length);
                            Container.ManualPacketContainerPort.Close();
                            // Создание нового массива байтов с размером, соответствующим количеству фактически прочитанных байт
                            byte[] responseBytes = new byte[bytesRead];
                            Array.Copy(buffer, responseBytes, bytesRead);

                            // Преобразование каждого байта в строку в формате hex и разделение пробелами
                            Container.ResponseView = string.Join(" ", responseBytes.Select(b => b.ToString("X2")));
                            OperationStatus = OperationStatus.Success;
                        }
                        catch (TimeoutException)
                        {
                            new MessageBoxCustom().ShowWarning("Превышено время ожидания ответа");
                        }
                        catch (Exception ex)
                        {
                            new MessageBoxCustom().ShowWarning(ex.Message);
                        }


                    }
                    else
                    {
                        new MessageBoxCustom().ShowWarning("Проверьте настройки порта");
                    }

                }
                else
                {
                    new MessageBoxCustom().ShowWarning("Неприемлемое сообщение");
                }
            }
            else
            {
                new MessageBoxCustom().ShowWarning("Недопустимое сообщение");
            }

            isNoOneExecuted = true;
            CommandManager.InvalidateRequerySuggested();
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
    public enum OperationStatus
    {
        Waiting,
        Success,
        Failure
    }
}

