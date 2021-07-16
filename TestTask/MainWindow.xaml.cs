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

            List<AdditionalParameter> additionalParameters = new List<AdditionalParameter>
            {
                    new AdditionalParameter {Title="Параметр 1", Type="Простая строка", List="Список..." },
                    new AdditionalParameter {Title="Параметр 2", Type="Строка с историей", List="Список..." },
                    new AdditionalParameter {Title="Параметр 3", Type="Значение из списка", List="Список..." },
                    new AdditionalParameter {Title="Параметр 4", Type="Набор значений из списка", List="Список..." }
            };
            grid_AdditionalParameters.ItemsSource = additionalParameters;
            //StackPanel stackPanel = new StackPanel();
            //StackPanel expanderPanel = new StackPanel();
            //expanderPanel.Children.Add(new ComboBoxItem { Content = "Простая строка" });
            //expanderPanel.Children.Add(new ComboBoxItem { Content = "Строка с историей" });
            //expanderPanel.Children.Add(new ComboBoxItem { Content = "Значение из списка" });
            //expanderPanel.Children.Add(new ComboBoxItem { Content = "Набор значений из списка" });

            //ComboBox comboBox = new ComboBox();
            //comboBox.DataContext = expanderPanel;
            //stackPanel.Children.Add(comboBox);
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
