using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestTask.Models;

namespace TestTask.ViewModels
{
    public class BaseViewModel : Observer
    {
        protected static IList<Values> Clone(IList<Values> clone)
        {
            return new List<Values>(clone);
        }
    }
}
