using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    public class SerialPortSpeedNode : INotifyPropertyChanged
    {
        private int? _portSpeed;
        public int? PortSpeed
        {
            get => _portSpeed;
            set => SetOptions(nameof(PortSpeed), ref _portSpeed, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetOptions(nameof(IsSelected), ref _isSelected, value);
        }
        public SerialPortSpeedNode(int? speed = null) { _portSpeed = speed; }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void SetOptions<T>(string Property, ref T variable, T value)
        {
            variable = value;
            OnPropertyChanged(new PropertyChangedEventArgs(Property));
        }
        #endregion
    }
}
