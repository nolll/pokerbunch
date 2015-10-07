using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Services;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class AddCashgame
    {
        private readonly BunchService _bunchService;
        private readonly CashgameService _cashgameService;
        private readonly UserService _userService;
        private readonly PlayerService _playerService;
        private readonly LocationService _locationService;

        public AddCashgame(BunchService bunchService, CashgameService cashgameService, UserService userService, PlayerService playerService, LocationService locationService)
        {
            _bunchService = bunchService;
            _cashgameService = cashgameService;
            _userService = userService;
            _playerService = playerService;
            _locationService = locationService;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var user = _userService.GetByNameOrEmail(request.UserName);
            var bunch = _bunchService.GetBySlug(request.Slug);
            var player = _playerService.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequirePlayer(user, player);
            var location = _locationService.Get(request.LocationId);
            var cashgame = new Cashgame(bunch.Id, location.Id, GameStatus.Running);
            var cashgameId = _cashgameService.AddGame(bunch, cashgame);

            return new Result(request.Slug, cashgameId);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }
            [Range(1, int.MaxValue, ErrorMessage = "Please a location")]
            public int LocationId { get; private set; }

            public Request(string userName, string slug, int locationId)
            {
                UserName = userName;
                Slug = slug;
                LocationId = locationId;
            }
        }

        public class Result
        {
            public string Slug { get; private set; }
            public int CashgameId { get; private set; }

            public Result(string slug, int cashgameId)
            {
                Slug = slug;
                CashgameId = cashgameId;
            }
        }
    }
}