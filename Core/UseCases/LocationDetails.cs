using Core.Repositories;

namespace Core.UseCases
{
    public class LocationDetails
    {
        private readonly ILocationRepository _locationRepository;

        public LocationDetails(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public Result Execute(Request request)
        {
            var location = _locationRepository.Get(request.LocationId);
            
            return new Result(location.Id, location.Name, location.Slug);
        }

        public class Request
        {
            public string LocationId { get; }

            public Request(string locationId)
            {
                LocationId = locationId;
            }
        }

        public class Result
        {
            public string Id { get; private set; }
            public string Name { get; private set; }
            public string Slug { get; private set; }

            public Result(string id, string name, string slug)
            {
                Id = id;
                Name = name;
                Slug = slug;
            }
        }
    }
}