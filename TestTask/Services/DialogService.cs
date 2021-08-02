using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestTask.Services
{
    /// <summary>
    /// Класс, реализующий интерфейс IDialogService
    /// </summary>
    public class DialogService : IDialogService
    {
        public string FilePath { get; set; }
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
        public MessageBoxResult ShowMessage(string message, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            MessageBoxResult result;
            result = MessageBox.Show(message, caption, button, icon);
            return result;
        }
        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }


    }
}
