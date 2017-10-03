using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using Infrastructure.Api.Connection;
using Infrastructure.Api.Models;
using PokerBunch.Common.Urls.ApiUrls;

namespace Infrastructure.Api.Services
{
    public class AppService : BaseService, IAppService
    {
        public AppService(ApiConnection apiClient) : base(apiClient)
        {
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
    }
}