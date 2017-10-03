using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using Infrastructure.Api.Clients;
using Infrastructure.Api.Models;

namespace Infrastructure.Api.Services
{
    public class LocationService : BaseService, ILocationService
    {
        public LocationService(PokerBunchClient apiClient) : base(apiClient)
        {
        }

        public Location Get(string id)
        {
            var apiLocation = ApiClient.Locations.Get(id);
            return CreateLocation(apiLocation);
        }

        public IList<Location> List(string bunchId)
        {
            var apiLocations = ApiClient.Locations.List(bunchId);
            return apiLocations.Select(CreateLocation).ToList();
        }

        public string Add(Location location)
        {
            var postLocation = new ApiLocation(location.Name, location.BunchId);
            var apiLocation = ApiClient.Locations.Add(postLocation);
            return CreateLocation(apiLocation).Id;
        }

        private Location CreateLocation(ApiLocation l)
        {
            return new Location(l.Id, l.Name, l.Bunch);
        }
    }
}