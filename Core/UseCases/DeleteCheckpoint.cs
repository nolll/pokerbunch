using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases
{
    public class DeleteCheckpoint
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public DeleteCheckpoint(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, ICheckpointRepository checkpointRepository, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _checkpointRepository = checkpointRepository;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var checkpoint = _checkpointRepository.GetCheckpoint(request.CheckpointId);
            var cashgame = _cashgameRepository.GetById(checkpoint.CashgameId);
            var bunch = _bunchRepository.GetById(cashgame.BunchId);
            var currentUser = _userRepository.GetByNameOrEmail(request.UserName);
            var currentPlayer = _playerRepository.GetByUserId(cashgame.BunchId, currentUser.Id);
            RoleHandler.RequireManager(currentUser, currentPlayer);
            _checkpointRepository.DeleteCheckpoint(checkpoint);

            var returnUrl = GetReturnUrl(cashgame.Status, bunch.Slug, cashgame);
            return new Result(returnUrl);
        }

        private static Url GetReturnUrl(GameStatus status, string slug, Cashgame cashgame)
        {
            if(status == GameStatus.Running)
                return new RunningCashgameUrl(slug);
            return new CashgameDetailsUrl(cashgame.Id);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public int CheckpointId { get; private set; }

            public Request(string userName, int checkpointId)
            {
                UserName = userName;
                CheckpointId = checkpointId;
            }
        }

        public class Result
        {
            public Url ReturnUrl { get; private set; }

            public Result(Url returnUrl)
            {
                ReturnUrl = returnUrl;
            }
        }
    }
}
