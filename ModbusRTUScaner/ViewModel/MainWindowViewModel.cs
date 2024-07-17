using ModbusRTUScanner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ModbusRTUScanner.ViewModel
{
    public partial class MainWindowViewModel
    {        
        private readonly RelayCommand<object> _switchThemeCommand;
        public ViewModelManager ViewManager {get; init;}

        public MainWindowViewModel()
        {
            //Инициализация свойств
            ViewManager = new ViewModelManagerBuilder().Build();

            //Инициализация команд
            _switchThemeCommand = new RelayCommand<object>((_) => ViewManager.FlagsManager.IsNightModeOn = !ViewManager.FlagsManager.IsNightModeOn);

            
        }               


        public ICommand SwitchThemeCommand => _switchThemeCommand;

    }
}
