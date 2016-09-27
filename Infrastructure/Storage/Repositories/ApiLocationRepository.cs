using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Infrastructure.Storage.Repositories
{
    public class ApiLocationRepository : ILocationRepository
    {
        private readonly ApiConnection _apiConnection;

        public ApiLocationRepository(ApiConnection apiConnection)
        {
            _apiConnection = apiConnection;
        }

        public Location Get(int id)
        {
            var apiLocation = _apiConnection.Get<ApiLocation>($"location/get/{id}");
            return CreateLocation(apiLocation);
        }

        public IList<Location> List(string slug)
        {
            var apiLocation = _apiConnection.Get<IList<ApiLocation>>($"location/list/{slug}");
            return apiLocation.Select(CreateLocation).ToList();
        }

        public int Add(Location location)
        {
            var postLocation = new ApiLocation(location.Name, location.Slug);
            var apiLocation = _apiConnection.Post<ApiLocation>($"location/add", postLocation);
            return CreateLocation(apiLocation).Id;
        }

        private Location CreateLocation(ApiLocation l)
        {
            return new Location(l.Id, l.Name, l.Bunch);
        }

        public class ApiLocation : IEntity
        {
            public int Id { get; set; }
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