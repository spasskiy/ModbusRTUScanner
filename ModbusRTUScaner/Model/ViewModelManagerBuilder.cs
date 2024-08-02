using ModbusRTUScanner.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    /// <summary>
    /// Билдер для создания экземпляра ViewModelManager
    /// </summary>
    internal class ViewModelManagerBuilder
    {
        /// <summary>
        /// Создает и возвращает экземпляр ViewModelManager
        /// </summary>
        /// <returns>Экземпляр ViewModelManager</returns>
        public ViewModelManager Build()
        {
            // Получение доступных последовательных портов
            ObservableCollection<SerialPort> ports = new ObservableCollection<SerialPort>(new SerialPortUtils().GetAvailableSerialPorts());

            // Создание настроек последовательного порта на основе первого доступного порта
            SerialPortManager portSettings = new SerialPortManager(ports, new SerialPortSettings(ports.FirstOrDefault()));

            // Создание менеджера флагов для главного окна ViewModel
            MainWindowViewModelFlags flagsManager = new MainWindowViewModelFlags();

            // Привязка обработчиков смены темы оформления
            new AppThemeManager(flagsManager);

            // Возвращение нового экземпляра ViewModelManager
            return new ViewModelManager(portSettings, flagsManager);
        }
    }
}
