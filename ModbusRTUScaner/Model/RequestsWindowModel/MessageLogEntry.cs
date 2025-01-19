using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model.RequestsWindowModel
{
    public class MessageLogEntry
    {
        public DateTime Timestamp { get; set; }
        public MessageType MessageType { get; set; }
        public string MessageContent { get; set; }

        public MessageLogEntry(DateTime timestamp, MessageType messageType, string messageContent)
        {
            Timestamp = timestamp;
            MessageType = messageType;
            MessageContent = messageContent;
        }


    }
    public enum MessageType
    {
        Input,
        Output
    }
}
