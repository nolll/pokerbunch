using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class EditCashgame
    {
        private readonly CashgameService _cashgameService;
        private readonly IUserRepository _userRepository;
        private readonly PlayerService _playerService;
        private readonly ILocationRepository _locationRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IBunchRepository _bunchRepository;

        public EditCashgame(CashgameService cashgameService, IUserRepository userRepository, PlayerService playerService, ILocationRepository locationRepository, IEventRepository eventRepository, IBunchRepository bunchRepository)
        {
            _cashgameService = cashgameService;
            _userRepository = userRepository;
            _playerService = playerService;
            _locationRepository = locationRepository;
            _eventRepository = eventRepository;
            _bunchRepository = bunchRepository;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var cashgame = _cashgameService.GetById(request.Id);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var bunch = _bunchRepository.Get(cashgame.BunchId);
            var player = _playerService.GetByUserId(bunch.Id, user.Id);
            RequireRole.Manager(user, player);
            var location = _locationRepository.Get(request.LocationId);
            cashgame = new Cashgame(cashgame.BunchId, location.Id, cashgame.Status, cashgame.Id);
            _cashgameService.UpdateGame(cashgame);

            if (!string.IsNullOrEmpty(request.EventId))
            {
                _eventRepository.AddCashgame(request.EventId, cashgame.Id);
            }
            
            return new Result(cashgame.Id);
        }

        public class Request
        {
            public string UserName { get; }
            public string Id { get; }
            [Required(ErrorMessage = "Please select a location")]
            public string LocationId { get; }
            public string EventId { get; }

            public Request(string userName, string id, string locationId, string eventId)
            {
                UserName = userName;
                Id = id;
                LocationId = locationId;
                EventId = eventId;
            }
        }
        public class Result
        {
            public string CashgameId { get; private set; }

            public Result(string cashgameId)
            {
                CashgameId = cashgameId;
            }
        }
    }
}
