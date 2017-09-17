using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class AddLocation
    {
        private readonly ILocationService _locationService;

        public AddLocation(ILocationService locationService)
        {
            _locationService = locationService;
        }

        public Result Execute(Request request)
        {
            var location = new Location("", request.Name, request.Slug);
            _locationService.Add(location);

            return new Result(request.Slug);
        }

        public class Request
        {
            public string Slug { get; }
            public string Name { get; }

            public Request(string slug, string name)
            {
                Slug = slug;
                Name = name;
            }
        }

        public class Result
        {
            public string Slug { get; }

            public Result(string slug)
            {
                Slug = slug;
            }
        }
    }
}