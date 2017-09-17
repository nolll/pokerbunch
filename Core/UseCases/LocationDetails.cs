using Core.Services;

namespace Core.UseCases
{
    public class LocationDetails
    {
        private readonly ILocationService _locationService;

        public LocationDetails(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public Result Execute(Request request)
        {
            var location = _locationService.Get(request.LocationId);
            
            return new Result(location.Id, location.Name, location.BunchId);
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
            public string Id { get; }
            public string Name { get; }
            public string Slug { get; }

            public Result(string id, string name, string slug)
            {
                Id = id;
                Name = name;
                Slug = slug;
            }
        }
    }
}