using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Services;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class EditCashgame
    {
        private readonly CashgameService _cashgameService;
        private readonly UserService _userService;
        private readonly PlayerService _playerService;
        private readonly LocationService _locationService;
        private readonly EventService _eventService;
        private readonly BunchService _bunchService;

        public EditCashgame(CashgameService cashgameService, UserService userService, PlayerService playerService, LocationService locationService, EventService eventService, BunchService bunchService)
        {
            _cashgameService = cashgameService;
            _userService = userService;
            _playerService = playerService;
            _locationService = locationService;
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
            var bunch = _bunchService.Get(cashgame.BunchId);
            var player = _playerService.GetByUserId(bunch.Slug, user.Id);
            RequireRole.Manager(user, player);
            var location = _locationService.Get(request.LocationId);
            cashgame = new Cashgame(cashgame.BunchId, location.Id, cashgame.Status, cashgame.Id);
            _cashgameService.UpdateGame(cashgame);

            if (request.EventId > 0)
            {
                _eventService.AddCashgame(request.EventId, cashgame.Id);
            }
            
            return new Result(cashgame.Id);
        }

        public class Request
        {
            public string UserName { get; }
            public int Id { get; }
            [Range(1, int.MaxValue, ErrorMessage = "Please select a location")]
            public int LocationId { get; }
            public int EventId { get; }

            public Request(string userName, int id, int locationId, int eventId)
            {
                UserName = userName;
                Id = id;
                LocationId = locationId;
                EventId = eventId;
            }
        }
        public class Result
        {
            public int CashgameId { get; private set; }

            public Result(int cashgameId)
            {
                CashgameId = cashgameId;
            }
        }
    }
}
