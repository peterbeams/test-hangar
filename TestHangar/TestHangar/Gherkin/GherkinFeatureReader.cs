using gherkin.parser;
using java.lang;

namespace TestHangar
{
    public class GherkinFeatureReader
    {
        public Model.Feature Read(string source)
        {
            var formatter = new FeatureFormatter(); // You implement this
            var parser = new Parser(formatter);

            parser.parse(source, @"c:\file.feature", new Integer(0));

            return formatter.Model;
        }
    }
}