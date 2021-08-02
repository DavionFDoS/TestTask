using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Models;
using TestTask.ViewModels;

namespace TestTask
{
    public interface IFileService
    {
        /// <summary>
        /// Открытие и чтение из файла по указанному пути
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        ObservableCollection<AdditionalParameter> Open(string filename);
        /// <summary>
        /// Сохранение в файл по указанному пути
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="additionalParameters"></param>
        void Save(string filename, IList<AdditionalParameter> additionalParameters);

        //void SaveList(string filename, IList<string> list);
    }
}
