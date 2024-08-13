using ModbusRTUScanner.Model.ModbusML;

namespace ModbusRTUScanner.Model.ModbusML
{
    public class Crc16ModbusCalculator
    {
        public byte[] CalculateCRC16(byte[] buf)
        {
            ushort crc = 0xFFFF;
            for (int pos = 0; pos < buf.Length; pos++)
            {
                crc ^= buf[pos];
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x0001) == 0x0001)
                        crc = (ushort)((crc >> 1) ^ 0xA001);
                    else
                        crc >>= 1;
                }
            }
            return new byte[] { (byte)crc, (byte)(crc >> 8) };
        }

        public bool VerifyCRC16(byte[]? buf)
        {
            if (buf == null || buf.Length < 3)
                return false;

            var dataWithoutCRC = buf.Take(buf.Length - 2).ToArray();
            var calculatedCRC = CalculateCRC16(dataWithoutCRC);
            var expectedCRC = buf.Skip(buf.Length - 2).ToArray();

            return calculatedCRC.SequenceEqual(expectedCRC);
        }
    }
}


