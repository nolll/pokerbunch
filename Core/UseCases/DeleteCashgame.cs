using Core.Exceptions;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class DeleteCashgame
    {
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IBunchRepository _bunchRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public DeleteCashgame(ICashgameRepository cashgameRepository, IBunchRepository bunchRepository, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _cashgameRepository = cashgameRepository;
            _bunchRepository = bunchRepository;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameRepository.GetById(request.Id);
            var bunch = _bunchRepository.Get(cashgame.BunchId);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUser(bunch.Id, user.Id);
            RequireRole.Manager(user, player);

            if (cashgame.PlayerCount > 0)
                throw new CashgameHasResultsException();

            _cashgameRepository.DeleteGame(cashgame.Id);

            return new Result(bunch.Id);
        }

        public class Request
        {
            public string UserName { get; }
            public string Id { get; }

            public Request(string userName, string id)
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
