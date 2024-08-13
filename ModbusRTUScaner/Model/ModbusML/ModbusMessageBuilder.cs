using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model.ModbusML
{
    public class ModbusMessageBuilder
    {
        /// <summary>
        /// Метод формирует сообщение запроса для протокола Modbus
        /// Необходимо указать адрес устройства
        /// адрес регистра и значение которое необходимо записать
        /// Так же нужно выбрать тип используемой функции
        /// </summary>
        /// <param name="deviceAddress"></param>
        /// <param name="registerAddress"></param>
        /// <param name="value"></param>
        /// <param name="functionType"></param>
        /// <returns></returns>
        public byte[] BuildRTUMessage(int deviceAddress, int registerAddress, ushort[] values, ModbusFunctionType functionType)
        {
            byte function = ((byte)functionType);
            byte[] messageBody;
            if (functionType == ModbusFunctionType.WriteMultipleCoils_15 || functionType == ModbusFunctionType.WriteMultipleHoldingRegisters_10)
            {
                ushort quantity = (ushort)values.Count();
                messageBody = new byte[quantity * 2 + 7];
                messageBody[0] = (byte)deviceAddress; //Адрес устройства
                messageBody[1] = function;//Функциональный код
                messageBody[2] = (byte)(registerAddress >> 8);//Адрес первого регистра Hi байт
                messageBody[3] = (byte)registerAddress;//Адрес первого регистра Lo байт
                messageBody[4] = (byte)(quantity >> 8);//	Количество регистров Hi байт
                messageBody[5] = (byte)quantity;//Количество регистров Lo байт
                messageBody[6] = (byte)(quantity * 2);//	Количество байт далее
                int i = 7;
                int k = 0;
                for (; k < quantity; i += 2)
                {
                    messageBody[i] = (byte)(values[k] >> 8);//	Значение Hi
                    messageBody[i + 1] = (byte)values[k];//Значение Lo
                    k++;
                }
            }
            else
            {
                messageBody = new byte[6];
                messageBody[0] = (byte)deviceAddress;
                messageBody[1] = function;
                messageBody[2] = (byte)(registerAddress >> 8);
                messageBody[3] = (byte)registerAddress;
                messageBody[4] = (byte)(values[0] >> 8);
                messageBody[5] = (byte)values[0];
            }

            byte[] checkSum = new Crc16ModbusCalculator().CalculateCRC16(messageBody);

            return messageBody.Concat(checkSum).ToArray();
        }
    }
}
