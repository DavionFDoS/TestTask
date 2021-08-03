using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Models
{
    /// <summary>
    /// Класс, представляющий собой значение из списка
    /// </summary>
    public class Values : Observer, ICloneable
    {
        /// <summary>
        /// Имя значения
        /// </summary>
        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged();
                }
            }
        }
        /// <summary>
        /// Метод реализующий поверхностное копирование
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
