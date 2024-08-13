using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model.ModbusML
{
    public class ModbusFunctionConverter
    {
        internal ModbusFunctionType GetReadFunction(RegisterType registerType)
        {
            switch (registerType)
            {
                case RegisterType.Coil:
                    return ModbusFunctionType.ReadCoils_01;
                case RegisterType.DiscreteInput:
                    return ModbusFunctionType.ReadDiscreteInputs_02;
                case RegisterType.HoldingRegister:
                    return ModbusFunctionType.ReadHoldingRegisters_03;
                case RegisterType.InputRegister:
                    return ModbusFunctionType.ReadInputRegisters_04;
                default:
                    throw new ArgumentException("Invalid register type", nameof(registerType));
            }
        }

        internal ModbusFunctionType GetWriteFunctionSingle(RegisterType registerType)
        {
            switch (registerType)
            {
                case RegisterType.Coil:
                    return ModbusFunctionType.WriteSingleCoil_05;
                case RegisterType.DiscreteInput:
                    throw new ArgumentException("Cannot write to discrete input", nameof(registerType));
                case RegisterType.HoldingRegister:
                    return ModbusFunctionType.WriteSingleHoldingRegister_06;
                case RegisterType.InputRegister:
                    throw new ArgumentException("Cannot write to input register", nameof(registerType));
                default:
                    throw new ArgumentException("Invalid register type", nameof(registerType));
            }
        }

        internal ModbusFunctionType GetWriteFunctionMultiple(RegisterType registerType)
        {
            switch (registerType)
            {
                case RegisterType.Coil:
                    return ModbusFunctionType.WriteMultipleCoils_15;
                case RegisterType.DiscreteInput:
                    throw new ArgumentException("Cannot write to discrete input", nameof(registerType));
                case RegisterType.HoldingRegister:
                    return ModbusFunctionType.WriteMultipleHoldingRegisters_10;
                case RegisterType.InputRegister:
                    throw new ArgumentException("Cannot write to input register", nameof(registerType));
                default:
                    throw new ArgumentException("Invalid register type", nameof(registerType));
            }
        }
    }
}
