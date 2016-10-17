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
        private readonly UserService _userService;
        private readonly PlayerService _playerService;
        private readonly ILocationRepository _locationRepository;
        private readonly EventService _eventService;
        private readonly BunchService _bunchService;

        public EditCashgame(CashgameService cashgameService, UserService userService, PlayerService playerService, ILocationRepository locationRepository, EventService eventService, BunchService bunchService)
        {
            _cashgameService = cashgameService;
            _userService = userService;
            _playerService = playerService;
            _locationRepository = locationRepository;
            _eventService = eventService;
            _bunchService = bunchService;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var cashgame = _cashgameService.GetById(request.Id);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var bunch = _bunchService.Get(cashgame.Bunch);
            var player = _playerService.GetByUserId(bunch.Slug, user.Id);
            RequireRole.Manager(user, player);
            var location = _locationRepository.Get(request.LocationId);
            cashgame = new Cashgame(cashgame.Bunch, cashgame.BunchId, location.Id, cashgame.Status, cashgame.Id);
            _cashgameService.UpdateGame(cashgame);

            if (!string.IsNullOrEmpty(request.EventId))
            {
                _eventService.AddCashgame(request.EventId, cashgame.Id);
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
