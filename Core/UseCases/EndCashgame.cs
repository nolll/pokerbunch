using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class EndCashgame
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly CashgameService _cashgameService;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public EndCashgame(IBunchRepository bunchRepository, CashgameService cashgameService, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameService = cashgameService;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }

        public void Execute(Request request)
        {
            var bunch = _bunchRepository.Get(request.Slug);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUser(bunch.Id, user.Id);
            RequireRole.Player(user, player);
            var cashgame = _cashgameService.GetRunning(bunch.Id);

            if (cashgame != null)
                _cashgameService.End(cashgame);
        }

        public class Request
        {
            public string UserName { get; }
            public string Slug { get; }

            public Request(string userName, string slug)
            {
                UserName = userName;
                Slug = slug;
            }
        }
    }
}
