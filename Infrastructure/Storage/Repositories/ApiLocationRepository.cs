using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Storage.Repositories
{
    public class ApiLocationRepository : ILocationRepository
    {
        private readonly ApiConnection _api;

        public ApiLocationRepository(ApiConnection api)
        {
            _api = api;
        }

        public Location Get(string id)
        {
            var apiLocation = _api.Get<ApiLocation>($"location/get/{id}");
            return CreateLocation(apiLocation);
        }

        public IList<Location> List(string slug)
        {
            var apiLocations = _api.Get<IList<ApiLocation>>($"location/list/{slug}");
            return apiLocations.Select(CreateLocation).ToList();
        }

        public string Add(Location location)
        {
            var postLocation = new ApiLocation(location.Name, location.BunchId);
            var apiLocation = _api.Post<ApiLocation>($"location/add", postLocation);
            return CreateLocation(apiLocation).Id;
        }

        private Location CreateLocation(ApiLocation l)
        {
            return new Location(l.Id, l.Name, l.Bunch);
        }

        private class ApiLocation
        {
            public string Id { get; set; }
            public string Name { get; set; }
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