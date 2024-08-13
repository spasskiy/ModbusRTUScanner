using ModbusRTUScanner.Model.ModbusML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsProject
{
    [TestClass]
    public class ModbusMessageBuilderTests
    {
        private ModbusMessageBuilder _builder;

        [TestInitialize]
        public void Setup()
        {
            _builder = new ModbusMessageBuilder();
        }

        [TestMethod]
        public void BuildRTUMessage_WriteSingleRegister_ReturnsCorrectMessage()
        {
            // Arrange
            int deviceAddress = 1;
            int registerAddress = 0x0001;
            ushort[] values = { 0x1234 };
            ModbusFunctionType functionType = ModbusFunctionType.WriteSingleHoldingRegister_06;
            byte[] expectedMessage = { 0x01, 0x06, 0x00, 0x01, 0x12, 0x34, 0xD5, 0x7D };

            // Act
            byte[] result = _builder.BuildRTUMessage(deviceAddress, registerAddress, values, functionType);

            // Assert
            Assert.IsTrue(result.SequenceEqual(expectedMessage));
        }

        [TestMethod]
        public void BuildRTUMessage_WriteMultipleHoldingRegisters_ReturnsCorrectMessage()
        {
            // Arrange
            int deviceAddress = 1;
            int registerAddress = 0x0001;
            ushort[] values = { 0x1234, 0x5678 };
            ModbusFunctionType functionType = ModbusFunctionType.WriteMultipleHoldingRegisters_10;
            byte[] expectedMessage = { 0x01, 0x10, 0x00, 0x01, 0x00, 0x02, 0x04, 0x12, 0x34, 0x56, 0x78, 0x49, 0x57 };

            // Act
            byte[] result = _builder.BuildRTUMessage(deviceAddress, registerAddress, values, functionType);

            // Assert
            Assert.IsTrue(result.SequenceEqual(expectedMessage));
        }

        [TestMethod]
        public void BuildRTUMessage_WriteMultipleCoils_ReturnsCorrectMessage()
        {
            // Arrange
            int deviceAddress = 1;
            int registerAddress = 0x0001;
            ushort[] values = { 0x1234, 0x5678 };
            ModbusFunctionType functionType = ModbusFunctionType.WriteMultipleCoils_15;
            byte[] expectedMessage = { 0x01, 0x0F, 0x00, 0x01, 0x00, 0x02, 0x04, 0x12, 0x34, 0x56, 0x78, 0x78, 0xF2 };

            // Act
            byte[] result = _builder.BuildRTUMessage(deviceAddress, registerAddress, values, functionType);

            // Assert
            Assert.IsTrue(result.SequenceEqual(expectedMessage));
        }
    }
}
