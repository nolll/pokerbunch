using Core.Exceptions;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class DeleteCashgame
    {
        private readonly CashgameService _cashgameService;
        private readonly BunchService _bunchService;
        private readonly UserService _userService;
        private readonly PlayerService _playerService;

        public DeleteCashgame(CashgameService cashgameService, BunchService bunchService, UserService userService, PlayerService playerService)
        {
            _cashgameService = cashgameService;
            _bunchService = bunchService;
            _userService = userService;
            _playerService = playerService;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameService.GetById(request.Id);
            var bunch = _bunchService.Get(cashgame.BunchId);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerService.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequireManager(user, player);

            if (cashgame.PlayerCount > 0)
                throw new CashgameHasResultsException();

            _cashgameService.DeleteGame(cashgame);

            return new Result(bunch.Slug);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public int Id { get; private set; }

            public Request(string userName, int id)
            {
                UserName = userName;
                Id = id;
            }
        }

        public class Result
        {
            public string Slug { get; private set; }

            public Result(string slug)
            {
                Slug = slug;
            }
        }
    }
}
