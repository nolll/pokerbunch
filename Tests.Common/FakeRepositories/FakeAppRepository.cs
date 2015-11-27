using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;

namespace Tests.Common.FakeRepositories
{
    public class FakeAppRepository : IAppRepository
    {
        private readonly IList<App> _list;
        public App Added { get; private set; }

        public FakeAppRepository()
        {
            _list = new List<App>();
        }

        public App Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public IList<App> GetList(IList<int> ids)
        {
            throw new System.NotImplementedException();
        }

        public IList<int> Find()
        {
            throw new System.NotImplementedException();
        }

        public IList<int> Find(int userId)
        {
            throw new System.NotImplementedException();
        }

        public IList<int> Find(string appKey)
        {
            throw new System.NotImplementedException();
        }

        public int Add(App app)
        {
            Added = app;
            const int id = 1000;
            _list.Add(new App(id, app.AppKey, app.Name, app.UserId));
            return id;
        }

        public void Update(App app)
        {
            throw new System.NotImplementedException();
        }
    }
}