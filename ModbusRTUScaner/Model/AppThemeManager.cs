using ModbusRTUScanner.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModbusRTUScanner.Model
{
    /// <summary>
    /// Отвечает за переключение приложения между темами
    /// </summary>
    public class AppThemeManager
    {
        private readonly MainWindowViewModelFlags _flagsManager;

        public AppThemeManager(MainWindowViewModelFlags flagsManager)
        {
            _flagsManager = flagsManager;
            
        }

        /// <summary>
        /// Подписывает на событие изменения темы
        /// </summary>
        public void SetThemeChangedEvent()
        {
            _flagsManager.ThemeChanged += OnThemeChanged;
        }

        /// <summary>
        /// Обрабатывает событие при изменении темы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnThemeChanged(object? sender, EventArgs e)
        {
            if (_flagsManager.IsNightModeOn)
            {
                ApplyTheme("StyleDictionaryNight.xaml");
            }
            else
            {
                ApplyTheme("StyleDictionaryDay.xaml");
            }
        }

        /// <summary>
        /// Применяет указанный словарь темы.
        /// </summary>
        /// <param name="dictionaryName"></param>
        private void ApplyTheme(string dictionaryName)
        {
            string directory = "view/styles/";
            string currentDictionaryName = dictionaryName == "StyleDictionaryNight.xaml" ? "StyleDictionaryDay.xaml" : "StyleDictionaryNight.xaml";

            // Логика применения темы
            var dictionariesToRemove = Application.Current.Resources.MergedDictionaries
                .Where(d => d.Source != null && d.Source.OriginalString.Contains(directory + currentDictionaryName)).ToList();
            foreach (var dictionary in dictionariesToRemove)
                Application.Current.Resources.MergedDictionaries.Remove(dictionary);

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/ModbusRTUScanner;component/" + directory + dictionaryName) });
        }
    }
}
