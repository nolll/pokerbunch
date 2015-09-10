using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Repositories;
using Core.Services;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class AddCashgame
    {
        private readonly BunchService _bunchService;
        private readonly CashgameService _cashgameService;
        private readonly UserService _userService;
        private readonly IPlayerRepository _playerRepository;

        public AddCashgame(BunchService bunchService, CashgameService cashgameService, UserService userService, IPlayerRepository playerRepository)
        {
            _bunchService = bunchService;
            _cashgameService = cashgameService;
            _userService = userService;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var user = _userService.GetByNameOrEmail(request.UserName);
            var bunch = _bunchService.GetBySlug(request.Slug);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequirePlayer(user, player);
            var cashgame = new Cashgame(bunch.Id, request.Location, GameStatus.Running);
            var cashgameId = _cashgameService.AddGame(bunch, cashgame);

            return new Result(request.Slug, cashgameId);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }
            [Required(ErrorMessage = "Please select or enter a location")]
            public string Location { get; private set; }

            public Request(string userName, string slug, string location)
            {
                UserName = userName;
                Slug = slug;
                Location = location;
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