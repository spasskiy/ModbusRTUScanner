using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModbusRTUScanner.Model
{
    /// <summary>
    /// Класс для использования команд с параметрами
    /// </summary>
    /// <typeparam name="T">Тип параметра, передаваемого команде</typeparam>
    public class RelayCommand<T> : ICommand
    {
        /// <summary>
        /// Делегат, выполняющий действие команды
        /// </summary>
        private readonly Action<T> _execute;

        /// <summary>
        /// Делегат, определяющий, может ли команда выполняться
        /// </summary>
        private readonly Func<T, bool> _canExecute;

        /// <summary>
        /// Конструктор класса RelayCommand
        /// </summary>
        /// <param name="execute">Делегат, выполняющий действие команды</param>
        /// <param name="canExecute">Делегат, определяющий, может ли команда выполняться (опционально)</param>
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Определяет, может ли команда выполняться
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        /// <returns>True, если команда может выполняться, иначе false</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        /// <summary>
        /// Выполняет команду
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        /// <summary>
        /// Событие, возникающее при изменении условий, которые могут повлиять на возможность выполнения команды
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
