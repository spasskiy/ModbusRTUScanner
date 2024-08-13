using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model.ModbusML
{
    public class SerialPortReader
    {
        private SerialPort _port;
        public SerialPortReader(SerialPort port)
        {
            _port = port;
        }

        /// <summary>
        /// Посылает сообщение в порт и принимает ответ
        /// </summary>
        /// <param name="message"></param>
        /// <param name="expectedBytes"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public byte[]? Read(byte[] message, int expectedBytes = 0, int delay = 250)
        {
            byte[]? result = null;

            if (_port != null)
            {
                try
                {
                    if (!_port.IsOpen)
                        _port.Open();
                    if (_port.BytesToRead > 0)
                        _port.DiscardInBuffer();

                    if (_port.IsOpen)
                        _port.Write(message, 0, message.Length);

                    if (WaitRecive(expectedBytes))
                    {
                        if (_port.IsOpen)
                        {
                            result = new byte[_port.BytesToRead];
                            _port.Read(result, 0, _port.BytesToRead);
                        }
                    }
                }
                catch
                {
                    result = null;//Если произошла ошибка, то просто пропускаем итерацию
                }
            }
            return result;
        }

        /// <summary>
        /// Метод который определяет завершилась ли передача или же нет
        /// </summary>
        private bool WaitRecive(int expectedBytes = 0)
        {
            if (_port.IsOpen == false)
                return true; //Непредвиденная ситуация. Разбираться нужно на вышележащем уровне

            bool result = false;
            int initialBytesToRead = 0;
            int readDelay = GetActualDelay(_port.BaudRate);
            int attemptCounter = 3;//Расчёт на то, что если три цикла нет изменений, то можно завершать ожидание

            while (_port != null && _port.IsOpen && (initialBytesToRead < _port.BytesToRead || initialBytesToRead == 0) && attemptCounter > 0)
            {
                if (expectedBytes != 0 && _port.BytesToRead >= expectedBytes)
                    return true; //Если обнаружили, что уже принято нужное число байт, то завершаем ожидание

                if (initialBytesToRead != _port.BytesToRead)
                {
                    initialBytesToRead = _port.BytesToRead;
                    attemptCounter = 3;
                }
                else
                    attemptCounter--;

                Thread.Sleep(readDelay);
            }
            return result;
        }


        /// <summary>
        /// Определяет актуальную задержку для текущей скорости
        /// </summary>
        /// <param name="portSpeed"></param>
        /// <returns></returns>
        private int GetActualDelay(int portSpeed)
        {
            switch (portSpeed)
            {
                case var v when v >= 115200:
                    return 5;
                case var v when v >= 57600:
                    return 10;
                case var v when v >= 9600:
                    return 30;
                case var v when v >= 1000:
                    return 250;
                case var v when v >= 300:
                    return 750;
                default:
                    return 1000;
            }
        }
    }
}
