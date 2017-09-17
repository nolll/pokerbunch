using Core.Services;

namespace Core.UseCases
{
    public class DeleteCheckpoint
    {
        private readonly ICashgameService _cashgameService;

        public DeleteCheckpoint(ICashgameService cashgameService)
        {
            _cashgameService = cashgameService;
        }

        public Result Execute(Request request)
        {
            var cashgame = _cashgameService.GetDetailedById(request.CashgameId);
            _cashgameService.DeleteAction(request.CheckpointId);

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
            public string Slug { get; }
            public bool GameIsRunning { get; }
            public string CashgameId { get; }

            public Result(string slug, bool gameIsRunning, string cashgameId)
            {
                Slug = slug;
                GameIsRunning = gameIsRunning;
                CashgameId = cashgameId;
            }
        }
    }
}
