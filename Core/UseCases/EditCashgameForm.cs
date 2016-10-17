using System.Collections.Generic;
using System.Linq;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class EditCashgameForm
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly CashgameService _cashgameService;
        private readonly UserService _userService;
        private readonly PlayerService _playerService;
        private readonly ILocationRepository _locationRepository;
        private readonly EventService _eventService;

        public EditCashgameForm(IBunchRepository bunchRepository, CashgameService cashgameService, UserService userService, PlayerService playerService, ILocationRepository locationRepository, EventService eventService)
        {
            _bunchRepository = bunchRepository;
            _cashgameService = cashgameService;
            _userService = userService;
            _playerService = playerService;
            _locationRepository = locationRepository;
            _eventService = eventService;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameService.GetById(request.Id);
            var bunch = _bunchRepository.Get(cashgame.Bunch);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerService.GetByUserId(bunch.Id, user.Id);
            RequireRole.Manager(user, player);

            var locations = _locationRepository.List(bunch.Id);
            var locationItems = locations.Select(o => new LocationItem(o.Id, o.Name)).ToList();

            var events = _eventService.GetByBunch(bunch.Id);
            var eventItems = events.Select(o => new EventItem(o.Id, o.Name)).ToList();
            var selectedEvent = _eventService.GetByCashgame(cashgame.Id);
            var selectedEventId = selectedEvent?.Id ?? "";

            return new Result(cashgame.DateString, cashgame.Id, bunch.Id, cashgame.LocationId, locationItems, selectedEventId, eventItems);
        }

        public class Request
        {
            public string UserName { get; }
            public string Id { get; }

            public Request(string userName, string id)
            {
                UserName = userName;
                Id = id;
            }
        }

        public class Result
        {
            public string Date { get; private set; }
            public string CashgameId { get; private set; }
            public string Slug { get; private set; }
            public string LocationId { get; private set; }
            public IList<LocationItem> Locations { get; private set; }
            public string SelectedEventId { get; private set; }
            public IList<EventItem> Events { get; private set; }

            public Result(string date, string cashgameId, string slug, string locationId, IList<LocationItem> locations, string selectedEventId, IList<EventItem> events)
            {
                Date = date;
                CashgameId = cashgameId;
                Slug = slug;
                LocationId = locationId;
                Locations = locations;
                SelectedEventId = selectedEventId;
                Events = events;
            }
        }

        public class LocationItem
        {
            public string Id { get; private set; }
            public string Name { get; private set; }

            public LocationItem(string id, string name)
            {
                Id = id;
                Name = name;
            }
        }

        public class EventItem
        {
            public string Id { get; private set; }
            public string Name { get; private set; }

            public EventItem(string id, string name)
            {
                Id = id;
                Name = name;
            }
        }
    }
}