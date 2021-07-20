using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TestTask.Models;

namespace TestTask
{
    public class ListWindowViewModel : INotifyPropertyChanged
    {
        private AdditionalParameter selectedParameter;
        public ObservableCollection<AdditionalParameter> AdditionalParameters { get; set; }
        public AdditionalParameter SelectedParameter
        {
            get { return selectedParameter; }
            set
            {
                selectedParameter = value;
                OnPropertyChanged(nameof(SelectedParameter));
            }
        }
        private static void ShowWindow()
        {
            ListWindow listWindow = new ListWindow();
            listWindow.Show();
        }

        // команда показа окна списка
        private RelayCommand showWindowCommand;
        public RelayCommand ShowWindowCommand
        {
            get
            {
                return showWindowCommand ??= new RelayCommand(obj =>
                {
                    if (selectedParameter != null)
                    {
                        ShowWindow();
                    }
                },
                    (obj) =>
                    {
                        bool canBeShown = selectedParameter.Type is AdditionalParameterType.ListValue or AdditionalParameterType.ListValueSet;
                        return canBeShown;
                    });
            }
        }

        // команда добавления нового объекта в дополнительном окне
        private RelayCommand addInWindowCommand;
        public RelayCommand AddInWindowCommand
        {
            get
            {
                return addInWindowCommand ??= new RelayCommand(obj =>
                {
                    AdditionalParameter additionalParameter = new AdditionalParameter();
                    AdditionalParameters.Insert(0, additionalParameter);
                    SelectedParameter = additionalParameter;
                });
            }
        }
        // команда удаления выбранного элемента в дополнительном окне
        private RelayCommand removeInWindowCommand;
        public RelayCommand RemoveInWindowCommand
        {
            get
            {
                return removeInWindowCommand ??= new RelayCommand(obj =>
                {
                    if (selectedParameter != null)
                    {
                        AdditionalParameters.Remove(selectedParameter);
                    }
                },
                    (obj) => AdditionalParameters.Count > 0);
            }
        }

        // команда перемещения вверх в дополнительном окне
        private RelayCommand moveUpInWindowCommand;
        public RelayCommand MoveUpInWindowCommand
        {
            get
            {
                return moveUpInWindowCommand ??= new RelayCommand(obj =>
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

        // команда перемещения вниз в дополнительном окне
        private RelayCommand moveDownInWindowCommand;
        public RelayCommand MoveDownInWindowCommand
        {
            get
            {
                return moveDownInWindowCommand ??= new RelayCommand(obj =>
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
