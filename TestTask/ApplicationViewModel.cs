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
        IDialogService dialogService;
        public ObservableCollection<AdditionalParameter> AdditionalParameters { get; set; }
        public ObservableCollection<AdditionalParameter> JsonData { get; set; }
        public AdditionalParameter SelectedParameter
        {
            get { return selectedParameter; }
            set
            {
                selectedParameter = value;
                OnPropertyChanged(nameof(SelectedParameter));
            }
        }

        public static AdditionalParameterType[] AdditionalParameterTypes => Enum.GetValues<AdditionalParameterType>();

        public static EnumToStringConverter<AdditionalParameterType> TypeToStringConverter { get; } =
            new EnumToStringConverter<AdditionalParameterType>(
                (AdditionalParameterType.String, "Простая строка"),
                (AdditionalParameterType.StringWithHystory, "Строка с историей"),
                (AdditionalParameterType.ListValue, "Значение из списка"),
                (AdditionalParameterType.ListValueSet, "Набор значений из списка")
                );

        // команда добавления нового объекта
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??= new RelayCommand(obj =>
                  {
                      AdditionalParameter additionalParameter = new AdditionalParameter();
                      AdditionalParameters.Insert(0, additionalParameter);
                      SelectedParameter = additionalParameter;
                  });
            }
        }
        // команда удаления выбранного элемента
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??= new RelayCommand(obj =>
                    {                   
                        if (selectedParameter != null)
                        {
                            AdditionalParameters.Remove(selectedParameter);
                        }
                    },
                    (obj) => AdditionalParameters.Count > 0);
            }
        }
        // команда сохранения файла
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??= new RelayCommand(obj =>
                  {
                      try
                      {
                          //if (dialogService.SaveFileDialog() == true)
                          //{
                              fileService.Save(dialogService.FilePath, AdditionalParameters);
                              dialogService.ShowMessage("Изменения сохранены");
                          //}
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  });
            }
        }
        // команда открытия файла
        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??= new RelayCommand(obj =>
                  {
                      try
                      {
                          //if (dialogService.OpenFileDialog() == true)
                          //{
                              var additionalParameters = fileService.Open(dialogService.FilePath);
                              AdditionalParameters.Clear();
                              foreach (var parameter in additionalParameters)
                                  AdditionalParameters.Add(parameter);
                              dialogService.ShowMessage("Изменения отменены");
                          //}
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  });
            }
        }

        // команда перемещения вверх
        private RelayCommand moveUpCommand;
        public RelayCommand MoveUpCommand
        {
            get
            {
                return moveUpCommand ??= new RelayCommand(obj =>
                {
                    if (selectedParameter != null)
                    {
                        int currentIndex = AdditionalParameters.IndexOf(selectedParameter);
                        AdditionalParameters.Move(currentIndex, currentIndex - 1);
                    }
                },
                    (obj) => obj != AdditionalParameters[0]);
            }
        }

        // команда перемещения вниз
        private RelayCommand moveDownCommand;
        public RelayCommand MoveDownCommand
        {
            get
            {
                return moveDownCommand ??= new RelayCommand(obj =>
                {
                    if (selectedParameter != null)
                    {
                        int currentIndex = AdditionalParameters.IndexOf(selectedParameter);
                        AdditionalParameters.Move(currentIndex, currentIndex + 1);
                    }
                },
                    (obj) => obj != AdditionalParameters[AdditionalParameters.Count - 1]);
            }
        }

        // команда показа окна списка
        private RelayCommand showCommand;
        public RelayCommand ShowCommand
        {
            get
            {
                return showCommand ??= new RelayCommand(obj =>
                {
                    if (selectedParameter != null)
                    {         
                            ShowMethod();
                    }
                },
                    (obj) => 
                    { 
                        bool canBeShown = selectedParameter.Type is AdditionalParameterType.ListValue or AdditionalParameterType.ListValueSet;
                        return canBeShown;
                    });
            }
        }

        private void ShowMethod()
        {
            ListWindow listWindow = new ListWindow();
            listWindow.Show();
        }

        public ApplicationViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.fileService = fileService;
            this.dialogService = dialogService;
            dialogService.FilePath = @"C:\Users\Matvey\source\repos\TestTask\TestTaskParametersData.json";

            JsonData = fileService.Open(dialogService.FilePath);
            AdditionalParameters = JsonData;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
