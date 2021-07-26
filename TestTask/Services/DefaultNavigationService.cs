using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestTask.ViewModels;

namespace TestTask.Services
{
    public class DefaultNavigationService : INavigationService
    {
        public void NavigateTo(BaseViewModel viewModel)
        {
            if (viewModel is AdditionalParameterViewModel)
            {
                var view = new ListWindow() { DataContext = viewModel };
                view.Owner = Application.Current.MainWindow;
                view.Show();
            }
        }
    }
}
