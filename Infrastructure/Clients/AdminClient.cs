using Infrastructure.Api.Connection;
using Infrastructure.Api.Models;
using PokerBunch.Common.Urls.ApiUrls;

namespace Infrastructure.Api.Clients
{
    public class AdminClient : ApiClient
    {
        public AdminClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public ApiMessage ClearCache()
        {
            return ApiConnection.Post<ApiMessage>(new ApiAdminClearCacheUrl());
        }

        public ApiMessage SendEmail()
        {
            return ApiConnection.Post<ApiMessage>(new ApiAdminSendEmailUrl());
        }
    }
}