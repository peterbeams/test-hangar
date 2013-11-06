using System.Collections.Generic;

namespace TestHangar.Model
{
    public class Scenario
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] Tags { get; set; }
        public List<Step> Steps { get; set; } 
    }
}