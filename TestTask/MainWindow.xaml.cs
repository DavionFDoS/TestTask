using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestTask.Models;

namespace TestTask
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> Types { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Types = new List<string>
            {
                "Простая строка",
                "Строка с историей",
                "Значение из списка",
                "Набор значений из списка"
            };
            comboBox_Type.ItemsSource = Types;
            DataContext = new ApplicationViewModel(new DefaultDialogService(), new JsonFileService());
        }
    }
}
