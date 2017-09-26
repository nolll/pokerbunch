using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using JetBrains.Annotations;
using PokerBunch.Common.Urls.ApiUrls;

namespace Infrastructure.Storage.Services
{
    public class EventService : IEventService
    {
        private readonly ApiConnection _api;

        public EventService(ApiConnection api)
        {
            _api = api;
        }

        public Event Get(string id)
        {
            var apiEvent = _api.Get<ApiEvent>(new ApiEventUrl(id));
            return CreateEvent(apiEvent);
        }

        public IList<Event> ListByBunch(string bunchId)
        {
            var apiEvents = _api.Get<IList<ApiEvent>>(new ApiBunchEventsUrl(bunchId));
            return apiEvents.Select(CreateEvent).ToList();
        }
        
        public string Add(Event e)
        {
            var postEvent = new ApiEvent(e.Name, e.BunchId);
            var apiEvent = _api.Post<ApiEvent>(new ApiBunchEventsUrl(e.BunchId), postEvent);
            return CreateEvent(apiEvent).Id;
        }

        public void AddCashgame(string eventId, string cashgameId)
        {
            var postCashame = new ApiEventCashgame(cashgameId);
            _api.Post<ApiEventCashgame>(new ApiEventCashgamesUrl(eventId), postCashame);
        }

        private Event CreateEvent(ApiEvent e)
        {
            if (e.Location != null && e.StartDate != null)
            {
                var location = new SmallLocation(e.Location.Id, e.Location.Name);
                var startDate = new Date(e.StartDate);
                return new Event(e.Id, e.BunchId, e.Name, location, startDate);
            }
            return new Event(e.Id, e.BunchId, e.Name);
        }

        private class ApiEventCashgame
        {
            [UsedImplicitly]
            public string CashgameId { get; set; }

            public ApiEventCashgame(string cashgameId)
            {
                CashgameId = cashgameId;
            }
        }

        private class ApiEvent
        {
            [UsedImplicitly]
            public string Id { get; set; }
            [UsedImplicitly]
            public string BunchId { get; set; }
            [UsedImplicitly]
            public string Name { get; set; }
            [UsedImplicitly]
            public string StartDate { get; set; }
            [UsedImplicitly]
            public ApiEventLocation Location { get; set; }

            public ApiEvent(string name, string bunchId)
            {
                Name = name;
                BunchId = bunchId;
            }

            public ApiEvent()
            {
            }
        }

        public class ApiEventLocation
        {
            [UsedImplicitly]
            public string Id { get; set; }
            [UsedImplicitly]
            public string Name { get; set; }
        }
    }
}