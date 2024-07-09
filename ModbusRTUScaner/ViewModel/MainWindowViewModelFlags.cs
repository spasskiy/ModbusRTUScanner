using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.ViewModel
{
    /// <summary>
    /// Класс с флагами состояния приложения
    /// </summary>
    public class MainWindowViewModelFlags : INotifyPropertyChanged
    {
        private bool _isNightModeOn;
        /// <summary>
        /// Флаг ночного режима
        /// </summary>
        public bool IsNightModeOn
        {
            get => _isNightModeOn;
            set => SetOptions(nameof(IsNightModeOn), ref  _isNightModeOn, value);
        }

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
