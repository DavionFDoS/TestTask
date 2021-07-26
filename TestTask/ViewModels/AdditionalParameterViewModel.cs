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
        private readonly IDialogService dialogService;

        public AdditionalParameterViewModel(AdditionalParameter parameter, INavigationService navigation, IDialogService dialogService)
        {
            this.parameter = parameter;
            this.navigation = navigation;
            this.dialogService = dialogService;
            stringList = parameter.ParametersList != null
                ? new ObservableCollection<string>(parameter.ParametersList)
                : new ObservableCollection<string>();
            stringListBefore = parameter.ParametersList != null
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

        private ObservableCollection<string> stringList;

        public ObservableCollection<string> StringList => stringList;
        private ObservableCollection<string> stringListBefore;

        public ObservableCollection<string> StringListBefore => stringListBefore;

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

        // команда перемещения вверх
        private ICommand moveUpInWindowCommand;
        public ICommand MoveUpInWindowCommand
        {
            get
            {
                return moveUpInWindowCommand ??= new RelayCommand(obj =>
                {
                    if (obj is string parameter)
                    {
                        int currentIndex = stringList.IndexOf(parameter);
                        stringList.Move(currentIndex, currentIndex - 1);
                    }

                },
                    (obj) => (string)obj != stringList?.First() && stringList.Count > 0);
            }
        }

        // команда перемещения вниз
        private ICommand moveDownInWindowCommand;
        public ICommand MoveDownInWindowCommand
        {
            get
            {
                return moveDownInWindowCommand ??= new RelayCommand(obj =>
                {
                    if (obj is string parameter)
                    {
                        int currentIndex = stringList.IndexOf(parameter);
                        stringList.Move(currentIndex, currentIndex + 1);
                    }
                },
                    (obj) => (string)obj != stringList?.Last() && stringList.Count > 0);
            }
        }

        // команда сохранения изменений
        private ICommand okndCloseInWindowCommand;
        public ICommand OkndCloseInWindowCommand
        {
            get
            {
                return okndCloseInWindowCommand ??= new RelayCommand(obj =>
                {

                });
            }
        }
        // команда отмены изменений
        private ICommand cancelInWindowCommand;
        public ICommand CancelInWindowCommand
        {
            get
            {
                return cancelInWindowCommand ??= new RelayCommand(obj =>
                {
                    try
                    {
                        stringList.Clear();
                        foreach (var s in stringListBefore)
                            stringList.Add(s);
                    }
                    catch (Exception ex)
                    {
                        dialogService.ShowMessage(ex.Message);
                    }
                });
            }
        }

        // команда добавления нового объекта
        private ICommand addInWindowCommand;
        public ICommand AddInWindowCommand
        {
            get
            {
                return addInWindowCommand ??= new RelayCommand(obj =>
                {
                    stringList.Insert(0, new string("Значение " + stringList.Count));
                },
                (obj) => parameter.Type == AdditionalParameterType.ListValueSet);
            }
        }
        // команда удаления выбранного элемента
        private ICommand removeInWindowCommand;
        public ICommand RemoveInWindowCommand
        {
            get
            {
                return removeInWindowCommand ??= new RelayCommand(obj =>
                {
                    if (obj is string parameter)
                        stringList.Remove(parameter);
                },
                    (obj) => (stringList.Count > 0) && parameter.Type == AdditionalParameterType.ListValueSet);
            }
        }
    }
}
