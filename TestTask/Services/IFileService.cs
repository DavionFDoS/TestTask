using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Models;

namespace TestTask
{
    public interface IFileService
    {
        ObservableCollection<AdditionalParameter> Open(string filename);
        void Save(string filename, ObservableCollection<AdditionalParameter> additionalParameters);
    }
}
