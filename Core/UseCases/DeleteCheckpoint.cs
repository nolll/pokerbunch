using Core.Repositories;

namespace Core.UseCases
{
    public class DeleteCheckpoint
    {
        private readonly ICashgameRepository _cashgameRepository;

        public DeleteCheckpoint(ICashgameRepository cashgameRepository)
        {
            _cashgameRepository = cashgameRepository;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameRepository.GetDetailedById(request.CashgameId);
            _cashgameRepository.DeleteAction(request.CheckpointId);

            return new Result(cashgame.Bunch.Id, cashgame.IsRunning, cashgame.Id);
        }

        public class Request
        {
            public string CashgameId { get; }
            public string CheckpointId { get; }

            public Request(string cashgameId, string checkpointId)
            {
                CashgameId = cashgameId;
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
