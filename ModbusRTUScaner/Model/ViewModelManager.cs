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
using System.Windows.Input;

namespace ModbusRTUScanner.Model
{
    /// <summary>
    /// Менеджер ViewModel для главного окна
    /// </summary>
    public class ViewModelManager
    {
        /// <summary>
        /// Менеджер команд
        /// </summary>
        public ScannerCommandManager CommandManager { get; init; }

        /// <summary>
        /// Настройки последовательного порта
        /// </summary>
        public SerialPortManager PortManager { get; init; }

        /// <summary>
        /// Менеджер флагов для главного окна ViewModel
        /// </summary>
        public MainWindowViewModelFlags FlagsManager { get; init; }


        public ConsoleManager ScannerConsole { get; init; }


        /// <summary>
        /// Конструктор класса ViewModelManager
        /// </summary>
        /// <param name="ports">Коллекция доступных последовательных портов</param>
        /// <param name="portSettings">Настройки последовательного порта</param>
        /// <param name="flagsManager">Менеджер флагов для главного окна ViewModel</param>
        public ViewModelManager(SerialPortManager portManager, MainWindowViewModelFlags flagsManager, ConsoleManager consoleManager, ScannerCommandManager scannerCommandManager)
        {            
            FlagsManager = flagsManager;
            ScannerConsole = consoleManager;
            PortManager = portManager;
            CommandManager = scannerCommandManager;
        }

    }
}
