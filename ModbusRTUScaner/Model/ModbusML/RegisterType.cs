using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model.ModbusML
{
    public enum RegisterType
    {
        InputRegister,
        HoldingRegister,
        Coil,
        DiscreteInput
    }
}
