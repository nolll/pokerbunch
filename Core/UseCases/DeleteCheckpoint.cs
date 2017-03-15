using Core.Entities;
using Core.Repositories;

namespace Core.UseCases
{
    public class DeleteCheckpoint
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public DeleteCheckpoint(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameRepository.GetByCheckpoint(request.CheckpointId);
            var checkpoint = cashgame.GetCheckpoint(request.CheckpointId);
            var bunch = _bunchRepository.Get(cashgame.BunchId);
            cashgame.DeleteCheckpoint(checkpoint);
            _cashgameRepository.Update(cashgame);

            var gameIsRunning = cashgame.Status == GameStatus.Running;
            return new Result(bunch.Id, gameIsRunning, cashgame.Id);
        }

        public class Request
        {
            public string CheckpointId { get; }

            public Request(string checkpointId)
            {
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
