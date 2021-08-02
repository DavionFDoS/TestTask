using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestTask.ViewModels;

namespace TestTask.Services
{
    public class NavigationService : INavigationService
    {
        public void NavigateTo(BaseViewModel viewModel)
        {
            if (viewModel is AdditionalParameterViewModel)
            {
                var view = new EditListWindow() { DataContext = viewModel };
                view.Owner = Application.Current.MainWindow;
                view.ShowDialog();
            }
        }

        public void NavigateFrom(BaseViewModel viewModel)
        {
            var view = Application.Current.Windows.Cast<Window>()
                                      .Where(win => win is EditListWindow)
                                      .FirstOrDefault();

            if (view != null)
            {
                view.Close();
            }
        }
    }
}
