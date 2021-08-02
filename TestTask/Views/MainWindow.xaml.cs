using System;
using System.Windows;

namespace TestTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DataContext = new ApplicationViewModel(new DialogService(), new JsonFileService(), new Services.NavigationService());
        }
    }
}
