using System.Collections.Generic;
using PokerBunch.Client.Connection;
using PokerBunch.Client.Models.Response;
using PokerBunch.Common.Urls.ApiUrls;

namespace PokerBunch.Client.Clients
{
    public class LocationClient : ApiClient
    {
        public LocationClient(ApiConnection apiConnection) : base(apiConnection)
        {
        }

        public IList<Location> List(string bunchId)
        {
            return ApiConnection.Get<IList<Location>>(new ApiBunchLocationsUrl(bunchId));
        }
    }
}