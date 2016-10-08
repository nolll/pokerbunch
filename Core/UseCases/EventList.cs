using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class EventList
    {
        private readonly BunchService _bunchService;
        private readonly EventService _eventService;
        private readonly UserService _userService;
        private readonly PlayerService _playerService;
        private readonly LocationService _locationService;

        public EventList(BunchService bunchService, EventService eventService, UserService userService, PlayerService playerService, LocationService locationService)
        {
            _bunchService = bunchService;
            _eventService = eventService;
            _userService = userService;
            _playerService = playerService;
            _locationService = locationService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.GetBySlug(request.Slug);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerService.GetByUserId(bunch.Id, user.Id);
            RequireRole.Player(user, player);
            var events = _eventService.GetByBunch(bunch.Id);
            var locationIds = events.Select(o => o.LocationId).Distinct().ToList();
            var locations = _locationService.Get(locationIds);

            var eventItems = events.OrderBy(o => o, new EventComparer()).Select(o => CreateEventItem(o, locations)).ToList();

            return new Result(eventItems);
        }

        private static Item CreateEventItem(Event e, IList<Location> locations)
        {
            var location = locations.FirstOrDefault(o => o.Id == e.LocationId);
            var locationName = location != null ? location.Name : "";
            if(e.HasGames)
                return new Item(e.Id, e.Name, locationName, e.StartDate, e.EndDate);
            return new Item(e.Id, e.Name);
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
            public int EventId { get; private set; }
            public string Name { get; private set; }
            public string Location { get; private set; }
            public Date StartDate { get; private set; }
            public Date EndDate { get; private set; }
            public bool HasGames { get; private set; }

            public Item(int id, string name)
            {
                EventId = id;
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

        private class EventComparer : IComparer<Event>
        {
            public int Compare(Event x, Event y)
            {
                if (x.HasGames && y.HasGames)
                    return x.StartDate.CompareTo(y.StartDate);
                if (x.HasGames && !y.HasGames)
                    return -1;
                if (!x.HasGames && y.HasGames)
                    return 1;
                return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
            }
        }
    }
}