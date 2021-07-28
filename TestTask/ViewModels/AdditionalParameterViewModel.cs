using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            stringList = parameter.ValuesList != null
                ? new ObservableCollection<Values>(parameter.ValuesList)
                : new ObservableCollection<Values>();
            stringListBefore = Clone(stringList);
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
        // лист значений
        private ObservableCollection<Values> stringList;

        public ObservableCollection<Values> StringList => stringList;

        // Копия листа значений для отката изменений
        private readonly IList<Values> stringListBefore;

        // команда открытия окна редактирования списка
        private ICommand showEditListWindowCommand;
        public ICommand ShowEditListWindowCommand
        {
            get
            {
                return showEditListWindowCommand ??= new RelayCommand(obj =>
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
                    if (obj is Values parameter)
                    {
                        int currentIndex = stringList.IndexOf(parameter);
                        stringList.Move(currentIndex, currentIndex - 1);
                    }

                },
                    (obj) => stringList.Count > 0 && (Values)obj != stringList?.First());
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
                    if (obj is Values parameter)
                    {
                        int currentIndex = stringList.IndexOf(parameter);
                        stringList.Move(currentIndex, currentIndex + 1);
                    }
                },
                    (obj) => stringList.Count > 0 && (Values)obj != stringList?.Last());
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
                    if (obj is EditListWindow window)
                    {
                        window.Close();
                    }
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
                        stringList.Clear();
                        foreach (var s in stringListBefore)
                            stringList.Add(s);          
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
                    stringList.Insert(stringList.Count, new Values { Name = "Value " + stringList.Count });
                },
                (obj) => parameter.Type == AdditionalParameterType.ListValueSet || parameter.Type == AdditionalParameterType.ListValue);
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
                    if (MessageBox.Show("Вы действительно хотите удалить это значение?", "Удалить", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (obj is Values parameter)
                            stringList.Remove(parameter);
                    }
                    
                },
                    (obj) => stringList.Count > 0 && obj != null);
            }
        }
    }
}
