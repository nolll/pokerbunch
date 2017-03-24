using Core.Entities;
using Core.Repositories;

namespace Core.UseCases
{
    public class AddCashgame
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IEventRepository _eventRepository;

        public AddCashgame(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IEventRepository eventRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _eventRepository = eventRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.Get(request.Slug);
            var cashgame = new Cashgame(bunch.Id, request.LocationId, GameStatus.Running);
            var cashgameId = _cashgameRepository.Add(bunch, cashgame);

            if (!string.IsNullOrEmpty(request.EventId))
            {
                _eventRepository.AddCashgame(request.EventId, cashgameId);
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
            public string Slug { get; private set; }
            public string CashgameId { get; private set; }

            public Result(string slug, string cashgameId)
            {
                Slug = slug;
                CashgameId = cashgameId;
            }
        }
    }
}