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
        /// Определяет актуальную задержку для текущей скорости,
        /// используя степенную функцию, подобранную по граничным условиям:
        /// при скорости 300 мс – задержка 1000 мс, при 115200 мс – 5 мс.
        /// </summary>
        /// <param name="portSpeed">Скорость порта</param>
        /// <returns>Задержка в миллисекундах</returns>
        private int GetActualDelay(int portSpeed)
        {
            if (portSpeed <= 300)
                return 1000;
            if (portSpeed >= 115200)
                return 5;

            const double A = 159400;
            const double B = -0.889;
            double delay = A * Math.Pow(portSpeed, B);
            return (int)Math.Round(delay);
        }

    }
}
