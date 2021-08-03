using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.ViewModels
{
    /// <summary>
    /// Класс, на основе которого необходимо создавать все ViewModels приложения
    /// </summary>
    public class BaseViewModel : Observer
    {
        protected void RollBack(ObservableCollection<Values> rollBackFrom, ObservableCollection<Values> rollBackTo)
        {
            rollBackFrom.Clear();
            foreach (Values item in rollBackTo)
            {
                rollBackFrom.Add(new Values { Name = (string)item.Name.Clone() });
            }
        }

        protected void RollBack(ObservableCollection<AdditionalParameterViewModel> rollBackFrom, ObservableCollection<AdditionalParameterViewModel> rollBackTo)
        {
            rollBackFrom.Clear();
            foreach (AdditionalParameterViewModel item in rollBackTo)
            {
                rollBackFrom.Add(new AdditionalParameterViewModel(item.Model.Clone(), new NavigationService(), new DialogService()));
            }
        }
    }
}
