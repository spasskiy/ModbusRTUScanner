﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    public class DeviceManager
    {
        public ObservableCollection<ModbusDevice> Devices { get; init; }

        public DeviceManager()
        {
            Devices = new ObservableCollection<ModbusDevice>();
            Devices.Add(new ModbusDevice(1, "COM1", 9600, 8, StopBits.One, Parity.None));
            Devices.Add(new ModbusDevice(20, "COM1", 9600, 8, StopBits.One, Parity.None));
            Devices.Add(new ModbusDevice(20, "COM1", 9600, 8, StopBits.One, Parity.None));
            Devices.Add(new ModbusDevice(20, "COM1", 9600, 8, StopBits.One, Parity.None));
            Devices.Add(new ModbusDevice(20, "COM1", 9600, 8, StopBits.One, Parity.None));
            Devices.Add(new ModbusDevice(20, "COM1", 9600, 8, StopBits.One, Parity.None));
            Devices.Add(new ModbusDevice(20, "COM1", 9600, 8, StopBits.One, Parity.None));
            Devices.Add(new ModbusDevice(20, "COM1", 9600, 8, StopBits.One, Parity.None));
            Devices.Add(new ModbusDevice(20, "COM1", 9600, 8, StopBits.One, Parity.None));
        }
    }
}