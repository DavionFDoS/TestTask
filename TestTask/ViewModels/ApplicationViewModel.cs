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
using System.Windows.Input;
using TestTask.Services;
using System.IO;

namespace TestTask.ViewModels
{
    public class ApplicationViewModel : BaseViewModel
    {
        private readonly INavigationService navigation;
        private readonly IFileService fileService;
        private readonly IDialogService dialogService;
        public ObservableCollection<AdditionalParameterViewModel> AdditionalParameters { get; set; }
        public IList<AdditionalParameterViewModel> AdditionalParametersBackUp { get; set; }
        public static AdditionalParameterType[] AdditionalParameterTypes => Enum.GetValues<AdditionalParameterType>();

        public static EnumToStringConverter<AdditionalParameterType> TypeToStringConverter { get; } =
            new EnumToStringConverter<AdditionalParameterType>(
                (AdditionalParameterType.String, "Простая строка"),
                (AdditionalParameterType.StringWithHystory, "Строка с историей"),
                (AdditionalParameterType.ListValue, "Значение из списка"),
                (AdditionalParameterType.ListValueSet, "Набор значений из списка")
                );

        /// <summary>
        /// Команда добавления нового дополнительного параметра
        /// </summary>
        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                return addCommand ??= new RelayCommand(obj =>
                  {
                      AdditionalParameters.Insert(0, new AdditionalParameterViewModel(
                          new AdditionalParameter
                          {
                              ValuesList = new List<Values>(),
                              Title = "Added Parameter",
                              Type = AdditionalParameterType.String
                          },
                          navigation,
                          dialogService));
                  });
            }
        }
        /// <summary>
        /// Команда удаления выбранного дополнительного параметра
        /// </summary>
        private ICommand removeCommand;
        public ICommand RemoveCommand
        {
            get
            {
                return removeCommand ??= new RelayCommand(obj =>
                    {
                        if (dialogService.ShowMessage("Вы действительно хотите удалить это значение?", "Удалить", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                            AdditionalParameters.Remove((AdditionalParameterViewModel)obj);
                    },
                    (obj) => obj is AdditionalParameterViewModel parameter && AdditionalParameters?.Count > 0 && obj != null);
            }
        }
        /// <summary>
        /// Команда сохранения изменений в файл
        /// </summary>
        private ICommand saveChangesCommand;
        public ICommand SaveChangesCommand
        {
            get
            {
                return saveChangesCommand ??= new RelayCommand(obj =>
                  {
                      fileService.Save(dialogService.FilePath, AdditionalParameters.Select(vm => vm.Model).ToList());
                      dialogService.ShowMessage("Изменения были успешно сохранены");
                      navigation.ExitFromApplication();
                  });
            }
        }
        /// <summary>
        /// Метод загрузки данных из файла
        /// </summary>
        private ObservableCollection<AdditionalParameterViewModel> InitializeData()
        {
            return new ObservableCollection<AdditionalParameterViewModel>(
                fileService.Open(dialogService.FilePath).Select(m => new AdditionalParameterViewModel(m, navigation, dialogService)));
        }
        /// <summary>
        /// Команда загрузки последних сохраненных данных
        /// </summary>
        private ICommand loadDataCommand;
        public ICommand LoadDataCommand
        {
            get
            {
                return loadDataCommand ??= new RelayCommand(obj =>
                  {
                      try
                      {
                          //AdditionalParameters = InitializeData();
                          RollBack(AdditionalParameters, new ObservableCollection<AdditionalParameterViewModel>(AdditionalParametersBackUp));
                          dialogService.ShowMessage("Изменения отменены");
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  });
            }
        }

        /// <summary>
        /// Команда перемещения дополнительного параметра вверх
        /// </summary>
        private ICommand moveUpCommand;
        public ICommand MoveUpCommand
        {
            get
            {
                return moveUpCommand ??= new RelayCommand(obj =>
                {
                    int currentIndex = AdditionalParameters.IndexOf((AdditionalParameterViewModel)obj);
                    AdditionalParameters.Move(currentIndex, currentIndex - 1);
                },
                    (obj) => obj is AdditionalParameterViewModel && AdditionalParameters?.Count > 0 && obj != AdditionalParameters?.First());
            }
        }

        /// <summary>
        /// Команда перемещения дополнительного параметра вниз
        /// </summary>
        private ICommand moveDownCommand;
        public ICommand MoveDownCommand
        {
            get
            {
                return moveDownCommand ??= new RelayCommand(obj =>
                {
                    int currentIndex = AdditionalParameters.IndexOf((AdditionalParameterViewModel)obj);
                    AdditionalParameters.Move(currentIndex, currentIndex + 1);
                },
                    (obj) => obj is AdditionalParameterViewModel && AdditionalParameters?.Count > 0 && obj != AdditionalParameters?.Last());
            }
        }

        public ApplicationViewModel(IDialogService dialogService, IFileService fileService, INavigationService navigation)
        {
            this.fileService = fileService;
            this.dialogService = dialogService;
            this.navigation = navigation;
            dialogService.FilePath = @"TestTaskParametersData.json";
            if (File.Exists(dialogService.FilePath))
            {
                AdditionalParameters = InitializeData();
                AdditionalParametersBackUp = InitializeData().ToList();
            }
            else
            {
                File.Create(dialogService.FilePath);
                AdditionalParameters = new ObservableCollection<AdditionalParameterViewModel>();
            }
        }
    }
}
