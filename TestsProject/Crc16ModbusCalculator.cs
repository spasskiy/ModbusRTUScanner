using ModbusRTUScanner.Model.ModbusML;

namespace TestsProject
{
    [TestClass]
    public class Crc16ModbusCalculatorTests
    {
        private Crc16ModbusCalculator _calculator;

        public Crc16ModbusCalculatorTests()
        {
            _calculator = new Crc16ModbusCalculator();
        }

        [TestInitialize]
        public void Setup()
        {
            if(_calculator is null)
                _calculator = new Crc16ModbusCalculator();
        }

        [TestMethod]
        public void CalculateCRC16_ValidInput_ReturnsCorrectCRC()
        {
            // Arrange
            byte[] input = { 0x0A, 0x03, 0x00, 0x06, 0x00, 0x11 };
            byte[] expectedCRC = { 0x64, 0xBC };

            // Act
            byte[] result = _calculator.CalculateCRC16(input);

            // Assert
            Assert.IsTrue(result.SequenceEqual(expectedCRC));
        }

        [TestMethod]
        public void VerifyCRC16_ValidInput_ReturnsTrue()
        {
            // Arrange
            byte[] input = { 0x0A, 0x03, 0x00, 0x06, 0x00, 0x11, 0x64, 0xBC };

            // Act
            bool result = _calculator.VerifyCRC16(input);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void VerifyCRC16_InvalidInput_ReturnsFalse()
        {
            // Arrange
            byte[] input = { 0x01, 0x03, 0x00, 0x00, 0x00, 0x02, 0x31, 0xCB };

            // Act
            bool result = _calculator.VerifyCRC16(input);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void VerifyCRC16_ShortInput_ReturnsFalse()
        {
            // Arrange
            byte[] input = { 0x01, 0x03 };

            // Act
            bool result = _calculator.VerifyCRC16(input);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void VerifyCRC16_NullInput_ReturnsFalse()
        {
            // Arrange
            byte[] input = null;

            // Act
            bool result = _calculator.VerifyCRC16(input);

            // Assert
            Assert.IsFalse(result);
        }
    }
}