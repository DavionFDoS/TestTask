using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestTask
{
    public interface IDialogService
    {
        /// <summary>
        /// Метод показа простейшего сообщения, переданного в параметр message
        /// </summary>
        /// <param name="message"></param>
        void ShowMessage(string message);
        /// <summary>
        /// Метод показа сообщения, переданного в параметр message с кнопками и иконкой
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caption"></param>
        /// <param name="button"></param>
        /// <param name="icon"></param>
        MessageBoxResult ShowMessage(string message, string caption, MessageBoxButton button, MessageBoxImage icon);
        /// <summary>
        /// Путь расположения файла
        /// </summary>
        string FilePath { get; set; }
        /// <summary>
        /// // Открытие файла
        /// </summary>
        /// <returns></returns>
        bool OpenFileDialog();
        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <returns></returns>
        bool SaveFileDialog();
    }
}
