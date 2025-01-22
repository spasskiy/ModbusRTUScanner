using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model.RequestsWindowModel
{
    public class ModbusMessageLogger
    {
        private ObservableCollection<MessageLogEntry> messageLog;

        public ModbusMessageLogger()
        {
            messageLog = new ObservableCollection<MessageLogEntry>();
        }

        public ObservableCollection<MessageLogEntry> MessageLog
        {
            get { return messageLog; }
        }

        public void LogMessage(MessageType messageType, string messageContent)
        {
            MessageLogEntry message = new MessageLogEntry(DateTime.Now, messageType, messageContent);
            messageLog.Add(message);
        }
    }
}
