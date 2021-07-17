using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Models;

namespace TestTask
{
    public interface IFileService
    {
        List<AdditionalParameter> Open(string filename);
        void Save(string filename, List<AdditionalParameter> additionalParameters);
    }
}
