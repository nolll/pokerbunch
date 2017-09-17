using Core.Services;

namespace Core.UseCases
{
    public class AddCashgame
    {
        private readonly ICashgameService _cashgameService;
        private readonly IEventService _eventService;

        public AddCashgame(ICashgameService cashgameService, IEventService eventService)
        {
            _cashgameService = cashgameService;
            _eventService = eventService;
        }

        public Result Execute(Request request)
        {
            var cashgameId = _cashgameService.Add(request.Slug, request.LocationId);

            if (!string.IsNullOrEmpty(request.EventId))
            {
                _eventService.AddCashgame(request.EventId, cashgameId);
            }

            return new Result(request.Slug, cashgameId);
        }

        public class Request
        {
            public string Slug { get; }
            public string LocationId { get; }
            public string EventId { get; }

            public Request(string slug, string locationId, string eventId)
            {
                Slug = slug;
                LocationId = locationId;
                EventId = eventId;
            }
        }

        public class Result
        {
            public string Slug { get; }
            public string CashgameId { get; }

            public Result(string slug, string cashgameId)
            {
                Slug = slug;
                CashgameId = cashgameId;
            }
        }
    }
}