using Core.Entities;
using Core.Repositories;

namespace Core.UseCases
{
    public class AddLocation
    {
        private readonly ILocationRepository _locationRepository;

        public AddLocation(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public Result Execute(Request request)
        {
            var location = new Location(0, request.Name, request.Slug);
            _locationRepository.Add(location);

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
            public string Slug { get; private set; }

            public Result(string slug)
            {
                Slug = slug;
            }
        }
    }
}