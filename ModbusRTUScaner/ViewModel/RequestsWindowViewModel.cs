﻿using ModbusRTUScanner.Model;
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
            SendRequestCommand = new RelayCommand<object>(async _ => await WriteManualADUAsync(_));
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
        private async Task WriteManualADUAsync(object _)
        {
            isNoOneExecuted = false;

            

            try
            {
                PortManager.PortSettings.DownloadSettingsToSerialPort(PortManager.SerialPort);

                OperationStatus = OperationStatus.Waiting;
                PortManager.Device.Address = 0;

                if (!ValidateMessage())
                {
                    ShowWarning("Недопустимое сообщение");
                    return;
                }

                byte[] message = PrepareMessage();
                if (message == null)
                {
                    ShowWarning("Неприемлемое сообщение");
                    return;
                }

                string logMessage = $"{PortManager.Device.Address.Value:X2} {PortManager.PDUView} {PortManager.CRCView}";
                MessageLogger.LogMessage(MessageType.Output, logMessage);

                byte[] responseBytes = await SendAndReceiveDataAsync(message);
                string responseView = ConvertToHexString(responseBytes);

                OperationStatus = OperationStatus.Success;
                MessageLogger.LogMessage(MessageType.Input, responseView);
            }
            catch (TimeoutException)
            {
#if DEBUG
                MessageLogger.LogMessage(MessageType.Input, "0A 03 FA 00 01 01 F4 00 0A 00 08 3D CE 38 EB 00 02 00 0B 00 0A 00 0A 00 2E 00 03 00 78 00 05 00 16 C0 23 00 00 00 00 00 07 00 07 00 07 00 07 00 07 00 07 00 07 00 07 00 07 00 07 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 07 00 08 00 01 00 00 0B B8 00 00 00 0A 00 08 00 02 00 00 0B B8 00 00 00 01 00 0C 00 02 00 00 0B B8 00 00 00 08 00 08 00 04 00 00 0B B8 00 00 00 14 00 08 00 04 00 00 0B B8 00 00 00 00 00 02 00 0C 0B B8 00 03 03 E8 00 01 00 0C 0B B8 00 00 03 E8 00 01 00 0C 0B B8 00 14 03 E8 00 01 00 0C 0B B8 00 00 03 E8 00 03 00 0C 0B B8 00 03 03 E8 00 01 00 0C 0B B8 00 00 03 E8 00 1E 03 E8 01 2C 00 00 00 00 00 00 00 00 00 3C 00 00 08 58 00 00 00 00 00 00 00 00 00 1E 00 1E 50 BF");
#else
                             ShowWarning("Превышено время ожидания ответа");
#endif

            }
            catch (ArgumentOutOfRangeException ex)
            {
                ShowWarning($"{ex.Message}\nПорт не поддерживает выбранные настройки");
            }
            catch(System.IO.IOException ex)
            {
                ShowWarning($"{ex.Message}\nПорт не поддерживает выбранные настройки\nПроверьте настройки стоп бит и других параметров порта");
            }
            catch (Exception ex)
            {
                ShowWarning($"{ex.Message}\nГлобальная ошибка\nПопробуйте перезагрузить приложение");
            }
            finally
            {
                isNoOneExecuted = true;
                CommandManager.InvalidateRequerySuggested();
                PortManager.SerialPort.Close();
            }
        }

        private bool ValidateMessage()
        {
            return PortManager.Device.Address != 0 &&
                   PortManager.PDUView != null &&
                   PortManager.PDUView.Length > 0 &&
                   PortManager.CRCView != null &&
                   PortManager.CRCView.Length > 0;
        }

        private byte[] PrepareMessage()
        {
            PortManager.PDU = new HexConverter().ConvertToByteArray(PortManager.PDUView);
            PortManager.CRC = new HexConverter().ConvertToByteArray(PortManager.CRCView);

            if (PortManager.CRC == null || PortManager.CRC.Length != 2 || PortManager.PDU == null || PortManager.PDU.Length == 0)
                return null;

            return new byte[] { PortManager.Device.Address.Value }
                .Concat(PortManager.PDU)
                .Concat(PortManager.CRC)
                .ToArray();
        }

        private async Task<byte[]> SendAndReceiveDataAsync(byte[] message)
        {
            if (PortManager.SerialPort == null)
            {
                ShowWarning("Проверьте настройки порта");
                return Array.Empty<byte>();
            }

            //PortManager.SetupPort();

            if (!PortManager.SerialPort.IsOpen)
                PortManager.SerialPort.Open();

            byte[] buffer = new byte[1024];
            PortManager.SerialPort.Write(message, 0, message.Length);

            await Task.Delay(PortManager.SerialPort.ReadTimeout / 2);
            int bytesRead = PortManager.SerialPort.Read(buffer, 0, buffer.Length);
            PortManager.SerialPort.Close();

            byte[] responseBytes = new byte[bytesRead];
            Array.Copy(buffer, responseBytes, bytesRead);

            PortManager.SerialPort.Close();

            return responseBytes;
        }

        private string ConvertToHexString(byte[] bytes)
        {
            return string.Join(" ", bytes.Select(b => b.ToString("X2")));
        }

        private void ShowWarning(string message)
        {
            new MessageBoxCustom().ShowWarning(message);
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

