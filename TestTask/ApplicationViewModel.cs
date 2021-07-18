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
                return addCommand ??= new RelayCommand(obj =>
                  {
                      AdditionalParameter additionalParameter = new AdditionalParameter();
                      AdditionalParameters.Insert(0, additionalParameter);
                      SelectedParameter = additionalParameter;
                  });
            }
        }

        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??= new RelayCommand(obj =>
                    {
                        AdditionalParameter additionalParameter = obj as AdditionalParameter;
                        if (additionalParameter != null)
                        {
                            AdditionalParameters.Remove(additionalParameter);
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
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.SaveFileDialog() == true)
                          {
                              fileService.Save(dialogService.FilePath, AdditionalParameters.ToList());
                          }
                      }
                      catch (Exception)
                      {
                          throw;
                      }
                  }));
            }
        }
        // команда открытия файла
        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              var additionalParameters = fileService.Open(dialogService.FilePath);
                              AdditionalParameters.Clear();
                              foreach (var parameter in additionalParameters)
                                  AdditionalParameters.Add(parameter);
                          }
                      }
                      catch (Exception)
                      {
                          throw;
                      }
                  }));
            }
        }
        public ApplicationViewModel(IDialogService dialogService, IFileService fileService)
        {
            this.fileService = fileService;
            this.dialogService = dialogService;
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
