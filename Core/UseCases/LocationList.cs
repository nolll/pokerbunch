using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Core.UseCases
{
    public class LocationList
    {
        private readonly ILocationRepository _locationRepository;

        public LocationList(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public Result Execute(Request request)
        {
            var locations = _locationRepository.List(request.Slug);
            var locationItems = locations.Select(CreateLocationItem).ToList();
            return new Result(locationItems);
        }

        private static Item CreateLocationItem(Location location)
        {
            return new Item(location.Id, location.Name);
        }

        public class Request
        {
            public string Slug { get; }

            public Request(string slug)
            {
                Slug = slug;
            }
        }

        public class Result
        {
            public IList<Item> Events { get; private set; }

            public Result(IList<Item> events)
            {
                Events = events;
            }
        }

        public class Item
        {
            public string LocationId { get; private set; }
            public string Name { get; private set; }

            public Item(string id, string name)
            {
                LocationId = id;
                Name = name;
            }
        }
    }
}