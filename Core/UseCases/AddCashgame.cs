using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class AddCashgame
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly CashgameService _cashgameService;
        private readonly IUserRepository _userRepository;
        private readonly PlayerService _playerService;
        private readonly ILocationRepository _locationRepository;
        private readonly IEventRepository _eventRepository;

        public AddCashgame(IBunchRepository bunchRepository, CashgameService cashgameService, IUserRepository userRepository, PlayerService playerService, ILocationRepository locationRepository, IEventRepository eventRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameService = cashgameService;
            _userRepository = userRepository;
            _playerService = playerService;
            _locationRepository = locationRepository;
            _eventRepository = eventRepository;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var bunch = _bunchRepository.Get(request.Slug);
            var player = _playerService.GetByUserId(bunch.Id, user.Id);
            RequireRole.Player(user, player);
            var location = _locationRepository.Get(request.LocationId);
            var cashgame = new Cashgame(bunch.Id, location.Id, GameStatus.Running);
            var cashgameId = _cashgameService.AddGame(bunch, cashgame);

            if (!string.IsNullOrEmpty(request.EventId))
            {
                _eventRepository.AddCashgame(request.EventId, cashgameId);
            }

            return new Result(request.Slug, cashgameId);
        }

        public class Request
        {
            public string UserName { get; }
            public string Slug { get; }
            [Required(ErrorMessage = "Please select a location")]
            public string LocationId { get; }
            public string EventId { get; }

            public Request(string userName, string slug, string locationId, string eventId)
            {
                UserName = userName;
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