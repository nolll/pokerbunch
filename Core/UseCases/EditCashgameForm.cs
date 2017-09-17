using System;
using System.Collections.Generic;
using System.Linq;
using Core.Services;

namespace Core.UseCases
{
    public class EditCashgameForm
    {
        private readonly ICashgameService _cashgameService;
        private readonly ILocationService _locationService;
        private readonly IEventService _eventService;

        public EditCashgameForm(ICashgameService cashgameService, ILocationService locationService, IEventService eventService)
        {
            _cashgameService = cashgameService;
            _locationService = locationService;
            _eventService = eventService;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameService.GetDetailedById(request.Id);
            var locations = _locationService.List(cashgame.Bunch.Id);
            var locationItems = locations.Select(o => new LocationItem(o.Id, o.Name)).ToList();

            var events = _eventService.ListByBunch(cashgame.Bunch.Id);
            var eventItems = events.Select(o => new EventItem(o.Id, o.Name)).ToList();
            var selectedEventId = cashgame.BelongsToEvent ? cashgame.Event.Id : "";
            var localStartTime = TimeZoneInfo.ConvertTimeFromUtc(cashgame.StartTime, cashgame.Bunch.Timezone);
            var dateString = Globalization.FormatIsoDate(localStartTime);

            return new Result(dateString, cashgame.Id, cashgame.Bunch.Id, cashgame.Location.Id, locationItems, selectedEventId, eventItems);
        }

        public class Request
        {
            public string Id { get; }

            public Request(string id)
            {
                Id = id;
            }
        }

        public class Result
        {
            public string Date { get; }
            public string CashgameId { get; }
            public string Slug { get; }
            public string LocationId { get; }
            public IList<LocationItem> Locations { get; }
            public string SelectedEventId { get; }
            public IList<EventItem> Events { get; }

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
            public string Id { get; }
            public string Name { get; }

            public LocationItem(string id, string name)
            {
                Id = id;
                Name = name;
            }
        }

        public class EventItem
        {
            public string Id { get; }
            public string Name { get; }

            public EventItem(string id, string name)
            {
                Id = id;
                Name = name;
            }
        }
    }
}