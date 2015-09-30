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

        public EditCashgame(CashgameService cashgameService, UserService userService, PlayerService playerService, LocationService locationService)
        {
            _cashgameService = cashgameService;
            _userService = userService;
            _playerService = playerService;
            _locationService = locationService;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var cashgame = _cashgameService.GetById(request.Id);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerService.GetByUserId(cashgame.BunchId, user.Id);
            RoleHandler.RequireManager(user, player);
            var location = GetOrCreateLocation(cashgame.BunchId, request.Location);
            cashgame = new Cashgame(cashgame.BunchId, location.Id, cashgame.Status, cashgame.Id);
            _cashgameService.UpdateGame(cashgame);
            
            return new Result(cashgame.Id);
        }

        private Location GetOrCreateLocation(int bunchId, string locationName)
        {
            var location = _locationService.GetByName(bunchId, locationName);
            if (location != null)
                return location;
            var id = _locationService.Add(new Location(0, locationName, bunchId));
            return _locationService.Get(id);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public int Id { get; private set; }
            [Required(ErrorMessage = "Please select or enter a location")]
            public string Location { get; private set; }

            public Request(string userName, int id, string location)
            {
                UserName = userName;
                Id = id;
                Location = location;
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
