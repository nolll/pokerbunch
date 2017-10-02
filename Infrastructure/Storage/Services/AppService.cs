using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using JetBrains.Annotations;
using PokerBunch.Common.Urls.ApiUrls;

namespace Infrastructure.Storage.Services
{
    public class AppService : IAppService
    {
        private readonly ApiConnection _api;
        
        public AppService(ApiConnection api)
        {
            _api = api;
        }
        
        public App GetById(string id)
        {
            var apiApp = _api.Get<ApiApp>(new ApiAppUrl(id));
            return CreateApp(apiApp);
        }

        public IList<App> ListAll()
        {
            var apiApps = _api.Get<IList<ApiApp>>(new ApiAppsUrl());
            return apiApps.Select(CreateApp).ToList();
        }

        public IList<App> List()
        {
            var apiApps = _api.Get<IList<ApiApp>>(new ApiUserAppsUrl());
            return apiApps.Select(CreateApp).ToList();
        }

        public string Add(string appName)
        {
            var postApp = new ApiApp(null, null, appName, null);
            var apiApp = _api.Post<ApiApp>(new ApiAppsUrl(), postApp);
            return CreateApp(apiApp).Id;
        }

        public void Update(App app)
        {
            throw new NotImplementedException("Update not implemented yet");
        }

        public void Delete(string appId)
        {
            _api.Delete(new ApiAppUrl(appId));
        }

        private App CreateApp(ApiApp a)
        {
            return new App(a.Id, a.Key, a.Name, a.UserId);
        }

        public class ApiApp
        {
            [UsedImplicitly]
            public string Id { get; set; }
            [UsedImplicitly]
            public string Key { get; set; }
            [UsedImplicitly]
            public string Name { get; set; }
            [UsedImplicitly]
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