using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using Infrastructure.Api.Connection;
using Infrastructure.Api.Models;
using PokerBunch.Common.Urls.ApiUrls;

namespace Infrastructure.Api.Services
{
    public class LocationService : ILocationService
    {
        private readonly ApiConnection _api;

        public LocationService(ApiConnection api)
        {
            _api = api;
        }

        public Location Get(string id)
        {
            var apiLocation = _api.Get<ApiLocation>(new ApiLocationUrl(id));
            return CreateLocation(apiLocation);
        }

        public IList<Location> List(string bunchId)
        {
            var apiLocations = _api.Get<IList<ApiLocation>>(new ApiBunchLocationsUrl(bunchId));
            return apiLocations.Select(CreateLocation).ToList();
        }

        public string Add(Location location)
        {
            var postLocation = new ApiLocation(location.Name, location.BunchId);
            var apiLocation = _api.Post<ApiLocation>(new ApiBunchLocationsUrl(location.BunchId), postLocation);
            return CreateLocation(apiLocation).Id;
        }

        private Location CreateLocation(ApiLocation l)
        {
            return new Location(l.Id, l.Name, l.Bunch);
        }
    }
}