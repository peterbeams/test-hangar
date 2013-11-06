using System.Collections.Generic;

namespace TestHangar.Model
{
    public class Configuration
    {
        public string Id { get; set; }
    }

    public class Feature
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Scenario> Scenarios { get; set; }
        public string TestSuiteId { get; set; }
    }
}