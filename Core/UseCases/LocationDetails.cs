using Core.Services;

namespace Core.UseCases
{
    public class LocationDetails
    {
        private readonly LocationService _locationService;

        public LocationDetails(LocationService locationService)
        {
            _locationService = locationService;
        }

        public Result Execute(Request request)
        {
            var location = _locationService.Get(request.LocationId);
            
            return new Result(location.Id, location.Name, location.Slug);
        }

        public class Request
        {
            public int LocationId { get; }

            public Request(int locationId)
            {
                LocationId = locationId;
            }
        }

        public class Result
        {
            public int Id { get; private set; }
            public string Name { get; private set; }
            public string Slug { get; private set; }

            public Result(int id, string name, string slug)
            {
                Id = id;
                Name = name;
                Slug = slug;
            }
        }
    }
}