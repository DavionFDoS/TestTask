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
            Types = new List<string>();
            Types.Add("Простая строка");
            Types.Add("Строка с историей");
            Types.Add("Значение из списка");
            Types.Add("Набор значений из списка");
            DataContext = new ApplicationViewModel(new JsonFileService());
            comboBox_Type.ItemsSource = Types;
        }

        private void Button_Ok_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Up_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Down_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Remove_Click(object sender, RoutedEventArgs e)
        {
            //var selectedItems = grid_AdditionalParameters.SelectedItems;
            //if(selectedItems != null)
            //{
            //    grid_AdditionalParameters.Items.Remove(selectedItems);
            //}
        }
    }
}
