using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model.ModbusML
{

    public class ModbusRTUML : ModbusML
    {
        const int TECHNICAL_BYTE_COUNT = 5;//Количество технических байт в каждом сообщении. Заголовок + CRC
        const int HEADER_BYTE_COUNT = 3;//Количество байт в заголовке
        const int SINGLE_REGISTER_RESPONSE_BYTE_COUNT = 7;//Количество байт при считывании только одного регистра
        public ModbusRTUML(SerialPort port, int deviceAddress) : base(port, deviceAddress)
        {
        }

        public override void SetAddress(int address)
        {
            _deviceAddress = address;
        }


        public override ushort? ReadSingleRegister(RegisterType type, int registerAddress)
        {
            ushort? result = null;
            if (_port.IsOpen)
            {
                ModbusFunctionType functionType = new ModbusFunctionConverter().GetReadFunction(type);

                byte[] message = new ModbusMessageBuilder().BuildRTUMessage(_deviceAddress, registerAddress, [1], functionType);
                byte[]? output = null;
                try
                {
                    output = new SerialPortReader(_port).Read(message, SINGLE_REGISTER_RESPONSE_BYTE_COUNT);
                }
                catch
                {
                    return null;
                }

                if (output is not null && output.Length == SINGLE_REGISTER_RESPONSE_BYTE_COUNT && new Crc16ModbusCalculator().VerifyCRC16(output))
                {

                    if (output != null && output.Length >= 4)
                        result = (ushort)((output[3] << 8) | output[4]);
                }
            }
            return result;
        }
        public override bool WriteSingleRegister(RegisterType type, int address, int value)
        {
            if (type == RegisterType.HoldingRegister || type == RegisterType.Coil)
            {
                bool result = false;
                SerialPortWriter writer = new SerialPortWriter(_port);
                try
                {
                    result = writer.WriteAndWaitForResponse(new ModbusMessageBuilder().BuildRTUMessage(_deviceAddress, address, [(ushort)value], new ModbusFunctionConverter().GetWriteFunctionSingle(type)));

                }
                catch (UnauthorizedAccessException ex)
                {
                    // Обработка ошибки доступа к порту здесь
                    Console.WriteLine($"Ошибка доступа к порту: {ex.Message}");
                    return false;
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Ошибка открытия порта: {ex.Message}");
                    return false;
                }
                _port.DiscardOutBuffer();
                Thread.Sleep(100); //Задержка что бы устройство успело применить настройку. Вероятно зависит от устройства
                return result;
            }
            else
                return false;
        }
        public override ushort[]? ReadMultipleRegisters(RegisterType type, int startAddress, int quantity)
        {
            ModbusFunctionType functionType = new ModbusFunctionConverter().GetReadFunction(type);
            byte[] message = new ModbusMessageBuilder().BuildRTUMessage(_deviceAddress, startAddress, [(ushort)quantity], functionType);

            int expectedBytes = quantity * 2 + TECHNICAL_BYTE_COUNT;
            byte[]? output = new SerialPortReader(_port).Read(message, expectedBytes);
            bool verify = new Crc16ModbusCalculator().VerifyCRC16(output);

            ushort[]? result = null;

            if (verify && output.Length == expectedBytes)
            {
                result = new ushort[quantity];
                int k = HEADER_BYTE_COUNT;
                int i = 0;
                while (k < quantity * 2 + HEADER_BYTE_COUNT)
                {
                    result[i++] = (ushort)((output[k++] << 8) | output[k++]);
                }
            }
            return result;
        }
        public override bool WriteMultipleRegisters(RegisterType type, int startAddress, ushort[] values)
        {
            bool result = false;
            if (type == RegisterType.HoldingRegister || type == RegisterType.Coil)
            {
                SerialPortWriter writer = new SerialPortWriter(_port);
                ModbusMessageBuilder messageBuilder = new ModbusMessageBuilder();
                ModbusFunctionConverter functionConverter = new ModbusFunctionConverter();

                byte[] message = messageBuilder.BuildRTUMessage(_deviceAddress, startAddress, values, functionConverter.GetWriteFunctionMultiple(type));

                string hexString = BitConverter.ToString(message).Replace("-", " ");
                Console.WriteLine("Мой пакет - " + hexString);

                if (_port.IsOpen)
                {
                    result = writer.WriteAndWaitForResponse(message);
                }

                _port.DiscardOutBuffer();
                Thread.Sleep(100); // Задержка что бы устройство успело применить настройку. Вероятно зависит от устройства

                return result;
            }
            else
            {
                return result;
            }
        }


    }
}
