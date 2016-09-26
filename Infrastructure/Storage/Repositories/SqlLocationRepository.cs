using System.Collections.Generic;
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
            var apiLocation = _apiConnection.ReadObject<ApiLocation>($"location/get/{id}");
            return CreateLocation(apiLocation);
        }

        public IList<Location> Get(IList<int> ids)
        {
        }

        public IList<int> Find(int bunchId)
        {
        }

        public IList<int> Find(int bunchId, string name)
        {
        }

        public int Add(Location location)
        {
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

            public ApiLocation()
            {
            }
        }
    }
}