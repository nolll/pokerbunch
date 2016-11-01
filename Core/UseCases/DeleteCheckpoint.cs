using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class DeleteCheckpoint
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly CashgameService _cashgameService;
        private readonly IUserRepository _userRepository;
        private readonly PlayerService _playerService;

        public DeleteCheckpoint(IBunchRepository bunchRepository, CashgameService cashgameService, IUserRepository userRepository, PlayerService playerService)
        {
            _bunchRepository = bunchRepository;
            _cashgameService = cashgameService;
            _userRepository = userRepository;
            _playerService = playerService;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameService.GetByCheckpoint(request.CheckpointId);
            var checkpoint = cashgame.GetCheckpoint(request.CheckpointId);
            var bunch = _bunchRepository.Get(cashgame.BunchId);
            var currentUser = _userRepository.GetByNameOrEmail(request.UserName);
            var currentPlayer = _playerService.GetByUser(bunch.Id, currentUser.Id);
            RequireRole.Manager(currentUser, currentPlayer);
            cashgame.DeleteCheckpoint(checkpoint);
            _cashgameService.Update(cashgame);

            var gameIsRunning = cashgame.Status == GameStatus.Running;
            return new Result(bunch.Id, gameIsRunning, cashgame.Id);
        }

        public class Request
        {
            public string UserName { get; }
            public string CheckpointId { get; }

            public Request(string userName, string checkpointId)
            {
                UserName = userName;
                CheckpointId = checkpointId;
            }
        }

        public class Result
        {
            public string Slug { get; private set; }
            public bool GameIsRunning { get; private set; }
            public string CashgameId { get; private set; }

            public Result(string slug, bool gameIsRunning, string cashgameId)
            {
                Slug = slug;
                GameIsRunning = gameIsRunning;
                CashgameId = cashgameId;
            }
        }
    }
}
