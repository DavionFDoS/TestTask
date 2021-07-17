using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using TestTask.Models;

namespace TestTask
{
    public class JsonFileService : IFileService
    {
        public List<AdditionalParameter> Open(string filename)
        {
            List<AdditionalParameter> additionalParameters = new List<AdditionalParameter>();
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(List<AdditionalParameter>));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                additionalParameters = jsonFormatter.ReadObject(fs) as List<AdditionalParameter>;
            }

            return additionalParameters;
        }

        public void Save(string filename, List<AdditionalParameter> phonesList)
        {
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(List<AdditionalParameter>));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, phonesList);
            }
        }
    }
}
