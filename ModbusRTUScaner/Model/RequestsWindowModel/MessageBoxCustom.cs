using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ModbusRTUScanner.View.MessageBoxCustom;

namespace ModbusRTUScanner.Model.RequestsWindowModel
{
    public class MessageBoxCustom
    {
        /// <summary>
        /// Отображает окно предупреждения
        /// <param name="message"></param>
        /// <param name="owner"></param>
        public void ShowWarning(string message, Window? owner = null)
        {
            if (owner is null)
                Application.Current.Dispatcher.Invoke(() =>
                {
                    owner = Application.Current.MainWindow;
                });

            // Создаем экземпляр окна WarningWindow
            Window window;

            // Проверяем, является ли вызывающий поток STA
            if (Application.Current.Dispatcher.CheckAccess())
            {
                // Вызывающий поток является STA, создаем окно напрямую
                window = new WarningWindow(message);
                if (owner is not null)
                    window.Owner = owner;
                window.ShowDialog();
            }
            else
            {
                // Вызывающий поток не является STA, используем Dispatcher для создания окна
                Application.Current.Dispatcher.Invoke(() =>
                {
                    window = new WarningWindow(message);
                    if (owner is not null)
                        window.Owner = owner;
                    window.ShowDialog();
                });
            }
        }

        /// <summary>
        /// Отображает окно подтверждения. Возвращает принято ли оно.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public bool ShowDialog(string message, Window? owner = null)
        {
            return ShowDialog(message, "Да", "Нет", owner);
        }
        public bool ShowDialog(string message, string yes, string no, Window? owner = null)
        {
            Window window = new DialogWindow(message, yes, no);
            if (owner is not null)
                window.Owner = owner;
            window.ShowDialog();
            bool? result = window.DialogResult;

            return result ?? false;
        }

        public void ShowInfo(string message, Window? owner = null)
        {
            Window window = new InfoWindow(message);
            if (owner is not null)
                window.Owner = owner;
            window.ShowDialog();
        }
    }
}
