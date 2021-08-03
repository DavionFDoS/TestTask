using System.Windows;
using TestTask.Services;
using TestTask.ViewModels;

namespace TestTask
{
    /// <summary>
    /// Логика взаимодействия App.xaml
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
