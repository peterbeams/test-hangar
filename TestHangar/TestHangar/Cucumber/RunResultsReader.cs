using System.Linq;
using Newtonsoft.Json;
using TestHangar.Model.RunResults;

namespace TestHangar.Cucumber
{
    public class RunResultsReader
    {
        public RunResult Load(string path)
        {
            return JsonConvert.DeserializeObject<RunResult[]>(System.IO.File.ReadAllText(path)).Single();
        }
    }
}