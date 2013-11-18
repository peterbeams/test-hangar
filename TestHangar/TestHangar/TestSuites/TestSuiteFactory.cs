using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHangar.Databases;
using TestHangar.Model;

namespace TestHangar.TestSuites
{
    public class TestSuiteImporter
    {
        public void Import(string testSuiteId, string path)
        {
            var reader = new GherkinFeatureReader();
            var raven = new RavenClient();

            raven.Store(new TestLibrary()
                {
                    Id = testSuiteId
                });

            ProcessTestSuite(reader, raven, path, testSuiteId);
        }
        
        private static void ProcessTestSuite(GherkinFeatureReader reader, RavenClient raven, string path, string testSuiteId)
        {
            foreach (var file in Directory.GetFiles(path, "*.feature"))
            {
                var source = File.ReadAllText(file);
                var model = reader.Read(source);
                model.TestSuiteId = testSuiteId;
                raven.Store(model);
            }

            foreach (var folder in Directory.GetDirectories(path))
            {
                ProcessTestSuite(reader, raven, folder, testSuiteId);
            }
        }
    }
}
