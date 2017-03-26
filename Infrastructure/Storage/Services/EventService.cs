using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;
using JetBrains.Annotations;

namespace Infrastructure.Storage.Services
{
    public class EventService : BaseService, IEventService
    {
        private readonly ApiConnection _api;

        public EventService(ApiConnection api)
        {
            _api = api;
        }

        public Event Get(string id)
        {
            var apiEvent = _api.Get<ApiEvent>(Url.Event(id));
            return CreateEvent(apiEvent);
        }

        public IList<Event> ListByBunch(string bunchId)
        {
            var apiEvents = _api.Get<IList<ApiEvent>>(Url.EventsByBunch(bunchId));
            return apiEvents.Select(CreateEvent).ToList();
        }
        
        public string Add(Event e)
        {
            var postEvent = new ApiEvent(e.Name, e.BunchId);
            var apiEvent = _api.Post<ApiEvent>(Url.EventsByBunch(e.BunchId), postEvent);
            return CreateEvent(apiEvent).Id;
        }

        public void AddCashgame(string eventId, string cashgameId)
        {
            var postCashame = new ApiEventCashgame(cashgameId);
            _api.Post<ApiEventCashgame>(Url.CashgamesByEvent(eventId), postCashame);
        }

        private Event CreateEvent(ApiEvent e)
        {
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