using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestTask.Models
{
    /// <summary>
    /// Класс, реализующий интерфейс INotifyPropertyChanged 
    /// </summary>
    public class Observer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
