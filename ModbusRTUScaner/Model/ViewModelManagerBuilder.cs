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

            // Создание консоли
            ConsoleManager scannerConsole = new ConsoleManager();
            
            // Создание настроек последовательного порта на основе первого доступного порта
            SerialPortManager portManager = new SerialPortManager(ports, new SerialPortSettings(ports.FirstOrDefault()), scannerConsole);

            // Создание менеджера флагов для главного окна ViewModel
            MainWindowViewModelFlags flagsManager = new MainWindowViewModelFlags();

            // Привязка обработчиков смены темы оформления
            new AppThemeManager(flagsManager).SetThemeChangedEvent();

            // Создание менеджера команд для главного окна ViewModel
            ScannerCommandManager scannerCommandManager = new ScannerCommandManager(portManager, flagsManager);

            DeviceManager deviceManager = new DeviceManager();

            // Возвращение нового экземпляра ViewModelManager
            return new ViewModelManager(portManager, flagsManager, scannerConsole, scannerCommandManager, deviceManager);
        }
    }
}
