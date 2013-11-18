using System;
using System.Linq;
using Newtonsoft.Json;
using TestHangar.Model.RunResults;

namespace TestHangar.Cucumber
{
    public class RunResultsReader
    {
        public RunResult Load(string path)
        {
            var data = JsonConvert.DeserializeObject<RunResult[]>(System.IO.File.ReadAllText(path)).Single();
            data.id = Guid.NewGuid().ToString();
            data.date = DateTime.Now;
            return data;
        }
    }
}