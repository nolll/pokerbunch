using System.Collections.Generic;
using PokerBunch.Client.Connection;
using PokerBunch.Client.Models;
using PokerBunch.Client.Models.Request;
using PokerBunch.Client.Models.Response;
using PokerBunch.Common.Urls.ApiUrls;

namespace PokerBunch.Client.Clients
{
    public class LocationClient : ApiClient
    {
        public LocationClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Location Get(string id)
        {
            return ApiConnection.Get<Location>(new ApiLocationUrl(id));
        }

        public IList<Location> List(string bunchId)
        {
            return ApiConnection.Get<IList<Location>>(new ApiBunchLocationsUrl(bunchId));
        }

        public Location Add(string bunchId, LocationAdd location)
        {
            return ApiConnection.Post<Location>(new ApiBunchLocationsUrl(bunchId), location);
        }
    }
}