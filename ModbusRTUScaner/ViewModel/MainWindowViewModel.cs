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
        private readonly MainWindowViewModelFlags _flagsManager;
        
        private readonly RelayCommand<object> _switchThemeCommand;
        
        
        
        public MainWindowViewModel()
        {
            //Инициализация свойств
            _flagsManager = new MainWindowViewModelFlags();

            //Инициализация команд
            _switchThemeCommand = new RelayCommand<object>((_) => FlagsManager.IsNightModeOn = !FlagsManager.IsNightModeOn);
        }

        

        public MainWindowViewModelFlags FlagsManager { get => _flagsManager; }

        public ICommand SwitchThemeCommand => _switchThemeCommand;
    }
}
