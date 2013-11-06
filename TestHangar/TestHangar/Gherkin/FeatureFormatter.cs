using System.Collections.Generic;
using System.Linq;
using gherkin.formatter.model;
using java.io;
using java.lang;
using java.util;
using Formatter = gherkin.formatter.Formatter;

namespace TestHangar
{
    public class FeatureFormatter : Formatter
    {
        public FeatureFormatter()
        {
            Model = new Model.Feature { Scenarios = new List<Model.Scenario>() };
        }

        public Model.Feature Model { get; private set; }
        private Model.Scenario CurrentScenario { get; set; }

        public void Dispose()
        {

        }

        public void uri(string str)
        {

        }

        public void eof()
        {

        }

        public void syntaxError(string str1, string str2, List l, string str3, Integer i)
        {

        }

        public void feature(Feature f)
        {
            Model.Name = f.getName();
            Model.Description = f.getDescription();
        }

        public void scenarioOutline(ScenarioOutline so)
        {
            CurrentScenario = new Model.Scenario()
            {
                Name = so.getName(),
                Description = so.getDescription(),
                Steps = new List<Model.Step>()
            };
            Model.Scenarios.Add(CurrentScenario);
        }

        public void examples(Examples e)
        {

        }

        public void startOfScenarioLifeCycle(Scenario s)
        {

        }

        public void background(Background b)
        {
            
        }

        public void scenario(Scenario s)
        {
            CurrentScenario = new Model.Scenario()
                {
                    Name = s.getName(),
                    Description = s.getDescription(),
                    Steps = new List<Model.Step>(),
                    Tags = s.getTags().toArray().Select(o => ((Tag)o).getName()).ToArray()
                };
            Model.Scenarios.Add(CurrentScenario);
        }

        public void step(Step s)
        {
            if (CurrentScenario == null)
                return;

            CurrentScenario.Steps.Add(new Model.Step
                {
                    Name = s.getName(),
                    Keyword = s.getKeyword().Trim()
                });
        }

        public void endOfScenarioLifeCycle(Scenario s)
        {
            
        }

        public void done()
        {

        }

        void Formatter.close()
        {

        }

        void Closeable.close()
        {

        }
    }
}