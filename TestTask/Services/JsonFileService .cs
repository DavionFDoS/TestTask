using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using TestTask.Models;
using TestTask.ViewModels;

namespace TestTask
{
    public class JsonFileService : IFileService
    {
        public ObservableCollection<AdditionalParameter> Open(string filename)
        {
            ObservableCollection<AdditionalParameter> additionalParameters = new ObservableCollection<AdditionalParameter>();
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(ObservableCollection<AdditionalParameter>));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                additionalParameters = jsonFormatter.ReadObject(fs) as ObservableCollection<AdditionalParameter>;
            }

            return additionalParameters;
        }

        public void Save(string filename, IList<AdditionalParameter> parametersList)
        {
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(IList<AdditionalParameter>));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, parametersList);
            }
        }
    }
}
