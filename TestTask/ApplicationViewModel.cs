using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestTask.Models;
using System.Windows;
using System.Windows.Controls;

namespace TestTask
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private AdditionalParameter selectedParameter;
        IFileService fileService;
        public ObservableCollection<AdditionalParameter> AdditionalParameters { get; set; }
        public AdditionalParameter SelectedParameter
        {
            get { return selectedParameter; }
            set
            {
                selectedParameter = value;
                OnPropertyChanged("AdditionalParameter");
            }
        }

        // команда добавления нового объекта
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      AdditionalParameter additionalParameter = new AdditionalParameter();
                      AdditionalParameters.Insert(0, additionalParameter);
                      SelectedParameter = additionalParameter;
                  }));
            }
        }
        public ApplicationViewModel(IFileService fileService)
        {
            this.fileService = fileService;
            AdditionalParameters = new ObservableCollection<AdditionalParameter>
            {
                new AdditionalParameter {Title="Параметр 1", Type ="Простая строка"},
                new AdditionalParameter {Title="Параметр 2", Type="Строка с историей"},
                new AdditionalParameter {Title="Параметр 3", Type="Значение из списка"},
                new AdditionalParameter {Title="Параметр 4", Type="Набор значений из списка"}
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
