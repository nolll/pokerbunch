using System.Collections.Generic;
using Infrastructure.Api.Connection;
using Infrastructure.Api.Models;
using PokerBunch.Common.Urls.ApiUrls;

namespace Infrastructure.Api.Clients
{
    public class AppClient : ApiClient
    {
        public AppClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public ApiApp GetById(string id)
        {
            return ApiConnection.Get<ApiApp>(new ApiAppUrl(id));
        }

        public IList<ApiApp> ListAll()
        {
            return ApiConnection.Get<IList<ApiApp>>(new ApiAppsUrl());
        }

        public IList<ApiApp> List()
        {
            return ApiConnection.Get<IList<ApiApp>>(new ApiUserAppsUrl());
        }

        public ApiApp Add(ApiApp postApp)
        {
            return ApiConnection.Post<ApiApp>(new ApiAppsUrl(), postApp);
        }

        public void Delete(string appId)
        {
            ApiConnection.Delete(new ApiAppUrl(appId));
        }
    }
}