using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class LocationList
    {
        private readonly BunchService _bunchService;
        private readonly UserService _userService;
        private readonly PlayerService _playerService;
        private readonly ILocationRepository _locationRepository;

        public LocationList(BunchService bunchService, UserService userService, PlayerService playerService, ILocationRepository locationRepository)
        {
            _bunchService = bunchService;
            _userService = userService;
            _playerService = playerService;
            _locationRepository = locationRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.GetBySlug(request.Slug);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerService.GetByUserId(bunch.Slug, user.Id);
            RequireRole.Player(user, player);
            var locations = _locationRepository.List(bunch.Slug);

            var locationItems = locations.Select(CreateLocationItem).ToList();

            return new Result(locationItems);
        }

        private static Item CreateLocationItem(Location location)
        {
            return new Item(location.Id, location.Name);
        }

        public class Request
        {
            public string UserName { get; }
            public string Slug { get; }

            public Request(string userName, string slug)
            {
                UserName = userName;
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
            public int LocationId { get; private set; }
            public string Name { get; private set; }
            public string Location { get; private set; }
            public Date StartDate { get; private set; }
            public Date EndDate { get; private set; }
            public bool HasGames { get; private set; }

            public Item(int id, string name)
            {
                LocationId = id;
                Name = name;
                HasGames = false;
            }

            public Item(int id, string name, string location, Date startDate, Date endDate)
                : this(id, name)
            {
                Location = location;
                StartDate = startDate;
                EndDate = endDate;
                HasGames = true;
            }
        }
    }
}