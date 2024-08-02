using ModbusRTUScanner.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    /// <summary>
    /// Менеджер ViewModel для главного окна
    /// </summary>
    public class ViewModelManager : INotifyPropertyChanged
    {
        /// <summary>
        /// Настройки последовательного порта
        /// </summary>
        public SerialPortManager PortManager { get; init; }

        /// <summary>
        /// Менеджер флагов для главного окна ViewModel
        /// </summary>
        public MainWindowViewModelFlags FlagsManager { get; init; }



        public ConsoleManager Console { get; init; }

        /// <summary>
        /// Объект для блокировки при выполнении сканирования
        /// </summary>
        public object ScanRunLockObject;

        /// <summary>
        /// Конструктор класса ViewModelManager
        /// </summary>
        /// <param name="ports">Коллекция доступных последовательных портов</param>
        /// <param name="portSettings">Настройки последовательного порта</param>
        /// <param name="flagsManager">Менеджер флагов для главного окна ViewModel</param>
        public ViewModelManager(SerialPortManager portManager, MainWindowViewModelFlags flagsManager)
        {
            PortManager = portManager;
            FlagsManager = flagsManager;
            ScanRunLockObject = new();
            Console = new();
        }

        
        public void SetDataBits(object param)
        {
            if(param is string str && str != PortManager.PortSettings.DataBits.ToString())
            {
                Console.AddNode($"DataBits переключено на {param}");
                int.TryParse(str, out int dataBits);
                PortManager.PortSettings.DataBits = dataBits;
            }
            
        }


        #region INotifyPropertyChanged
        /// <summary>
        /// Событие, возникающее при изменении свойства
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Вызывает событие PropertyChanged
        /// </summary>
        /// <param name="e">Аргументы события</param>
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Устанавливает значение свойства и вызывает событие PropertyChanged
        /// </summary>
        /// <typeparam name="T">Тип свойства</typeparam>
        /// <param name="property">Имя свойства</param>
        /// <param name="variable">Ссылка на переменную свойства</param>
        /// <param name="value">Новое значение свойства</param>
        protected void SetOptions<T>(string property, ref T variable, T value)
        {
            variable = value;
            OnPropertyChanged(new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}
