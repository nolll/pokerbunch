using Core.Entities;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases
{
    public class DeleteCheckpoint
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICheckpointRepository _checkpointRepository;

        public DeleteCheckpoint(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, ICheckpointRepository checkpointRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _checkpointRepository = checkpointRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var cashgame = _cashgameRepository.GetByDateString(bunch.Id, request.DateStr);
            var checkpoint = _checkpointRepository.GetCheckpoint(request.CheckpointId);
            _checkpointRepository.DeleteCheckpoint(checkpoint);

            var returnUrl = GetReturnUrl(cashgame.Status, request);
            return new Result(returnUrl);
        }

        private static Url GetReturnUrl(GameStatus status, Request request)
        {
            if(status == GameStatus.Running)
                return new RunningCashgameUrl(request.Slug);
            return new CashgameDetailsUrl(request.Slug, request.DateStr);
        }

        public class Request
        {
            public string Slug { get; private set; }
            public string DateStr { get; private set; }
            public int CheckpointId { get; private set; }

            public Request(string slug, string dateStr, int checkpointId)
            {
                Slug = slug;
                DateStr = dateStr;
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
