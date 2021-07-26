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
    public class AdditionalParameter
    {
        public string Title { get; set; }
        public AdditionalParameterType Type { get; set; }

        public List<Params> ParametersList { get; set; }
    }
}
