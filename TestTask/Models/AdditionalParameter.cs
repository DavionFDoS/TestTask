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
    public class AdditionalParameter // Модель добавленного параметра для DataGrid
    {
        public string Title { get; set; } // Название
        public AdditionalParameterType Type { get; set; } // Перечисления для ComboBox

        public List<Values> ValuesList { get; set; } // Список параметров в дополнительном окне
    }
}
