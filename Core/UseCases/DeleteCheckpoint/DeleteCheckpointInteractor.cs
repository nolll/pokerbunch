using Core.Entities;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases.DeleteCheckpoint
{
    public class DeleteCheckpointInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly ICheckpointRepository _checkpointRepository;

        public DeleteCheckpointInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, ICheckpointRepository checkpointRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _checkpointRepository = checkpointRepository;
        }

        public DeleteCheckpointResult Execute(DeleteCheckpointRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var cashgame = _cashgameRepository.GetByDateString(bunch.Id, request.DateStr);
            var checkpoint = _checkpointRepository.GetCheckpoint(request.CheckpointId);
            _checkpointRepository.DeleteCheckpoint(checkpoint);

            var returnUrl = GetReturnUrl(cashgame.Status, request);
            return new DeleteCheckpointResult(returnUrl);
        }

        private static Url GetReturnUrl(GameStatus status, DeleteCheckpointRequest request)
        {
            if(status == GameStatus.Running)
                return new RunningCashgameUrl(request.Slug);
            return new CashgameDetailsUrl(request.Slug, request.DateStr);
        }
    }
}
