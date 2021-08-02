using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TestTask.Models
{
    /// <summary>
    /// Класс, представляющий собой модель дополнительного параметра
    /// </summary>
    public class AdditionalParameter
    {
        /// <summary>
        /// Название парметра
        /// </summary>
        public string Title { get; set; } 
        /// <summary>
        /// Тип параметра
        /// </summary>
        public AdditionalParameterType Type { get; set; } 

        /// <summary>
        /// Список значений параметра
        /// </summary>
        public List<Values> ValuesList { get; set; }

        /// <summary>
        /// Метод для получения клона списка значений
        /// </summary>
        /// <param name="originalList"></param>
        /// <returns></returns>
        public IList<Values> Clone(IList<Values> originalList)
        {
            if (originalList != null)
            {
                IList<Values> copyList = new List<Values>();
                foreach (Values values in originalList)
                {
                    copyList.Add(new Values { Name = values.Name });
                }
                return new List<Values>(copyList);
            }
            else
            {
                return new List<Values>();
            }
        }
    }
}
