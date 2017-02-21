using Core.Repositories;

namespace Core.UseCases
{
    public class EventMatrix : Matrix
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public EventMatrix(IBunchRepository bunchRepository, IEventRepository eventRepository, ICashgameRepository cashgameRepository, IPlayerRepository playerRepository)
            : base(playerRepository)
        {
            _bunchRepository = bunchRepository;
            _eventRepository = eventRepository;
            _cashgameRepository = cashgameRepository;
        }

        public Result Execute(Request request)
        {
            var @event = _eventRepository.Get(request.EventId);
            var bunch = _bunchRepository.Get(@event.BunchId);
            var cashgames = _cashgameRepository.EventList(request.EventId);
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