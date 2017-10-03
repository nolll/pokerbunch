using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using Infrastructure.Api.Clients;
using Infrastructure.Api.Models;
using PokerBunch.Common.Urls.ApiUrls;

namespace Infrastructure.Api.Services
{
    public class AppService : BaseService, IAppService
    {
        public AppService(PokerBunchClient apiClient) : base(apiClient)
        {
        }
        
        public App GetById(string id)
        {
            var apiApp = ApiClient.Apps.GetById(id);
            return CreateApp(apiApp);
        }

        public IList<App> ListAll()
        {
            var apiApps = ApiClient.Apps.ListAll();
            return apiApps.Select(CreateApp).ToList();
        }

        public IList<App> List()
        {
            var apiApps = ApiClient.Apps.List();
            return apiApps.Select(CreateApp).ToList();
        }

        public string Add(string appName)
        {
            var postApp = new ApiApp(null, null, appName, null);
            var apiApp = ApiClient.Apps.Add(postApp);
            return CreateApp(apiApp).Id;
        }

        public void Update(App app)
        {
            throw new NotImplementedException("Update not implemented yet");
        }

        public void Delete(string appId)
        {
            ApiClient.Apps.Delete(appId);
        }

        private App CreateApp(ApiApp a)
        {
            return new App(a.Id, a.Key, a.Name, a.UserId);
        }
    }
}