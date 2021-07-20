using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestTask.Models;
using TestTask.Services;

namespace TestTask.ViewModels
{
    public class AdditionalParameterViewModel : BaseViewModel
    {
        private readonly AdditionalParameter parameter;
        private readonly INavigationService navigation;
        private ObservableCollection<string> stringList;
        public ObservableCollection<string> StringList { get; set; }
        public AdditionalParameterViewModel(AdditionalParameter parameter, INavigationService navigation)
        {
            this.parameter = parameter;
            this.navigation = navigation;
            stringList = parameter.ParametersList != null
                ? new ObservableCollection<string>(parameter.ParametersList)
                : new ObservableCollection<string>();
        }

        public AdditionalParameter Model => parameter;

        public string Title
        {
            get => parameter.Title;
            set
            {
                if (parameter.Title != value)
                {
                    parameter.Title = value;
                    RaisePropertyChanged();
                }
            }
        }

        public AdditionalParameterType ParameterType
        {
            get => parameter.Type;
            set
            {
                if (parameter.Type != value)
                {
                    parameter.Type = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ICommand showListWindowCommand;
        public ICommand ShowListWindowCommand 
        {
            get
            {
                return showListWindowCommand ??= new RelayCommand(obj =>
                {
                    navigation.NavigateTo(this);
                },
                    (obj) => ParameterType == AdditionalParameterType.ListValue || ParameterType == AdditionalParameterType.ListValueSet);
            }
        }
    }
}
