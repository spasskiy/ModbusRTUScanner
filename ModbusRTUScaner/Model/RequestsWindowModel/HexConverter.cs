using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusRTUScanner.Model.RequestsWindowModel
{
    public class HexConverter
    {
        /// <summary>
        /// Преобразует hex строку в int
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public int? ConvertHexToInt(string hex)
        {
            int result;
            string cleanedHex = hex.Replace(" ", "").Replace(":", "");
            if (int.TryParse(cleanedHex, System.Globalization.NumberStyles.HexNumber, null, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Преобразует hex строку в массив байт
        /// с учётом разделителей в виде пробела и :
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public byte[]? ConvertToByteArray(string? hexString)
        {
            List<string> values = SplitString(hexString);
            if (values.Count > 0)
            {
                byte[] result = new byte[values.Count];

                for (int i = 0; i < values.Count; i++)
                {
                    result[i] = Convert.ToByte(values[i], 16);
                }
                return result;
            }

            return null;
        }

        /// <summary>
        /// Разделяет строку на подстроки по 2 символа
        /// с учётом разделителей в виде пробела и :
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<string> SplitString(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return new List<string>();
            }

            input = input.ToUpper().Replace(":", " ");
            List<string> result = new List<string>();
            List<char> characters = input.Where(c => char.IsLetterOrDigit(c) || c == ' ').ToList();

            StringBuilder currentElement = new StringBuilder();

            for (int i = 0; i < characters.Count; i++)
            {
                if (characters[i] == ' ')
                {
                    if (currentElement.Length > 0)
                    {
                        result.Add(currentElement.ToString());
                        currentElement.Clear();
                    }
                }
                else
                {
                    currentElement.Append(characters[i]);
                    if (currentElement.Length == 2)
                    {
                        result.Add(currentElement.ToString());
                        currentElement.Clear();
                    }
                }
            }

            if (currentElement.Length > 0)
            {
                result.Add(currentElement.ToString());
            }

            return result;
        }

        public string ConvertToString(byte[] bytes)
        {
            if (bytes == null)
            {
                return string.Empty;
            }

            return string.Join(" ", bytes.Select(b => b.ToString("X2")));
        }
    }
}
