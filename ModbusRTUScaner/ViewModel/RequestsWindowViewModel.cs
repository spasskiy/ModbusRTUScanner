using ModbusRTUScanner.Model;
using ModbusRTUScanner.Model.RequestsWindowModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.ViewModel
{
    class RequestsWindowViewModel
    {
        public RequestPortManager PortManager { get; set; }        
        public RequestsWindowViewModel(ModbusDevice device) 
        { 
            PortManager = new RequestPortManager(device);
            
        }
    }
}
