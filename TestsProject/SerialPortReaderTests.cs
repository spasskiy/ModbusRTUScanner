using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModbusRTUScanner.Model.ModbusML;
using Moq;
using System.IO.Ports;
using System.Linq;
using System.Threading;

namespace SerialPortReaderTests
{
    [TestClass]
    public class SerialPortReaderTests
    {
        [TestMethod]
        public void Read_SendsMessageAndReceivesResponse_ReturnsResponse()
        {
            // Arrange
            var mockPort = new Mock<SerialPort>();
            mockPort.Setup(p => p.IsOpen).Returns(true);
            mockPort.Setup(p => p.BytesToRead).Returns(5);
            mockPort.Setup(p => p.Read(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
                .Callback<byte[], int, int>((buffer, offset, count) =>
                {
                    byte[] response = { 0x01, 0x02, 0x03, 0x04, 0x05 };
                    response.CopyTo(buffer, offset);
                });

            var reader = new SerialPortReader(mockPort.Object);
            byte[] message = { 0x01, 0x03, 0x00, 0x00, 0x00, 0x01, 0x84, 0x0A };

            // Act
            byte[]? result = reader.Read(message, 5);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.SequenceEqual(new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 }));
        }


        [TestMethod]
        public void Read_DeviceRespondsAfterDelay_ReturnsResponse()
        {
            // Arrange
            var mockPort = new Mock<SerialPort>();
            mockPort.Setup(p => p.IsOpen).Returns(true);
            mockPort.SetupSequence(p => p.BytesToRead)
                .Returns(0)
                .Returns(0)
                .Returns(5);
            mockPort.Setup(p => p.Read(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
                .Callback<byte[], int, int>((buffer, offset, count) =>
                {
                    byte[] response = { 0x01, 0x02, 0x03, 0x04, 0x05 };
                    response.CopyTo(buffer, offset);
                });

            var reader = new SerialPortReader(mockPort.Object);
            byte[] message = { 0x01, 0x03, 0x00, 0x00, 0x00, 0x01, 0x84, 0x0A };

            // Act
            byte[]? result = reader.Read(message, 5, 500);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.SequenceEqual(new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 }));
        }
    }
}