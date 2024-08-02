using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model
{
    /// <summary>
    /// Представляет узел в связанном списке для консольного приложения
    /// </summary>
    internal class ConsoleNode
    {
        /// <summary>
        /// Ссылка на следующий узел в связанном списке
        /// </summary>
        public ConsoleNode? NodeNext;

        /// <summary>
        /// Значение узла
        /// </summary>
        public string NodeValue { get; set; }

        /// <summary>
        /// Конструктор класса ConsoleNode
        /// </summary>
        /// <param name="value">Значение узла</param>
        public ConsoleNode(string value)
        {
            NodeValue = value;
        }
    }
}
