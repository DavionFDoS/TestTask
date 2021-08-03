using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.ViewModels;

namespace TestTask.Services
{
    public interface INavigationService
    {
        /// <summary>
        /// Метод открытия модального окна
        /// </summary>
        /// <param name="viewModel"></param>
        void NavigateTo(BaseViewModel viewModel);
        /// <summary>
        /// Метод закрытия модального окна
        /// </summary>
        void ExitFromEditListWindow();
        /// <summary>
        /// Метод закрытия окна приложения
        /// </summary>
        void ExitFromApplication();
    }
}
