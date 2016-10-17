using System.Collections.Generic;
using System.Linq;
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
            _list = CreateList();
        }

        public App Get(string id)
        {
            return _list.FirstOrDefault(o => o.Id == id);
        }

        public IList<App> GetList(IList<string> ids)
        {
            return _list;
        }

        public IList<string> Find()
        {
            return _list.Select(o => o.Id).ToList();
        }

        public IList<string> FindByUser(string userId)
        {
            throw new System.NotImplementedException();
        }

        public IList<string> FindByAppKey(string appKey)
        {
            return _list.Where(o => o.AppKey == appKey).Select(o => o.Id).ToList();
        }

        public string Add(App app)
        {
            Added = app;
            const string id = "1000";
            _list.Add(new App(id, app.AppKey, app.Name, app.UserId));
            return id;
        }

        public void Update(App app)
        {
            throw new System.NotImplementedException();
        }

        private IList<App> CreateList()
        {
            return new List<App>
            {
                TestData.AppA,
                TestData.AppB
            };
        }
    }
}