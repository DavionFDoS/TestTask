using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TestTask.Services;
using TestTask.ViewModels;

namespace TestTask
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            var viewModel = new ApplicationViewModel(new DialogService(), new JsonFileService(), new NavigationService()); ;
            var view = new MainWindow { DataContext = viewModel };
            view.ShowDialog();
        }
    }
}
