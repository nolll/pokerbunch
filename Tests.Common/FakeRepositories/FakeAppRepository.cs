using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Exceptions;
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

        public App GetById(string id)
        {
            return _list.FirstOrDefault(o => o.Id == id);
        }

        public IList<App> GetList(IList<string> ids)
        {
            return _list;
        }

        public IList<App> ListAll()
        {
            return _list;
        }

        public IList<App> List()
        {
            throw new System.NotImplementedException();
        }

        public App GetByAppKey(string appKey)
        {
            try
            {
                return _list.First(o => o.AppKey == appKey);
            }
            catch
            {
                throw new AppNotFoundException();
            }
        }

        public string Add(App app)
        {
            Added = app;
            const string id = "1000";
            _list.Add(new App(id, app.AppKey, app.Name, app.UserId));
            return id;
        }

        public string Add(string appName)
        {
            throw new NotImplementedException();
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