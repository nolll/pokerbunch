using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using JetBrains.Annotations;

namespace Infrastructure.Storage.Repositories
{
    public class ApiLocationRepository : ApiRepository, ILocationRepository
    {
        private readonly ApiConnection _api;

        public ApiLocationRepository(ApiConnection api)
        {
            _api = api;
        }

        public Location Get(string id)
        {
            var apiLocation = _api.Get<ApiLocation>(Url.LocationSingle(id));
            return CreateLocation(apiLocation);
        }

        public IList<Location> List(string id)
        {
            var apiLocations = _api.Get<IList<ApiLocation>>(Url.LocationBunchList(id));
            return apiLocations.Select(CreateLocation).ToList();
        }

        public string Add(Location location)
        {
            var postLocation = new ApiLocation(location.Name, location.BunchId);
            var apiLocation = _api.Post<ApiLocation>(Url.LocationList, postLocation);
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