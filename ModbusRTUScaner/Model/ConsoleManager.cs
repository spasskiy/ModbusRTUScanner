using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    /// <summary>
    /// Менеджер для управления выводом консоли и уведомлениями об изменении свойств
    /// </summary>
    public class ConsoleManager : INotifyPropertyChanged
    {
        /// <summary>
        /// Вывод консоли
        /// </summary>
        private string? _consoleOutput;

        /// <summary>
        /// Вывод консоли
        /// </summary>
        public string? ConsoleOutput
        {
            get => _consoleOutput;
            set 
            {
                if(string.IsNullOrEmpty(value))
                    Clear();
                SetOptions(nameof(ConsoleOutput), ref _consoleOutput, value);
            } 
        }

        private ConsoleNode? nodeHead;
        private ConsoleNode? nodeTail;
        private const int MaxNodeCount = 500;
        private int nodeCount;

        /// <summary>
        /// Добавляет новый узел в связанный список и удаляет первый узел, если количество узлов превышает 500
        /// </summary>
        /// <param name="value">Значение нового узла</param>
        public void AddNode(string value)
        {
            ConsoleNode newNode = new ConsoleNode(value);
            if (nodeHead == null)
            {
                nodeHead = newNode;
                nodeTail = newNode;
            }
            else
            {
                nodeTail.NodeNext = newNode;
                nodeTail = newNode;
            }

            nodeCount++;

            if (nodeCount > MaxNodeCount)
            {
                RemoveFirstNode();
            }
            ConsoleOutput = GenerateOutputString();
        }
        /// <summary>
        /// Генерирует строковый вывод
        /// </summary>
        /// <returns></returns>
        private string GenerateOutputString()
        {
            StringBuilder sb = new StringBuilder();
            ConsoleNode? currentNode = nodeHead;
            while (currentNode != null)
            {
                sb.AppendLine(currentNode.NodeValue);
                currentNode = currentNode.NodeNext;
            }
            return sb.ToString();
        }

        /// <summary>
        /// Удаляет первый узел из связанного списка
        /// </summary>
        private void RemoveFirstNode()
        {
            if (nodeHead != null)
            {
                nodeHead = nodeHead.NodeNext;
                nodeCount--;
            }
        }

        /// <summary>
        /// Очистить консоль
        /// </summary>
        public void Clear()
        {
            nodeHead = null;
            nodeTail = null;
            nodeCount = 0;            
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
