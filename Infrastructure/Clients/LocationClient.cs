using System.Collections.Generic;
using Infrastructure.Api.Connection;
using Infrastructure.Api.Models;
using PokerBunch.Common.Urls.ApiUrls;

namespace Infrastructure.Api.Clients
{
    public class LocationClient : ApiClient
    {
        public LocationClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public ApiLocation Get(string id)
        {
            return ApiConnection.Get<ApiLocation>(new ApiLocationUrl(id));
        }

        public IList<ApiLocation> List(string bunchId)
        {
            return ApiConnection.Get<IList<ApiLocation>>(new ApiBunchLocationsUrl(bunchId));
        }

        public ApiLocation Add(ApiLocation location)
        {
            return ApiConnection.Post<ApiLocation>(new ApiBunchLocationsUrl(location.Bunch), location);
        }
    }
}