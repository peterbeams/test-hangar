using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Client.Linq;
using TestHangar.Model;
using TestHangar.Model.RunResults;

namespace TestHangar.Web.UI.Models
{
    public class ScenariosInTag
    {
        public string Tag { get; set; }
        public ScenarioResult[] Scenarios { get; set; }
    }

    public class ScenarioResult
    {
        public Scenario Scenario { get; set; }
        public string FeatureName { get; set; }
    }

    public class ScenariosByTagIndex : AbstractIndexCreationTask<Feature, ScenariosInTag>
    {
       public ScenariosByTagIndex()
        {
            Map = features => from feature in features
                              from scenario in feature.Scenarios
                              from tag in scenario.Tags
                              select new ScenariosInTag { 
                                  Tag = tag, 
                                  Scenarios = new [] { 
                                      new ScenarioResult
                                          {
                                              Scenario = scenario,
                                              FeatureName = feature.Name
                                          }
                                  } 
                              };

            Reduce = results => from result in results
                                group result by result.Tag
                                    into x
                                    select new ScenariosInTag
                                        {
                                            Tag = x.Key,
                                            Scenarios = x.SelectMany(s => s.Scenarios).ToArray()
                                        };

                     
        }
    }

    public class ViewModelStore
    {
        private static readonly Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(() =>
        {
            var store = new DocumentStore { Url = "http://localhost:8080/" };
            store.Initialize();
            var catalog = new CompositionContainer(new AssemblyCatalog(typeof(ViewModelStore).Assembly));
            var dbCmd = store.DatabaseCommands.ForDatabase("test-hangar");
            IndexCreation.CreateIndexes(catalog, dbCmd, store.Conventions);
            return store;
        });

        public IEnumerable<Feature> GetFeatures(string testSuiteId)
        {
            using (var session = store.Value.OpenSession("test-hangar"))
            {
                return session.Query<Feature>().Where(f => f.TestSuiteId.Equals(testSuiteId));
            }
        }

        public Feature GetFeature(string id)
        {
            using (var session = store.Value.OpenSession("test-hangar"))
            {
                return session.Query<Feature>().Where(f => f.Name.Equals(id)).First();
            }
        }

        public ScenariosInTag GetScenariosByTag(string id)
        {
            using (var session = store.Value.OpenSession("test-hangar"))
            {
                return session.Query<ScenariosInTag>("ScenariosByTagIndex")
                              .Where(r => r.Tag.Equals(id))
                              .First();
            }
        }

        public IEnumerable<TestSuite> GetTestSuites()
        {
            using (var session = store.Value.OpenSession("test-hangar"))
            {
                return session.Query<TestSuite>();
            }
        }

        public IEnumerable<Configuration> GetConfigurations()
        {
            using (var session = store.Value.OpenSession("test-hangar"))
            {
                return session.Query<Configuration>();
            }
        }

        public IEnumerable<RunResult> GetResults()
        {
            using (var session = store.Value.OpenSession("test-hangar"))
            {
                return session.Query<RunResult>();
            }
        }

        public RunResult GetResult()
        {
            using (var session = store.Value.OpenSession("test-hangar"))
            {
                return session.Query<RunResult>().First();
            }
        }
    }

    public class CreateTestSuiteViewModel
    {
        public string Id { get; set; }
        public string Path { get; set; }
    }

    public class CreateConfigurationViewModel
    {
        public string Id { get; set; }
    }
}