using System;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;

namespace TestHangar.Databases
{
    public class RavenClient
    {
        private static readonly Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(() =>
            {
                var store = new DocumentStore { Url = "http://localhost:8080/" };
                store.Initialize();
                return store;
            });

        public void Store(object document)
        {
            using (var session = store.Value.OpenSession("test-hangar"))
            {
                session.Store(document);
                session.SaveChanges();
            }
        }
    }
}