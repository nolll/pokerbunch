using System.Collections.Generic;
using PokerBunch.Client.Connection;
using PokerBunch.Client.Models;
using PokerBunch.Client.Models.Request;
using PokerBunch.Client.Models.Response;
using PokerBunch.Common.Urls.ApiUrls;

namespace PokerBunch.Client.Clients
{
    public class AppClient : ApiClient
    {
        public AppClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public App GetById(string id)
        {
            return ApiConnection.Get<App>(new ApiAppUrl(id));
        }

        public IList<App> ListAll()
        {
            return ApiConnection.Get<IList<App>>(new ApiAppsUrl());
        }

        public IList<App> List()
        {
            return ApiConnection.Get<IList<App>>(new ApiUserAppsUrl());
        }

        public App Add(AppAdd postApp)
        {
            return ApiConnection.Post<App>(new ApiAppsUrl(), postApp);
        }

        public void Delete(string appId)
        {
            ApiConnection.Delete(new ApiAppUrl(appId));
        }
    }
}