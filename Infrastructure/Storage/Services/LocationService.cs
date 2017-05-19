using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using JetBrains.Annotations;

namespace Infrastructure.Storage.Services
{
    public class LocationService : BaseService, ILocationService
    {
        private readonly ApiConnection _api;

        public LocationService(ApiConnection api)
        {
            _api = api;
        }

        public Location Get(string id)
        {
            var apiLocation = _api.Get<ApiLocation>(Url.Location(id));
            return CreateLocation(apiLocation);
        }

        public IList<Location> List(string bunchId)
        {
            var apiLocations = _api.Get<IList<ApiLocation>>(Url.LocationByBunch(bunchId));
            return apiLocations.Select(CreateLocation).ToList();
        }

        public string Add(Location location)
        {
            var postLocation = new ApiLocation(location.Name, location.BunchId);
            var apiLocation = _api.Post<ApiLocation>(Url.LocationAdd(location.BunchId), postLocation);
            return CreateLocation(apiLocation).Id;
        }

        private Location CreateLocation(ApiLocation l)
        {
            return new Location(l.Id, l.Name, l.Bunch);
        }

        private class ApiLocation
        {
            [UsedImplicitly]
            public string Id { get; set; }
            [UsedImplicitly]
            public string Name { get; set; }
            [UsedImplicitly]
            public string Bunch { get; set; }

            public ApiLocation(string name, string bunch)
            {
                Name = name;
                Bunch = bunch;
            }

            public ApiLocation()
            {
            }
        }
    }
}