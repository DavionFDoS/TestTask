using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TestTask.Models
{
    public class AdditionalParameter : INotifyPropertyChanged
    {
        private string title;
        private AdditionalParameterType type;
        private ObservableCollection<string> parametersList;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public AdditionalParameterType Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        public ObservableCollection<string> ParametersList
        {
            get { return parametersList; }
            set
            {
                parametersList.Add(value.ToString());
                OnPropertyChanged(nameof(ParametersList));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
