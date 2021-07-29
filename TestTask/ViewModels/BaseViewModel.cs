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
            IList<Values> copyList = new List<Values>();
            foreach(Values values in clone)
            {
                copyList.Add(new Values { Name = values.Name });
            }
            return new List<Values>(copyList);
        }
    }
}
