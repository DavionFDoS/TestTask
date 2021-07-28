﻿using System;
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
        //private int indexOfChosenParameter;
        public ObservableCollection<AdditionalParameterViewModel> AdditionalParameters { get; set; }
        public static AdditionalParameterType[] AdditionalParameterTypes => Enum.GetValues<AdditionalParameterType>();

        public static EnumToStringConverter<AdditionalParameterType> TypeToStringConverter { get; } =
            new EnumToStringConverter<AdditionalParameterType>(
                (AdditionalParameterType.String, "Простая строка"),
                (AdditionalParameterType.StringWithHystory, "Строка с историей"),
                (AdditionalParameterType.ListValue, "Значение из списка"),
                (AdditionalParameterType.ListValueSet, "Набор значений из списка")
                );

        // команда добавления нового объекта
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
        // команда удаления выбранного элемента
        private ICommand removeCommand;
        public ICommand RemoveCommand
        {
            get
            {
                return removeCommand ??= new RelayCommand(obj =>
                    {
                        if (MessageBox.Show("Вы действительно хотите удалить этот параметр?", "Удалить", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            if (obj is AdditionalParameterViewModel parameter)
                                AdditionalParameters.Remove(parameter);
                        }
                    },
                    (obj) => AdditionalParameters.Count > 0 && obj != null);
            }
        }
        // команда сохранения файла
        private ICommand saveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ??= new RelayCommand(obj =>
                  {
                      try
                      {
                          fileService.Save(dialogService.FilePath, AdditionalParameters.Select(vm => vm.Model).ToList());
                          dialogService.ShowMessage("Изменения сохранены");
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  });
            }
        }

        private ObservableCollection<AdditionalParameterViewModel> InitializeData()
        {
            var additionalParameters = fileService.Open(dialogService.FilePath);
            IList<AdditionalParameterViewModel> vm = new List<AdditionalParameterViewModel>();
            foreach (var parameter in additionalParameters)
                vm.Add(new AdditionalParameterViewModel(
                    parameter, navigation, dialogService));
            return new ObservableCollection<AdditionalParameterViewModel>(vm);
        }
        // команда открытия файла
        private ICommand openCommand;
        public ICommand OpenCommand
        {
            get
            {
                return openCommand ??= new RelayCommand(obj =>
                  {
                      try
                      {
                          //AdditionalParameters.Clear();
                          //AdditionalParameters = InitializeData();
                          
                          var additionalParameters = fileService.Open(dialogService.FilePath); //Rem: почему не сделано как в конструкторе и через общий метод? или вызов OpenCommand.Execute()
                          AdditionalParameters.Clear();
                          foreach (var parameter in additionalParameters)
                              AdditionalParameters.Add(new AdditionalParameterViewModel(
                              parameter, navigation, dialogService));
                          dialogService.ShowMessage("Изменения отменены");
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  });
            }
        }

        // команда перемещения вверх
        private ICommand moveUpCommand;
        public ICommand MoveUpCommand
        {
            get
            {
                return moveUpCommand ??= new RelayCommand(obj =>
                {
                    if (obj is AdditionalParameterViewModel parameter)
                    {
                        int currentIndex = AdditionalParameters.IndexOf(parameter);
                        AdditionalParameters.Move(currentIndex, currentIndex - 1);
                    }

                },
                    (obj) => obj is AdditionalParameterViewModel && AdditionalParameters.Count > 0 && obj != AdditionalParameters?.First());
            }
        }

        // команда перемещения вниз
        private ICommand moveDownCommand;
        public ICommand MoveDownCommand
        {
            get
            {
                return moveDownCommand ??= new RelayCommand(obj =>
                {
                    if (obj is AdditionalParameterViewModel parameter)
                    {
                        int currentIndex = AdditionalParameters.IndexOf(parameter);
                        AdditionalParameters.Move(currentIndex, currentIndex + 1);
                    }
                },
                    (obj) => obj is AdditionalParameterViewModel && AdditionalParameters.Count > 0 && obj != AdditionalParameters?.Last());
            }
        }

        public ApplicationViewModel(IDialogService dialogService, IFileService fileService, INavigationService navigation)
        {
            this.fileService = fileService;
            this.dialogService = dialogService;
            this.navigation = navigation;
            dialogService.FilePath = @"TestTaskParametersData.json";
            //AdditionalParameters = new ObservableCollection<AdditionalParameterViewModel>
            //{
            //    new AdditionalParameterViewModel(new AdditionalParameter{ValuesList = new List<Values>{ new Values{Name = "Value 1"}, new Values{Name = "Value 2"}, new Values{Name = "Value 3"} }, Title = "Par1", Type = AdditionalParameterType.String }, navigation, dialogService),
            //    new AdditionalParameterViewModel(new AdditionalParameter{ValuesList = new List<Values>{ new Values{Name = "Value 1"}, new Values{Name = "Value 2"}, new Values{Name = "Value 3"} }, Title = "Par2", Type = AdditionalParameterType.StringWithHystory}, navigation, dialogService),
            //    new AdditionalParameterViewModel(new AdditionalParameter{ValuesList = new List<Values>{ new Values{Name = "Value 1"}}, Title = "Par3", Type = AdditionalParameterType.ListValue }, navigation, dialogService),
            //    new AdditionalParameterViewModel(new AdditionalParameter{ValuesList = new List<Values>{ new Values{Name = "Value 1"}, new Values{Name = "Value 2"}, new Values{Name = "Value 3"} }, Title = "Par4", Type = AdditionalParameterType.ListValueset }, navigation, dialogService)
            //};
            if (File.Exists(dialogService.FilePath))
            {
                AdditionalParameters = new ObservableCollection<AdditionalParameterViewModel>(
                    fileService.Open(dialogService.FilePath).Select(m => new AdditionalParameterViewModel(m, navigation, dialogService)));
                //AdditionalParameters = InitializeData();
            }
            else
            {
                dialogService.ShowMessage("File doesn't exist");
            }
        }
    }
}
