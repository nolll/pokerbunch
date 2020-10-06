using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using PokerBunch.Client.Clients;
using ApiLocation = PokerBunch.Client.Models.Response.Location;

namespace Infrastructure.Api.Services
{
    public class LocationService : BaseService, ILocationService
    {
        public LocationService(PokerBunchClient apiClient) : base(apiClient)
        {
        }

        public IList<Location> List(string bunchId)
        {
            var apiLocations = ApiClient.Locations.List(bunchId);
            return apiLocations.Select(CreateLocation).ToList();
        }

        private Location CreateLocation(ApiLocation l)
        {
            return new Location(l.Id, l.Name, l.Bunch);
        }
    }
}