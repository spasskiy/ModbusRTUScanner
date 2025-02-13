using ModbusRTUScanner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ModbusRTUScanner.ViewModel
{
    /// <summary>
    ///  ViewModel для главного окна приложения.
    /// </summary>
    public partial class MainWindowViewModel
    {
        /// <summary>
        /// Менеджер ViewModel.
        /// </summary>
        public ViewModelManager ViewManager { get; init; }

        /// <summary>
        /// Конструктор для MainWindowViewModel.
        /// </summary>
        public MainWindowViewModel()
        {
            ViewManager = new ViewModelManagerBuilder().Build();
#if DEBUG
            ViewManager.ScannerConsole.AddNode("Приложение запущено в DEBUG режиме");
#endif
        }


    }
}
