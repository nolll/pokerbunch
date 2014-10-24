using System;
using Raven.Client;
using Raven.Client.Document;

namespace Infrastructure.RavenDb.Repositories
{
    public abstract class RavenRepository : IDisposable
    {
        private readonly IDocumentStore _documentStore;

        protected RavenRepository()
        {
            _documentStore = new DocumentStore { ConnectionStringName = "ravendb", DefaultDatabase = "pokerbunch" }.Initialize();
        }

        protected IDocumentSession GetSession()
        {
            return _documentStore.OpenSession();
        }

        public void Dispose()
        {
            _documentStore.Dispose();
        }
    }
}