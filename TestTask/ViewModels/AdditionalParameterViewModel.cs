using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
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
            StringList = parameter.ValuesList != null
                ? new ObservableCollection<Values>(parameter.ValuesList)
                : new ObservableCollection<Values>();
            StringListBefore = parameter.CloneList(parameter.ValuesList);
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

        /// <summary>
        /// Список значений 
        /// </summary>
        public ObservableCollection<Values> StringList { get; set; }

        /// <summary>
        /// Копия списка значений для отката изменений
        /// </summary>
        private IList<Values> StringListBefore;

        /// <summary>
        /// Команда открытия окна редактирования списка значений
        /// </summary>
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

        /// <summary>
        /// Команда перемещения значения вверх
        /// </summary>
        private ICommand moveUpValueCommand;
        public ICommand MoveUpValueCommand
        {
            get
            {
                return moveUpValueCommand ??= new RelayCommand(obj =>
                {

                    int currentIndex = StringList.IndexOf((Values)obj);
                    StringList.Move(currentIndex, currentIndex - 1);

                },
                    (obj) => obj is Values parameter && StringList?.Count > 0 && (Values)obj != StringList?.First() && obj != null);
            }
        }

        /// <summary>
        /// Команда перемещения значения вниз
        /// </summary>
        private ICommand moveDownValueCommand;
        public ICommand MoveDownValueCommand
        {
            get
            {
                return moveDownValueCommand ??= new RelayCommand(obj =>
                {
                    int currentIndex = StringList.IndexOf((Values)obj);
                    StringList.Move(currentIndex, currentIndex + 1);
                },
                    (obj) => obj is Values parameter && StringList?.Count > 0 && (Values)obj != StringList?.Last() && obj != null);
            }
        }

        /// <summary>
        /// Команда сохранения сделанных изменений
        /// </summary>
        private ICommand okndCloseCommand;
        public ICommand OkndCloseCommand
        {
            get
            {
                return okndCloseCommand ??= new RelayCommand(obj =>
                {
                    Model.ValuesList = (List<Values>)parameter.CloneList(StringList);
                    StringListBefore = parameter.CloneList(StringList);
                    navigation.ExitFromEditListWindow();
                });
            }
        }
        /// <summary>
        /// Команда отмены изменений
        /// </summary>
        private ICommand cancelCommand;
        public ICommand CancelCommand //peredelat'
        {
            get
            {
                return cancelCommand ??= new RelayCommand(obj =>
                {
                    //StringList.Clear();
                    //foreach (var s in StringListBefore) //Rem: расточительно: очищать, а потом добавлять поэлементно!    что делать?: изменить целиком список + нотификация
                    //    StringList.Add(new Values { Name = (string)s.Name.Clone() });
                    RollBack(StringList, new ObservableCollection<Values>(StringListBefore));
                    //StringList = new ObservableCollection<Values>(StringListBefore.Select(value => new Values { Name = value.Name}));
                });
            }
        }      

        /// <summary>
        /// Команда добавления нового значения в список
        /// </summary>
        private ICommand addValueCommand;
        public ICommand AddValueCommand
        {
            get
            {
                return addValueCommand ??= new RelayCommand(obj =>
                {
                    StringList.Insert(StringList.Count, new Values { Name = "Value " + (StringList.Count + 1) });
                },
                (obj) => parameter.Type == AdditionalParameterType.ListValueSet || parameter.Type == AdditionalParameterType.ListValue);
            }
        }
        /// <summary>
        /// Команда удаления выбранного значения из списка
        /// </summary>
        private ICommand removeValueCommand;
        public ICommand RemoveValueCommand
        {
            get
            {
                return removeValueCommand ??= new RelayCommand(obj =>
                {
                    if (dialogService.ShowMessage("Вы действительно хотите удалить это значение?", "Удалить", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        StringList.Remove((Values)obj);
                },
                    (obj) => obj is Values parameter && StringList?.Count > 0 && obj != null);
            }
        }
    }
}
