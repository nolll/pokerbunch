using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class EndCashgame
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly UserService _userService;
        private readonly IPlayerRepository _playerRepository;

        public EndCashgame(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, UserService userService, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _userService = userService;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequirePlayer(user, player);
            var cashgame = _cashgameRepository.GetRunning(bunch.Id);

            if (cashgame == null)
                return null;

            _cashgameRepository.EndGame(bunch, cashgame);
            return new Result(cashgame.Id);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }

            public Request(string userName, string slug)
            {
                UserName = userName;
                Slug = slug;
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
