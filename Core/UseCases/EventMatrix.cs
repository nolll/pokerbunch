using Core.Services;

namespace Core.UseCases
{
    public class EventMatrix : Matrix
    {
        private readonly IBunchService _bunchService;
        private readonly IEventService _eventService;
        private readonly ICashgameService _cashgameService;

        public EventMatrix(IBunchService bunchService, IEventService eventService, ICashgameService cashgameService, IPlayerService playerService)
            : base(playerService)
        {
            _bunchService = bunchService;
            _eventService = eventService;
            _cashgameService = cashgameService;
        }

        public Result Execute(Request request)
        {
            var @event = _eventService.Get(request.EventId);
            var bunch = _bunchService.Get(@event.BunchId);
            var cashgames = _cashgameService.EventList(request.EventId);
            return Execute(bunch, cashgames);
        }

        public class Request
        {
            public string EventId { get; }

            public Request(string eventId)
            {
                EventId = eventId;
            }
        }
    }
}