using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model.ModbusML
{
    public class SerialPortWriter
    {
        private SerialPort _port;
        private int _attemptCounter;

        public SerialPortWriter(SerialPort port, int attemptCounter = 5)
        {
            _port = port;
            _attemptCounter = attemptCounter;
        }

        /// <summary>
        /// Отправляет сообщение в порт и ожидает ответ
        /// </summary>
        /// <param name="message"></param>
        /// <param name="delay"></param>
        /// <returns>True, если сообщение успешно отправлено и получило ответ; иначе false</returns>
        public bool WriteAndWaitForResponse(byte[] message, int delay = 250)
        {
            bool result = false;

            if (_port != null)
            {
                try
                {
                    if (_port.IsOpen == false)
                        _port.Open();
                }
                catch (UnauthorizedAccessException ex)
                {
                    return false;
                }

                int counter = _attemptCounter;
                while (counter > 0)
                {
                    try
                    {
                        _port.Write(message, 0, message.Length);
                        Thread.Sleep(delay);
                        // Ожидание ответа от устройства
                        byte[]? response = ReadResponse();

                        if (response is not null)
                        {
                            bool flag = response.SequenceEqual(message);
                            if (flag == false && message.Length > 8)
                            {
                                if (message.Length > 8)
                                {
                                    flag = new Crc16ModbusCalculator().VerifyCRC16(response);
                                }
                            }
                            // Проверка наличия ответа и его корректности
                            if (flag)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                    catch
                    {
                        Thread.Sleep(delay);
                    }
                    counter--;
                }
            }

            return result;
        }

        /// <summary>
        /// Читает ответ от устройства
        /// </summary>
        /// <returns>Массив байтов ответа, или null, если ответ не был получен</returns>
        private byte[]? ReadResponse()
        {
            byte[] response = null;

            // Читаем байты из порта
            if (_port != null && _port.IsOpen)
            {
                int bytesToRead = _port.BytesToRead;
                if (bytesToRead > 0)
                {
                    response = new byte[bytesToRead];
                    _port.Read(response, 0, bytesToRead);
                }
            }


            return response;
        }
    }
}
