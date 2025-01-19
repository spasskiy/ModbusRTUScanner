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
        public ICommand CalculateCRCCommand { get; }
        public ICommand SendRequestCommand { get; }


        public RequestPortManager PortManager { get; set; }        
        public RequestsWindowViewModel(ModbusDevice device) 
        { 
            PortManager = new RequestPortManager(device);
            CalculateCRCCommand = new RelayCommand<object>(CalculateCRC);
            SendRequestCommand = new RelayCommand<object>(WriteManualADU);
            MessageLogger = new ModbusMessageLogger();
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

        private ModbusMessageLogger _messageLogger;
        /// <summary>
        /// Хранитель логов
        /// </summary>
        public ModbusMessageLogger MessageLogger
        {
            get => _messageLogger;
            set => SetOptions(nameof(MessageLogger), ref _messageLogger, value);
        }

        /// <summary>
        /// Записать вручную собранное сообщение в текущий порт
        /// </summary>
        /// <param name="_"></param>
        private async void WriteManualADU(object _)
        {
            isNoOneExecuted = false;
            string responseView;
            
            OperationStatus = OperationStatus.Waiting;
            PortManager.Device.Address = 0;
            if (PortManager.Device.Address != 0 &&
                PortManager.PDUView != null &&
                PortManager.PDUView.Length > 0 &&
                PortManager.CRCView != null &&
                PortManager.CRCView.Length > 0)
                
            {
                PortManager.PDU = new HexConverter().ConvertToByteArray(PortManager.PDUView);
                PortManager.CRC = new HexConverter().ConvertToByteArray(PortManager.CRCView);
                if (PortManager.CRC is not null && PortManager.CRC.Length == 2 && PortManager.PDU is not null && PortManager.PDU.Length > 0)
                {
                    byte[] message = new byte[] { PortManager.Device.Address.Value }.Concat(PortManager.PDU).ToArray().Concat(PortManager.CRC).ToArray();

                    if (PortManager.SerialPort is not null)
                    {
                        PortManager.SetupPort();

                        string logMessage = PortManager.Device.Address.Value.ToString("X2") + " " + PortManager.PDUView + " " + PortManager.CRCView;
                        MessageLogger.logMessage(MessageType.Output, logMessage);

                        try
                        {
                            // Создание буфера для чтения данных
                            byte[] buffer = new byte[1024]; // Размер буфера для чтения данных

                            if (PortManager.SerialPort.IsOpen == false)
                                PortManager.SerialPort.Open();
                            PortManager.SerialPort.Write(message, 0, message.Length);
                            // Чтение данных из порта
                            await Task.Delay(PortManager.SerialPort.ReadTimeout / 2);
                            int bytesRead = PortManager.SerialPort.Read(buffer, 0, buffer.Length);
                            PortManager.SerialPort.Close();
                            // Создание нового массива байтов с размером, соответствующим количеству фактически прочитанных байт
                            byte[] responseBytes = new byte[bytesRead];
                            Array.Copy(buffer, responseBytes, bytesRead);

                            // Преобразование каждого байта в строку в формате hex и разделение пробелами
                            responseView = string.Join(" ", responseBytes.Select(b => b.ToString("X2")));
                            OperationStatus = OperationStatus.Success;
                            MessageLogger.logMessage(MessageType.Input, responseView);
                        }
                        catch (TimeoutException)
                        {
                            new MessageBoxCustom().ShowWarning("Превышено время ожидания ответа");
                        }
                        catch (Exception ex)
                        {
                            new MessageBoxCustom().ShowWarning(ex.Message + "\nПроверьте настройки порта");
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

