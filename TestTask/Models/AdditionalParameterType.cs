using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Models
{
    /// <summary>
    /// Перечисление типов допольнительных параметров
    /// </summary>
    public enum AdditionalParameterType
    {
        /// <summary>
        /// Простая строка
        /// </summary>
        String,
        /// <summary>
        /// Строка с историей
        /// </summary>
        StringWithHystory,
        /// <summary>
        /// Значение из списка
        /// </summary>
        ListValue,
        /// <summary>
        /// Набор значений из списка
        /// </summary>
        ListValueSet
    }
}
