using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Storage.Repositories
{
    public class ApiAppRepository : IAppRepository
    {
        private readonly ApiConnection _api;

        public ApiAppRepository(ApiConnection api)
        {
            _api = api;
        }
        
        public App GetById(string id)
        {
            var apiApp = _api.Get<ApiApp>($"apps/{id}");
            return CreateApp(apiApp);
        }

        public IList<App> List()
        {
            var apiApps = _api.Get<IList<ApiApp>>("apps");
            return apiApps.Select(CreateApp).ToList();
        }

        public IList<App> ListByUser(string userId)
        {
            var apiApps = _api.Get<IList<ApiApp>>("user/apps");
            return apiApps.Select(CreateApp).ToList();
        }

        public string Add(App app)
        {
            var postApp = new ApiApp(null, app.AppKey, app.Name, app.UserId);
            var apiApp = _api.Post<ApiApp>("apps", postApp);
            return CreateApp(apiApp).Id;
        }

        public void Update(App app)
        {
            throw new NotImplementedException("Update not implemented yet");
        }

        private App CreateApp(ApiApp a)
        {
            return new App(a.Id, a.Key, a.Name, a.UserId);
        }

        public class ApiApp
        {
            public string Id { get; set; }
            public string Key { get; set; }
            public string Name { get; set; }
            public string UserId { get; set; }

            public ApiApp(string id, string key, string name, string userId)
            {
                Id = id;
                Key = key;
                Name = name;
                UserId = userId;
            }

            public ApiApp()
            {
            }
        }
    }
}