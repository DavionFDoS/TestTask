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
        public MainWindow()
        {
            InitializeComponent();

            List<ComboBoxItem> comboBoxItems = new List<ComboBoxItem>
            {
                new ComboBoxItem { Content = "Простая строка" },
                new ComboBoxItem { Content = "Строка с историей" },
                new ComboBoxItem { Content = "Значение из списка" },
                new ComboBoxItem { Content = "Набор значений из списка" }
        };
            ComboBox comboBox = new();
            comboBox.ItemsSource = comboBoxItems;

            List<AdditionalParameter> additionalParameters = new()
            {
                new AdditionalParameter { Title = "Параметр 1", Type = comboBoxItems[0].Content.ToString(), List = "Список значений" },
                new AdditionalParameter { Title = "Параметр 2", Type = comboBoxItems[1].Content.ToString(), List = "Список значений" },
                new AdditionalParameter { Title = "Параметр 3", Type = comboBoxItems[2].Content.ToString(), List = "Список значений" },
                new AdditionalParameter { Title = "Параметр 4", Type = comboBoxItems[3].Content.ToString(), List = "Список значений" }
            };
            grid_AdditionalParameters.ItemsSource = additionalParameters;
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

        }
    }
}
