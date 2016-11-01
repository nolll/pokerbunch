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
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IEventRepository _eventRepository;

        public EditCashgameForm(IBunchRepository bunchRepository, CashgameService cashgameService, IUserRepository userRepository, IPlayerRepository playerRepository, ILocationRepository locationRepository, IEventRepository eventRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameService = cashgameService;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
            _locationRepository = locationRepository;
            _eventRepository = eventRepository;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameService.GetById(request.Id);
            var bunch = _bunchRepository.Get(cashgame.BunchId);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUser(bunch.Id, user.Id);
            RequireRole.Manager(user, player);

            var locations = _locationRepository.List(bunch.Id);
            var locationItems = locations.Select(o => new LocationItem(o.Id, o.Name)).ToList();

            var events = _eventRepository.ListByBunch(bunch.Id);
            var eventItems = events.Select(o => new EventItem(o.Id, o.Name)).ToList();
            var selectedEvent = _eventRepository.GetByCashgame(cashgame.Id);
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