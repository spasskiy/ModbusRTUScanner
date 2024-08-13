using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model.ModbusML
{
    public enum ModbusFunctionType
    {
        ReadCoils_01 = 1,
        ReadDiscreteInputs_02 = 2,
        ReadHoldingRegisters_03 = 3,
        ReadInputRegisters_04 = 4,
        WriteSingleCoil_05 = 5,
        WriteSingleHoldingRegister_06 = 6,
        WriteMultipleHoldingRegisters_10 = 16,
        WriteMultipleCoils_15 = 15
    }
}
